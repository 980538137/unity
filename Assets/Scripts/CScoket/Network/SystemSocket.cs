using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.OSAbstract;
using com.ourgame.texasSlots;
using UnityEngine;


namespace Assets.Scripts.Network{
    class SystemSocket : Framework.Singleton<SystemSocket>
    {
        /// <summary>
        /// TCP客户端
        /// </summary>
        public NetTcpClient TcpClient { get; private set; }

        private SystemSocket()
        {
            
        }

        /// <summary>
        /// 开始连接服务器
        /// </summary>
        public void Start(string IP,int Port)
        {

//            ProtocolDelegate pd = new ProtocolDelegate();
            TcpClient = new NetTcpClient();
//
//            LoginServerData serverData = ConfigManager.Instance.getServerAddress();

//            if (null != serverData)
//            {
				TcpClient.CreatSocket(IP, (UInt16)Port);
				bool sok=Security.PrefetchSocketPolicy(IP,843 );
				Debug.Log ("   安全沙箱  "+sok.ToString());
                TcpClient.Connect();
//            }
        }

        /// <summary>
        /// 开始重连服务器
        /// </summary>
        public void Reconnect()
        {
            TcpClient.ReConnect();
        }

		/// <summary>
		/// 断开连接服务器
		/// </summary>
		public void ConClose()
		{
			TcpClient.CloseConnect ();
		}
        
    }
}
