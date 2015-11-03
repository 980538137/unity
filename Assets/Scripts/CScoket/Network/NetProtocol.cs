using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Framework.OSAbstract
{
    static class ProtocolConstant
    {
        /// <summary>
        /// 前同步码
        /// </summary>
        public const UInt16 Premable = 5613;

        /// <summary>
        /// 最大的网络消息长度
        /// </summary>
        public const UInt32 MaxNetMsgLen = 65535;

        /// <summary>
        /// 发送心跳消息的周期
        /// </summary>
        public const Double HeartBeatPeriod = 1000 * 30;
    }

    /// <summary>
    /// 消息来源
    /// </summary>
    public enum MsgOrigin : ushort
    {
        // 消息来源于客户端
        MSG_ORIGIN_MSG_CLIENT = 1,

        // 消息来源于服务器
        MSG_ORIGIN_MSG_SERVER = 2,

        // 心跳消息
        MSG_ORIGIN_MSG_HEARTBEAT = 3
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MsgHead
    {
        /// <summary>
        /// 此协议消息的id
        /// </summary>
        public UInt32 msgid
        {
            get;
            private set;
        }

        /// <summary>
        /// 此协议消息的长度
        /// </summary>
        public UInt32 msglen
        {
            get; 
            private set; 
        }

        /// <summary>
        /// 消息序列号
        /// </summary>
        public UInt32 seqid
        {
            get;
            private set;
        }

        public void SetMsgHead(UInt32 msgid, UInt32 msglen, UInt32 seqid)
        {
            this.msgid  = msgid;
            this.msglen = msglen;
            this.seqid  = seqid;
        }
    }
}
