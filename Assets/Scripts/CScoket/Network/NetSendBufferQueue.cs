using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Framework.OSAbstract
{
    /// <summary>
    /// 保存应用层投递下来的发送缓存区
    /// </summary>
    class NetSendBufferQueue
    {
        /// <summary>
        /// 发送缓存区
        /// </summary>
        public class Buffer
        {
            // 发送缓存区
            public byte[] buf { get; set; }

            // 缓存区长度
            public Int32 buflen { get; set; }

            // 远端端点地址
            public IPEndPoint remoteendpoint { get; set; }
        }

        // 发送缓存区队列
        private Queue<Buffer> mSendBfferQueue = new Queue<Buffer>();
        private Object mSendBfferQueueSync = new Object();

        public NetSendBufferQueue()
        {

        }

        /// <summary>
        /// 添加套接字发送消息
        /// </summary>
        /// <param name="buf">要发送的网络消息</param>
        /// <param name="buflen">消息长度</param>
        /// <param name="remoteendpoint">要发送到的远端地址</param>
        /// <returns>是否应该立即发送</returns>
        public bool AddSocketBuffer(byte[] buf, Int32 buflen, IPEndPoint remoteendpoint = null)
        {
            // 检查上一个数据是否发送完毕，如果有的话将buffer放入发送队列中排队
            lock (mSendBfferQueueSync)
            {
                if (false == Empty())
                {
                    AddBuffer(buf, buflen, remoteendpoint);
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 获取要发送的数据
        /// </summary>
        /// <returns>缓存区信息</returns>
        public Buffer GetBuffer()
        {
            lock (mSendBfferQueueSync)
            {
                // 没有数据的情况
                if (0 == mSendBfferQueue.Count)
                {
                    return null;
                }

                // 出队并且删除引用
                return mSendBfferQueue.Dequeue();
            }
        }

        /// <summary>
        /// 添加发送缓存区信息
        /// </summary>
        /// <param name="buf">发送缓存区</param>
        /// <param name="buflen">缓存区大小</param>
        /// <param name="remoteendpoint">远端端点地址</param>
        private void AddBuffer(byte[] buf, Int32 buflen, IPEndPoint remoteendpoint)
        {
            Buffer b = new Buffer();
            b.buf = buf;
            b.buflen = buflen;
            b.remoteendpoint = remoteendpoint;

            mSendBfferQueue.Enqueue(b);
        }

        /// <summary>
        /// 集合是否为空
        /// </summary>
        /// <returns></returns>
        private bool Empty()
        {
            return (0 == mSendBfferQueue.Count);
        }
    }
}
