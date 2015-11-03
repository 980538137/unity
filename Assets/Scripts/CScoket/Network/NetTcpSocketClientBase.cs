using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Framework;
using UnityEngine;
using Assets.Scripts.Manager.Common;

namespace Framework.OSAbstract
{
    public abstract class NetTcpSocketClientBase : NetSocket
    {
        // 管理发送缓存区队列
        private NetSendBufferQueue mSendBufferQueue = new NetSendBufferQueue();
		private bool One=false;
        protected NetTcpSocketClientBase()
        {
        }
        /// <summary>
        /// 从套接字的接收网络消息
        /// </summary>
        protected void RecvMsg(Socket sock)
        {
            TcpSocketProcArg arg = new TcpSocketProcArg();
            // 使用new的方式将此buffer一路传递给应用层
            arg.Buf = new Byte[3*1024];
            arg.Sock = sock;
			arg.BufLen =3*1024;
            arg.Stage = SocketRecvMsgStage.RECV_BODY;

            try
            {
				One=true;
				arg.Sock.BeginReceive(arg.Buf, 0, arg.Buf.Length, SocketFlags.None, TcpRecvMsgEndCallBack, arg);
//				arg.ReceiveDone.WaitOne(1000); // 5秒超时
            }
            catch (SocketException se)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("错误码: {0}, 描述: {1}", se.SocketErrorCode, se.Message));
            }
            catch (Exception e)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, e.ToString());
            }
        }

        /// <summary>
        /// 发送TCP消息
        /// </summary>
        /// <param name="sendbuf">发送缓存区</param>
        /// <param name="msglen">要发送的数据长度</param>
        /// /// <param name="msgid">消息id</param>
        public void SendMsg(byte[] sendbuf, Int32 msglen, UInt32 msgid, UInt32 cseq)
        {
            if (null == sendbuf)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, string.Format("sendbuf is null"));
                return;
            }

            if (msglen < 0
                || msglen > ProtocolConstant.MaxNetMsgLen)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, string.Format("消息长度错误, 消息长度为[{0}]", msglen));
                return;
            }
			byte [] headbuf1 = BitConverter.GetBytes (msgid);
			byte [] headbuf2 = BitConverter.GetBytes (msglen);
			byte [] headbuf3 = BitConverter.GetBytes (cseq);
			byte[] date=new byte[sendbuf.Length+12];
			Buffer.BlockCopy(headbuf1, 0, date, 0, 4);
			Buffer.BlockCopy(headbuf2, 0, date, 4,4);
			Buffer.BlockCopy(headbuf3, 0, date, 8,4);
			if (msgid == 12345) {
				Send(date, date.Length);
			} else {
				Buffer.BlockCopy(sendbuf, 0, date, 12,sendbuf.Length);
				Send(date, date.Length);
			}
            // 添加头部信息



        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sendbuf">发送缓存区</param>
        /// <param name="msglen">要发送的消息长度</param>
        protected void Send(byte[] sendbuf, Int32 msglen)
        {
            try
            {
                // 检查上一个数据是否发送完毕，如果没有的话将buffer放入发送队列中排队
                if (false == mSendBufferQueue.AddSocketBuffer(sendbuf, msglen))
                {
                    return;
                }

                TcpSocketProcArg arg = new TcpSocketProcArg();
                arg.Buf = sendbuf;
                arg.Sock = Sock;
                arg.BufLen = msglen;

                Sock.BeginSend(arg.Buf, 0, arg.BufLen, SocketFlags.None, SendMsgEndCallBack, arg);
            }
            catch (SocketException se)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("错误码: {0}, 描述: {1}", se.SocketErrorCode, se.Message));
            }
            catch (Exception e)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, e.ToString());
            }
        }
        /// <summary>
        /// 异步处理方法
        /// </summary>
        /// <param name="ar">异步操作结果</param>
        private void TcpRecvMsgEndCallBack(IAsyncResult ar)
        {
            // 获取客户端的套接字
            TcpSocketProcArg client = (TcpSocketProcArg)ar.AsyncState;

            try
            {
				Int32 msglen=0;
				Int32 info=0;
				Int32 recvlen = client.Sock.EndReceive(ar);
				string inf=null;
				byte[] date=new byte[recvlen];
				if(recvlen>0){
					client.Stream.Write(client.Buf, 0, recvlen);
					Buffer.BlockCopy(client.Buf,0,date,0,recvlen);
					byte[] head2 = new byte[4];
					byte[] head1 = new byte[4];
					Buffer.BlockCopy (client.Buf, 0, head1, 0, 4);
					Buffer.BlockCopy (client.Buf, 4, head2, 0, 4);
					msglen = BitConverter.ToInt32 (head2, 0);
					info=BitConverter.ToInt32 (head1, 0);
					inf=info.ToString().Substring(0,6);
				}
				if(client.Sock.Poll(0, SelectMode.SelectRead))
				{
					if(recvlen==0){
						PlayerInfoManager.Instance.LoginSuccess=false;
					}
				}
				if (recvlen < msglen)
                {
					client.Sock.BeginReceive(client.Buf, 0, client.BufLen, SocketFlags.None, TcpRecvMsgEndCallBack, client);
                    return;
                }
              	else
                {
					if(inf!="134217"&&recvlen>0)
					{
						client.Sock.BeginReceive(client.Buf, 0, client.BufLen, SocketFlags.None, TcpRecvMsgEndCallBack, client);
					}
					else{
						HandleMsgBody(client);
					}
                    return;
                }
            }
            catch (SocketException se)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("错误码: {0}, 描述: {1}", se.SocketErrorCode, se.Message));

                if (SocketError.WouldBlock == se.SocketErrorCode
                    || SocketError.Interrupted == se.SocketErrorCode)
                {
                    return;
                }
				client.ReceiveDone.Set();

