/********************************************************************
	created:	2007/10/21
	created:	21:10:2007   11:50
	file base:	abstrace NetFactory
	file ext:	cs
	author:		liyi
	
	purpose:	socket基类

    protobuf-net使用方法(效率方面和C++是差不多的):
  
    // 序列化代码
    Person.Person p = new Person.Person();
    p.name = "liyi";
    MemoryStream memStream = new MemoryStream();
    ProtoBuf.Serializer.Serialize<Person.Person>(memStream, p);
    byte[] x = memStream.ToArray();
    
    // 反序列化代码，这里不会发生内存的拷贝，只是指针的赋值
    MemoryStream m = new MemoryStream(x);
    Person.Person p1 = ProtoBuf.Serializer.Deserialize<Person.Person>(m);
    print(p1.name);
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Linq;

namespace Framework.OSAbstract
{
    public abstract class NetSocket
    {
        #region 属性

        /// <summary>
        /// 套接字对象
        /// </summary>
        public Socket Sock
        {
            get;
            protected set;
        }

        /// <summary>
        /// 接收缓存区
        /// </summary>
        public Int32 RecvBufSize
        {
            get;
            protected set;
        }

        /// <summary>
        /// 发送缓存区
        /// </summary>
        public Int32 SendBufSize
        {
            get;
            protected set;
        }

        /// <summary>
        /// 套接字是否阻塞
        /// </summary>
        public bool IsBlock
        {
            get;
            protected set;
        }

        private String mLocalIp;
        /// <summary>
        /// 本地IP
        /// </summary>
        public String LocalIP
        {
            get { return mLocalIp; }
            set
            {
                IPAddress ip = null;
                if (true == IPAddress.TryParse(value, out ip))
                {
                    mLocalIp = value;
                }
                else
                {
                    throw new Exception("无效的本地IP地址: " + value);
                }
            }
        }

        private UInt16 mLocalPort;
        /// <summary>
        /// 本地端口号
        /// </summary>
        public UInt16 LocalPort
        {
            get { return mLocalPort; }
            set
            {
                if (value > 0 && value <= 65535)
                {
                    mLocalPort = value;
                }
                else
                {
                    throw new Exception("无效的本地端口号: " + value);
                }
            }
        }

        private String mRemoteSeverIp;
        /// <summary>
        /// 远程服务器的IP
        /// </summary>
        public String RemoteServerIP
        {
            get { return mRemoteSeverIp; }
            set
            {
                IPAddress ip = null;
                if (true == IPAddress.TryParse(value, out ip))
                {
                    mRemoteSeverIp = value;
                }
                else
                {
                    throw new Exception("无效的远程IP地址: " + value);
                }
            }
        }

        private UInt16 mRemoteServerPort;
        /// <summary>
        /// 远程服务器的端口号
        /// </summary>
        public UInt16 RemoteServerPort
        {
            get { return mRemoteServerPort; }
            set
            {
                if (value > 0 && value <= 65535)
                {
                    mRemoteServerPort = value;
                }
                else
                {
                    throw new Exception("无效的远程端口号: " + value);
                }
            }
        }

        #endregion

        /// <summary>
        /// 关闭此套接字
        /// </summary>
        protected virtual void CloseSocket()
        {
            __CloseSocket("客户端主动断开连接");
        }

        /// <summary>
        /// 关闭此套接字
        /// </summary>
        /// <param name="errinfo">错误信息</param>
        protected virtual void CloseSocket(String errinfo)
        {
            __CloseSocket(errinfo);
        }

        /// <summary>
        /// 关闭此套接字
        /// </summary>
        /// <param name="errinfo">错误信息</param>
        protected virtual void __CloseSocket(String errinfo)
        {
            try
            {
                if (null != Sock)
                {
                    Sock.Close();
                }
            }
            catch(Exception e)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("关闭socket连接异常, 异常原因:{0}", e.ToString()));
            }
        }

        /// <summary>
        /// 检测网络消息的有效性
        /// </summary>
        /// <returns>是否有效</returns>
//        protected bool CheckMsgValidity(ref MsgHead head)
//        {
//            // 检查数据载荷的长度
//            if (head.msglen > ProtocolConstant.MaxNetMsgLen)
//            {
//                //CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("消息头部中的消息长度太长[{0}], 断开TCP连接", head.msglen));
//                return false;
//            }
//
//            return true;
//        }
    }
}
