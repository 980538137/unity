using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Globalization;
using System.Threading;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Collections;




namespace HTTP
{
	public class HTTPException : Exception
	{
		public HTTPException (string message) : base(message)
		{
		}
	}
	
	public enum RequestState {
		Waiting, Reading, Done	
	}

	public class Request
	{
		public string method = "GET";
		public string protocol = "HTTP/1.1";
		public byte[] bytes;
		public Uri uri;
		public static byte[] EOL = { (byte)'\r', (byte)'\n' };
		public Response response = null;
		public bool isDone = false;
		public int maximumRetryCount = 8;
		public bool acceptGzip = true;
		public bool useCache = false;
		public Exception exception = null;
		public RequestState state = RequestState.Waiting;
		
		Dictionary<string, List<string>> headers = new Dictionary<string, List<string>> ();
		static Dictionary<string, string> etags = new Dictionary<string, string> ();

		public Request (string method, string uri)
		{
			this.method = method;
			this.uri = new Uri (uri);
		}

		public Request (string method, string uri, bool useCache)
		{
			this.method = method;
			this.uri = new Uri (uri);
			this.useCache = useCache;
		}

		public Request (string method, string uri, byte[] bytes)
		{
			this.method = method;
			this.uri = new Uri (uri);
			this.bytes = bytes;
		}


//		byte[] GetBytes(string str)
//		{
//			byte[] bytes = new byte[str.Length * sizeof(char)];
//			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
//			return bytes;
//		}

		public void AddField(string k, string v) {
			string kv = "&" + k + "=" + v;

			System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
			byte[] kvBuf = ue.GetBytes(kv);
			int kvLen = kvBuf.Length;
//			Debug.Log ("kv string" + kv);

			byte[] newBuff;
			if (null != bytes) {//need append 

				newBuff = new byte[bytes.Length + kvLen];
				System.Buffer.BlockCopy (bytes, 0, newBuff, 0, bytes.Length);
				System.Buffer.BlockCopy (kvBuf, 0, newBuff, bytes.Length, kvLen);
//				System.Buffer.BlockCopy (kv.ToCharArray (), 0, newBuff, bytes.Length, kvLen);
				bytes = newBuff;

			} else {
				bytes = new byte[kvLen];
				System.Buffer.BlockCopy (kvBuf, 0, bytes, 0, kvLen);
			}


//			Debug.Log ("added bytes :" + System.Text.Encoding.UTF8.GetString(bytes));
//			Debug.Log ("bytes len:" + bytes.Length);
			
		}



		public void AddHeader (string name, string value)
		{
			name = name.ToLower ().Trim ();
			value = value.Trim ();
			if (!headers.ContainsKey (name))
				headers[name] = new List<string> ();
			headers[name].Add (value);
		}

		public string GetHeader (string name)
		{
			name = name.ToLower ().Trim ();
			if (!headers.ContainsKey (name))
				return "";
			return headers[name][0];
		}

		public List<string> GetHeaders (string name)
		{
			name = name.ToLower ().Trim ();
			if (!headers.ContainsKey (name))
				headers[name] = new List<string> ();
			return headers[name];
		}

		public void SetHeader (string name, string value)
		{
			name = name.ToLower ().Trim ();
			value = value.Trim ();
			if (!headers.ContainsKey (name))
				headers[name] = new List<string> ();
			headers[name].Clear ();
			headers[name].Add (value);
		}

		public void Send ()
		{
			Debug.Log("Send -1 ");
			isDone = false;
			state = RequestState.Waiting;
			if (acceptGzip)
				SetHeader ("Accept-Encoding", "gzip");
			Debug.Log("Send 0 ");

			var extractAction = new ThreadedAction(SendFunctionNoParam);

//			ThreadPool.QueueUserWorkItem (new WaitCallback (SendFunction));

			Debug.Log("Send 14 ");
		}

