using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Framework.OSAbstract;

namespace Framework
{
    public class ApplicationQueue<T> where T : class
    {
        // 队列
        private Queue<T> mQueue = new Queue<T>();
        private object mQueueSync = new object();

        // 队列的长度限制
        private Int32 mQueueLen;

        // 队列的名字
        String mQueueName;

        public ApplicationQueue()
            : this(FrameworkConstant.DefaultQueueLen, "Default")
        {

        }

        public ApplicationQueue(String name)
            : this(FrameworkConstant.DefaultQueueLen, name)
        {

        }

        public ApplicationQueue(Int32 queueLen, String name)
        {
            mQueueLen = queueLen;
            mQueueName = name;
        }

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="msg">线程消息</param>
        public void AddMsg(T msg)
        {
            // 判断线程队列的容量是否已经满了
            lock (mQueueSync)
            {
                if (mQueue.Count() > mQueueLen)
                {
                    //CSysLog.Instance.Write(SysLogLevel.EMERG, String.Format("{0}队列满", mQueueName));

                    return;
                }

                // 添加到消息队列中
                mQueue.Enqueue(msg);
            }
        }

        /// <summary>
        /// 获取队列顶部的线程消息
        /// </summary>
        /// <returns>线程消息</returns>
        public T GetMsg()
        {
            lock (mQueueSync)
            {
                // 空队列的情况
                if (0 == mQueue.Count)
                {
                    return null;
                }

                return mQueue.Dequeue();
            }
        }
    }
}
