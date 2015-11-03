using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;

    public static class UtilNet
    {
        // 消息序列号
        private static UInt32 mMsgSeq = 1;
        private static object mMsgSeqSync = new object();

        /// <summary>
        /// 将结构体(C#中的struct是值类型)转换为Byte数组
        /// </summary>
        /// <param name="structObj">托管的结构体</param>
        /// <returns>byte数组</returns>
        public static byte[] StructToBytes<T>(T structObj) where T : struct
        {
            // 获得结构体大小
            int size = Marshal.SizeOf(structObj);
            // 保存结构的比特数组
            byte[] bytes = new byte[size];

            // 通过使用指定的字节数，从进程的非托管内存中分配内存
            System.IntPtr ptr = Marshal.AllocHGlobal(size);

            // 将数据从托管对象封送到非托管内存块
            Marshal.StructureToPtr(structObj, ptr, true);
            // 将结构体中的数据复制到比特数组中
            Marshal.Copy(ptr, bytes, 0, size);

            // 释放内存
            Marshal.FreeHGlobal(ptr);

            return bytes;
        }

        /// <summary>
        /// 将byte数组转换为结构体(C#中的struct是值类型)
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="type">结构体的类型</param>
        /// <returns>返回结构体</returns>
        public static T BytesToStruct<T>(byte[] bytes) where T : struct
        {
            // 结构的大小
            int size = Marshal.SizeOf(typeof(T));
            if (size > bytes.Length)
            {
                throw new Exception(String.Format("byte数组的长度[{0}]太短", bytes.Length));
            }

            // 通过使用指定的字节数，从进程的非托管内存中分配内存
            System.IntPtr ptr = Marshal.AllocHGlobal(size);

            // 将数组复制到内存中
            Marshal.Copy(bytes, 0, ptr, size);
            //从内存中创建结构
            T data = (T)(Marshal.PtrToStructure(ptr, typeof(T)));

            //释放内存
            Marshal.FreeHGlobal(ptr);

            return data;
        }

        /// <summary>
        /// 序列化网络消息
        /// </summary>
        /// <typeparam name="T">消息的类型</typeparam>
        /// <param name="t">要序列化的消息</param>
        /// <returns>字节数组</returns>
        public static byte[] SerializeNetMsg<T>(T t) where T: class
        {
            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize<T>(memStream, t);
					return memStream.ToArray();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 反序列化网络消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="msg">字节数组</param>
        /// <returns>消息类型</returns>
        public static T DeserializeNetMsg<T>(Byte[] msg) where T : class
        {
            try
            {
                using (MemoryStream recvStream = new MemoryStream(msg))
                {
                    return ProtoBuf.Serializer.Deserialize<T>(recvStream);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 将指针IntPtr转换为结构体
        /// </summary>
        /// <param name="structObj">托管的结构体</param>
        /// <returns>byte数组</returns>
        public static T IntPtrToStruct<T>(IntPtr ptr) where T : struct
        {
            T t = (T)(Marshal.PtrToStructure(ptr, typeof(T)));
            //释放内存
            Marshal.FreeHGlobal(ptr);
            return t;
        }

        /// <summary>
        /// 产生请求消息序列号
        /// </summary>
        /// <returns>消息序列号</returns>
        public static UInt32 GenerateMsgSeq()
        {
            lock (mMsgSeqSync)
            {
                return mMsgSeq++;
            }
        }

        /// <summary>
        /// 打印byte数组中的内容
        /// </summary>
        public static void PrintMsg(Byte [] buf, String info)
        {
            if (null == buf)
            {
                return;
            }

            StringBuilder str = new StringBuilder();
            str.AppendFormat("{0}[消息长度({1})]: ", info, buf.Length);

            for (int i = 0; i < buf.Length; i++)
            {
                str.AppendFormat("{0} ", buf[i].ToString("x"));
            }

        }

        /// <summary>
        /// 检查IP地址的有效性
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns>是否有效</returns>
        public static bool CheckIP(String ip)
        {
            IPAddress temp = null;
            return IPAddress.TryParse(ip, out temp);
        }

        /// <summary>
        /// 检查端口号
        /// </summary>
        /// <param name="port">端口号</param>
        /// <returns>是否有效</returns>
        public static bool CheckPort(UInt16 port)
        {
            return (port > 0 && port <= 65535);
        }
    }