		public class ThreadedAction
		{
			public ThreadedAction(Action action)
			{
				var thread = new Thread(() => {
					if(action != null)
						action();
					_isDone = true;
				});
				thread.Start();
			}
			
			public IEnumerator WaitForComplete() {
				while (!_isDone)
					yield return null;
			}
			
			private bool _isDone = false;
		}

		private void SendFunctionNoParam(){
			Debug.Log("SendFunctionNoParam 0 ");
			SendFunction(null);
		}

		private void SendFunction(System.Object t) {
			Debug.Log("SendFunction 0 ");
			try {
				var retry = 0;
				Debug.Log("Send 1 ");
				while (++retry < maximumRetryCount) {
					Debug.Log("Send 2 ");
					if (useCache) {
						string etag = "";
						if (etags.TryGetValue (uri.AbsoluteUri, out etag)) {
							SetHeader ("If-None-Match", etag);
						}
					}
					Debug.Log("Send 3 ");
					
					SetHeader ("Host", uri.Host);
					var client = new TcpClient ();
					client.Connect (uri.Host, uri.Port);
					Debug.Log("Send 4 ");
					using (var stream = client.GetStream ()) {
						Debug.Log("Send 5 ");
						
						var ostream = stream as Stream;
						if (uri.Scheme.ToLower() == "https") {
							Debug.Log("Send 6 ");
							
							ostream = new SslStream (stream, false, new RemoteCertificateValidationCallback (ValidateServerCertificate));
							try {
								var ssl = ostream as SslStream;
								ssl.AuthenticateAsClient (uri.Host);
							} catch (Exception e) {
								Debug.LogError ("Exception: " + e.Message);
								return;
							}
						}
						Debug.Log("Send 7 ");
						
						WriteToStream (ostream);
						response = new Response ();
						state = RequestState.Reading;
						response.ReadFromStream(ostream);
					}
					Debug.Log("Send 8 ");
					
					client.Close ();
					switch (response.status) {
					case 307:
					case 302:
					case 301:
						uri = new Uri (response.GetHeader ("Location"));
						continue;
					default:
						retry = maximumRetryCount;
						break;
					}
					Debug.Log("Send 9 ");
					
				}
				Debug.Log("Send 10 ");
				
				if (useCache) {
					string etag = response.GetHeader ("etag");
					if (etag.Length > 0)
						etags[uri.AbsoluteUri] = etag;
				}
				Debug.Log("Send 11 ");
				
			} catch (Exception e) {
				Debug.Log("Send 12 ");
				
				Debug.LogException(e);
				Debug.LogError(e.Message);
				exception = e;
				response = null;
			}
			Debug.Log("Send 13 ");
			
			state = RequestState.Done;
			isDone = true;
		}

		public string Text {
			set { bytes = System.Text.Encoding.UTF8.GetBytes (value); }
		}


		public static bool ValidateServerCertificate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			Debug.LogWarning ("SSL Cert Error:" + sslPolicyErrors.ToString ());
			return true;
		}

		void WriteToStream (Stream outputStream)
		{
			var stream = new BinaryWriter (outputStream);
			stream.Write (ASCIIEncoding.ASCII.GetBytes (method.ToUpper () + " " + uri.PathAndQuery + " " + protocol));
			stream.Write (EOL);
			foreach (string name in headers.Keys) {
				foreach (string value in headers[name]) {
					stream.Write (ASCIIEncoding.ASCII.GetBytes (name));
					stream.Write (':');
					stream.Write (ASCIIEncoding.ASCII.GetBytes (value));
					stream.Write (EOL);
				}
			}
			if (bytes != null && bytes.Length > 0) {
				if (GetHeader ("Content-Length") == "") {
					stream.Write (ASCIIEncoding.ASCII.GetBytes ("content-length: " + bytes.Length.ToString ()));
					stream.Write (EOL);
					stream.Write (EOL);
				}
				stream.Write (bytes);
			} else {
				stream.Write (EOL);
			}
		}
		
	}
	
}

