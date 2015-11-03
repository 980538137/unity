using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.SocketMsgHandle
{
    /// <summary>
    /// 命令的基类
    /// </summary>
    public abstract class CmdBase
    {
        /// <summary>
        /// 命令接收包状态
        /// </summary>
        public enum CmdRecvPackageStatus
        {
            // 客户端接收到正确的响应消息
            SUCCESSRESPOND,

            // 客户端接收到错误的响应消息
            FAILRESPOND,

            // 客户端没有收到响应消息，超时
            TIMEOUT
        }

        /// <summary>
        /// 此命令的名字
        /// </summary>
        public String CmdName { get; private set; }

        /// <summary>
        /// 请求所对应的响应消息的消息ID
        /// </summary>
        public UInt32 RspMsgID { get; private set; }

        protected CmdBase(String cmdname, UInt32 rspmsgid)
        {
            CmdName = cmdname;
            RspMsgID = rspmsgid;
        }
    }
}
