using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.OSAbstract;

    public enum ModelToControlQueueMsg
    {
        /// <summary>
        /// TCP连接超时
        /// </summary>
        TCP_CONNECT_FAILURE,

        /// <summary>
        /// TCP连接超时
        /// </summary>
        TCP_CONNECT_TIMEOUT,

        /// <summary>
        /// 成功连接到TCP服务器
        /// </summary>
        TCP_CONNECT_SUCCESS,

        /// <summary>
        /// TCP连接已经断开
        /// </summary>
        TCP_DISCONNECT,

        /// <summary>
        /// 接收到网络消息
        /// </summary>
        RECV_MESSAGE,

        /// <summary>
        /// 接收响应超时
        /// </summary>
        RESPONSE_TIMEOUT
    }

    /// <summary>
    /// 建立TCP连接失败
    /// </summary>
    class NetTcpConnectFailMsg : CMsgBase
    {
        public String ErrInfo { get; private set; }

        public NetTcpConnectFailMsg(String errinfo)
            : base((int)CMsgMainType.USER, (int)ModelToControlQueueMsg.TCP_CONNECT_FAILURE)
        {
            ErrInfo = errinfo;
        }
    }

    /// <summary>
    /// TCP连接超时
    /// </summary>
    class NetTcpConnectTimeoutMsg : CMsgBase
    {
        public NetTcpConnectTimeoutMsg()
            : base((int)CMsgMainType.USER, (int)ModelToControlQueueMsg.TCP_CONNECT_TIMEOUT)
        {
        }
    }

    /// <summary>
    /// 成功连接到远端服务器
    /// </summary>
    class NetTcpConnectSuccessMsg : CMsgBase
    {
        public NetTcpConnectSuccessMsg()
            : base((int)CMsgMainType.USER, (int)ModelToControlQueueMsg.TCP_CONNECT_SUCCESS)
        {
        }
    }

    /// <summary>
    /// TCP连接断开
    /// </summary>
    class NetTcpDisconnectMsg : CMsgBase
    {
        public String ErrInfo { get; private set; }

        public NetTcpDisconnectMsg(String errinfo)
            : base((int)CMsgMainType.USER, (int)ModelToControlQueueMsg.TCP_DISCONNECT)
        {
            ErrInfo = errinfo;
        }
    }

    /// <summary>
    /// 接收到网络消息
    /// </summary>
    class NetRecvMsg : CMsgBase
    {
        /// <summary>
        /// 网络消息
        /// </summary>
        public Byte [] NetMsg { get; private set; }

        /// <summary>
        /// 网络消息的长度
        /// </summary>
        public Int32  NetMsgLen { get; private set; }

        /// <summary>
        /// 协议头部信息
        /// </summary>
        public MsgHead Head { get; private set; }
		/// <summary>
		/// 协议ID
		/// </summary>
		public Int32 Msgnumber{ get; private set;}
		/// <summary>
		/// 消息ID
		/// </summary>
		public Int32 reqId{ get; private set;}
	 
	/// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="msglen"></param>
	public NetRecvMsg(Byte [] buf, Int32 msglen, Int32 msgnumber,Int32 reqid)
		: base((int)CMsgMainType.USER, (int)ModelToControlQueueMsg.RECV_MESSAGE)
        {
            NetMsg = buf;
            NetMsgLen = msglen;
			Msgnumber = msgnumber;
			reqId = reqid;
		}
    }

    /// <summary>
    /// 定时器检测到接收响应超时
    /// </summary>
//    class NetResponseRecvTimeout : CMsgBase
//    {
//        /// <summary>
//        /// 检测到超时的客户端命令
//        /// </summary>
//        public CmdClientBase Cmd { get; private set; }
//
//        /// <summary>
//        /// 超时定时器的名字
//        /// </summary>
//        public String TimerName { get; private set; }
//
//        public NetResponseRecvTimeout(CmdClientBase cmd, String timername)
//            : base((int)CMsgMainType.USER, (int)ModelToControlQueueMsg.RESPONSE_TIMEOUT)
//        {
//            Cmd = cmd;
//            TimerName = timername;
//        }
  //  }