//                CloseSocket();
            }
            catch (Exception e)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, e.ToString());
//                CloseSocket();
            }
        }
        /// <summary>
        /// 处理消息头部
        /// </summary>
        /// <param name="client">socket参数</param>
        private void HandleMsgHead(TcpSocketProcArg client)
        {
            // 打印头部消息
            //UtilNet.PrintMsg(client.Buf, "接收到Head");

            // 获取头部信息
            MsgHead head = UtilNet.BytesToStruct<MsgHead>(client.Buf);

            // 完整接收到了头部信息，开始验证消息
//            if (false == CheckMsgValidity(ref head))
//            {
//                // 断开连接
//                CloseSocket("协议头部信息无效，客户端断开连接");
//
//                return;
//            }

            // 保存消息头部, C#中的struct为值类型
            client.Head = head;
            // 消息体的长度信息
            client.BufLen = (Int32)(head.msglen);
            // 接收数据
            client.Stage = SocketRecvMsgStage.RECV_BODY;

            // 开始接收数据载荷
            client.Sock.BeginReceive(client.Buf, 0, client.BufLen, SocketFlags.None, TcpRecvMsgEndCallBack, client);
        }

        /// <summary>
        /// 处理消息体
        /// </summary>
        /// <param name="client"></param>
        private void HandleMsgBody(TcpSocketProcArg client)
        {
			byte[] byteRead = client.Stream.ToArray ();
			byte[] head1 = new byte[4];
			byte[] head2 = new byte[4];
			byte[] head3 = new byte[4];
			//TODO: 向应用层投递消息
			Buffer.BlockCopy (byteRead, 0, head1, 0, 4);
			Int32 msgnumber = BitConverter.ToInt32 (head1, 0);
			Buffer.BlockCopy (byteRead, 4, head2, 0, 4);
			Int32 msglen = BitConverter.ToInt32 (head2, 0);
			Buffer.BlockCopy (byteRead, 8, head3, 0, 4);
			Int32 reqid = BitConverter.ToInt32 (head3, 0);
			Byte [] msgbuf = new Byte[msglen]; 
			Buffer.BlockCopy (byteRead, 12, msgbuf, 0, msglen);
			if(msglen>2000)
			{
				Debug.LogError (msgbuf.Length.ToString()+"   aaaaa");
			}
			NetRecvMsg recvmsg = new NetRecvMsg (msgbuf, msglen, msgnumber, reqid);
			ModelToControlQueue.Instance.AddNetMsg (recvmsg);
			
            // 重新接收头部，进行循环接收消息
            RecvMsg(client.Sock);

            client = null;
        }

        /// <summary>
        /// 发送消息接收回调
        /// </summary>
        /// <param name="ar">异步操作结果</param>
        private void SendMsgEndCallBack(IAsyncResult ar)
        {
            try
            {
                TcpSocketProcArg arg = (TcpSocketProcArg)ar.AsyncState;

                // 本次所发送的数据长度
                Int32 sendnum = arg.Sock.EndSend(ar);

                // 没有发完，继续发送
                if (sendnum < arg.BufLen)
                {
                    arg.BufLen = arg.BufLen - sendnum;
                    arg.Sock.BeginSend(arg.Buf, sendnum, arg.BufLen, SocketFlags.None, SendMsgEndCallBack, arg);

                    return;
                }

                // 发送完数据后进行打印
                UtilNet.PrintMsg(arg.Buf, "发送消息");

                NetSendBufferQueue.Buffer buf = null;
                // 数据发送完毕, 获取新的数据继续发送
                buf = mSendBufferQueue.GetBuffer();
                // 没有需要发送的数据了
                if (null == buf)
                {
                    return;
                }

                // 队列中有数据需要发送
                arg.Buf = buf.buf;
                arg.BufLen = buf.buflen;
                arg.Sock.BeginSend(arg.Buf, 0, arg.BufLen, SocketFlags.None, SendMsgEndCallBack, arg);
            }
            catch (SocketException se)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("错误码: {0}, 描述: {1}", se.SocketErrorCode, se.Message));

                if (SocketError.WouldBlock == se.SocketErrorCode
                    || SocketError.Interrupted == se.SocketErrorCode)
                {
                    return;
                }

//                CloseSocket();
            }
            catch (Exception e)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, e.ToString());
//                CloseSocket();
            }
        }
    }
}
