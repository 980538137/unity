using Framework.OSAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.SocketMsgHandle
{
    /// <summary>
    /// 服务器命令基类
    /// </summary>
    public abstract class CmdServer : CmdBase
    {
        /// <summary>
        /// 保存协议消息头部
        /// </summary>
        public MsgHead Head { get; set; }

        // 保存请求消息
        private Byte[] mReqestMsg;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">命令的名字</param>
        /// <param name="msgid">命令码id</param>
        public CmdServer(String name, UInt32 msgid)
            : base(name, msgid)
        {

        }

        public void Handle(Byte [] msg, Int32 len)
        {
            mReqestMsg = msg;

	        // 获取请求消息
            if (GetReq(msg, len))
	        {
		        // 进行逻辑处理
                LogicDo();
	        }
        }

        /// <summary>
        /// 获取请求消息，主要是反序列化消息
        /// </summary>
        /// <param name="msg">网络消息</param>
        /// <param name="len">网络消息长度</param>
        /// <returns></returns>
        protected abstract bool GetReq(Byte[] msg, Int32 len);

        /// <summary>
        /// 进行服务器命令的逻辑处理
        /// </summary>
        protected abstract void LogicDo();
    }
}
