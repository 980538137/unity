using Framework.OSAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.SocketMsgHandle
{
    /// <summary>
    /// 用于通过协议中的序列号信息来查找客户端命令
    /// 只有客户端命令才需要去查找，因为发送了请求
    /// 消息以后，这个对象的地址就找不到了
    /// </summary>
    public class CmdClientManager : Framework.Singleton<CmdClientManager>
    {
        /// <summary>
        /// 客户端命令的集合
        /// </summary>
        private Dictionary<UInt32, CmdClientBase> mCmdClients;

        private CmdClientManager()
        {
            mCmdClients = new Dictionary<UInt32, CmdClientBase>();
        }

        /// <summary>
        /// 添加客户端命令
        /// </summary>
        /// <param name="Cseq">消息序列号</param>
        /// <param name="client">客户端命令</param>
        public void AddCmdClient(UInt32 Cseq, CmdClientBase client)
        {
            // 检查消息序列号是否出现了重复
            if (mCmdClients.ContainsKey(Cseq))
            {
               // CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("Cseq[{0}]重复", Cseq));
                return;
            }

            mCmdClients.Add(Cseq, client);
        }

        /// <summary>
        /// 删除客户端命令
        /// </summary>
        /// <param name="Cseq">消息序列号</param>
        public void RemoveCmdClient(UInt32 Cseq)
        {
            mCmdClients.Remove(Cseq);
        }

        /// <summary>
        /// 通过响应中的序列号来查找我们所需要的客户端命令
        /// 这里也只能找到客户端命令
        /// </summary>
        /// <param name="cseq">消息中的序列号</param>
        /// <param name="msgid">消息id</param>
        /// <param name="errinfo">错误信息提示</param>
        /// <returns>客户端命令</returns>
        public CmdClientBase FindClientCmd(UInt32 cseq, UInt32 msgid, out String errinfo)
        {
            // 没有这样的消息序列号
            if (false == mCmdClients.ContainsKey(cseq))
            {
                errinfo = String.Format("响应消息错误，没有找到这样的Cseq[{0}]", cseq);
                return null;
            }

            // 检查消息id是否相等
            if (msgid != mCmdClients[cseq].RspMsgID)
            {
                errinfo = String.Format("响应消息错误，msgid错误，Cseq[{0}], MsgID[接收值({1}):保存值({2})]", cseq, msgid, mCmdClients[cseq].RspMsgID);
                return null;
            }

            errinfo = null;
            return mCmdClients[cseq];
        }
    }
}
