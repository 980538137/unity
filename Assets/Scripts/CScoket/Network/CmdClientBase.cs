using Framework.OSAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.SocketMsgHandle
{
    /// <summary>
    /// 客户端命令基类
    /// </summary>
    public abstract class CmdClientBase : CmdBase
    {
        // 响应超时检测定时器
     //   private CmdTimer mTimer = null;

        // 是否启动定时器
        private bool mIsStartTimer = true;

        // 消息序列号
        protected UInt32 Cseq { get; private set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">客户端命令的名字</param>
        /// <param name="rspmsgid">请求所对应的响应消息的消息id</param>
        protected CmdClientBase(String name, UInt32 rspmsgid, bool isstarttimer = true)
            : base(name, rspmsgid)
        {
            mIsStartTimer = isstarttimer;
            Cseq = UtilNet.GenerateMsgSeq();

                CmdClientManager.Instance.AddCmdClient(Cseq, this);
//            }
        }

        /// <summary>
        /// 发送网络消息
        /// </summary>
        public void SendMessage()
        {
//            if (mIsStartTimer)
//            {
//                // 启动定时器
//                mTimer.StartTimer();
//            }

            DoSendMsg();
        }

        /// <summary>
        /// 处理接收到的响应消息
        /// </summary>
        /// <param name="msg">响应消息</param>
        /// <param name="msglen">响应消息长度</param>
        /// <param name="status">接收状态</param>
        public void HandleResponse(Byte[] msg,
                                   Int32 msglen,
                                   CmdRecvPackageStatus status)
        {
//            if (mIsStartTimer)
//            {
//                // 停止定时器
//                mTimer.StopTimer();
//
//                // 从客户端命令管理容器中删除此对象
//                CmdClientManager.Instance.RemoveCmdClient(Cseq);
//            }

            // 没有收到响应消息
            if (CmdRecvPackageStatus.TIMEOUT == status)
            {
                // 超时处理
                OnTimeOut();
            }
            // 接收到正确的响应消息
            else if (CmdRecvPackageStatus.SUCCESSRESPOND == status)
            {
                // 正确响应的处理
                OnSuccessRespond(msg);
            }
            // 接收到错误的响应消息
            else if (CmdRecvPackageStatus.FAILRESPOND == status)
            {
                // 错误响应的处理
                OnFailRespond(msg);
            }
            else
            {
               // CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("CmdRecvPackageStatus[{0}]错误", status.ToString()));
            }
        }

        /// <summary>
        /// 具有要实现的发送消息的逻辑
        /// </summary>
        protected abstract void DoSendMsg();

        /// <summary>
        /// 超时处理
        /// </summary>
	    protected abstract void OnTimeOut();
        
        /// <summary>
        /// 成功响应处理
        /// </summary>
        /// <param name="msg">响应消息</param>
	    protected abstract void OnSuccessRespond(Byte [] msg);
        
        /// <summary>
        /// 失败响应处理
        /// </summary>
        /// <param name="msg">响应消息</param>
        protected abstract void OnFailRespond(Byte [] msg);
    }
}
