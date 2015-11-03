using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.OSAbstract;

namespace Framework
{
    public class ModelToControlQueue : Singleton<ModelToControlQueue>
    {
        ApplicationQueue<CMsgBase> mQueue;

        private ModelToControlQueue()
        {
            mQueue = new ApplicationQueue<CMsgBase>("ModelToControlQueue");
        }

        /// <summary>
        /// 添加网络消息
        /// </summary>
        /// <param name="msg">网络消息</param>
        public void AddNetMsg(CMsgBase msg)
        {
            mQueue.AddMsg(msg);
        }

        /// <summary>
        /// 获取网络消息
        /// </summary>
        /// <returns>网络消息，没有网络消息的话，返回null</returns>
        public CMsgBase GetNetMsg()
        {
            return mQueue.GetMsg();
        }
    }
}
