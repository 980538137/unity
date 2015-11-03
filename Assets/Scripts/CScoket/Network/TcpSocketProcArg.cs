using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Framework.OSAbstract
{
    /// <summary>
    /// 接收网络消息的流程
    /// </summary>
    enum SocketRecvMsgStage
    {
        /// <summary>
        /// 接收消息头部
        /// </summary>
        RECV_HEAD,

        /// <summary>
        /// 接收消息体
        /// </summary>
        RECV_BODY
    }

    /// <summary>
    /// 异步TCP socket信息
    /// </summary>
    internal class TcpSocketProcArg
    {
        /// <summary>
        /// 套接字
        /// </summary>
        public Socket Sock
        {
            get;
            set;
        }

        /// <summary>
        /// 接收/发送缓存区
        /// </summary>
        public Byte[] Buf
        {
            get;
            set;
        }

        /// <summary>
        ///  临时数据长度变量，用于检测数据是否都接收到了
        /// </summary>
        public Int32 BufLen
        {
            get;
            set;
        }

        /// <summary>
        ///  接收消息的阶段
        /// </summary>
        public SocketRecvMsgStage Stage
        {
            get;
            set;
        }

        /// <summary>
        /// 消息头部
        /// </summary>
        public MsgHead Head
        {
            get;
            set;
        }
		public System.IO.MemoryStream Stream = new System.IO.MemoryStream();

		public System.Threading.ManualResetEvent ReceiveDone = new System.Threading.ManualResetEvent(false);
    }

    /// <summary>
    /// 异步UDP socket信息
    /// </summary>
    internal class UdpSocketProcArg
    {
        /// <summary>
        /// 套接字
        /// </summary>
        public Socket Sock
        {
            get;
            set;
        }

        /// <summary>
        /// 接收/发送缓存区
        /// </summary>
        public Byte[] Buf
        {
            get;
            set;
        }

        /// <summary>
        ///  临时数据长度变量，用于检测数据是否都接收到了
        /// </summary>
        public Int32 BufLen
        {
            get;
            set;
        }

        /// <summary>
        /// 远端的端点地址
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; set; }
    }
}
