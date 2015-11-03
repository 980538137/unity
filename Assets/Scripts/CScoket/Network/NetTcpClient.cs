/********************************************************************
	created:	2007/10/20
	created:	20:10:2007   23:58
	file base:	NetClient
	file ext:	cs
	author:		liyi
	
	purpose:	实现TCP客户端的封装，体会套接字就是一个文件这句话
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.Runtime.InteropServices;
using Framework;
using com.ourgame.texasSlots;
using Assets.Scripts.Network;
using Assets.Scripts.Manager.Common;

namespace Framework.OSAbstract
{
    public class NetTcpClient : NetTcpSocketClientBase
    {
        /// <summary>
        /// 交代给UI控制层去完成的工作
        /// </summary>
    //    public IProtoDelegate ProtoDelegate { get; private set; }

        // 发送心跳消息的定时器
        Timer mHeartBeatTimer;

        #region 构造函数

        /// <summary>
        /// 构造函数，默认为阻塞套接字
        /// </summary>
        /// <param name="protodelegate">协议代理</param>
//        public NetTcpClient(IProtoDelegate protodelegate)
//            : this(protodelegate, true)
//        {
//        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="protodelegate">协议代理</param>
        /// <param name="is_block">套接字是否阻塞</param>
//        public NetTcpClient(IProtoDelegate protodelegate, bool is_block)
//            : this(protodelegate, is_block, 64 * 10240, 64 * 10240)
//        {
//        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="protodelegate">协议代理</param>
        /// <param name="is_block">套接字是否阻塞</param>
        /// <param name="send_buf_size">发送缓存区</param>
        /// <param name="recv_buf_size">接收缓存区</param>
//        public NetTcpClient(IProtoDelegate protodelegate,
//                            bool is_block,
//                            Int32 send_buf_size,
//                            Int32 recv_buf_size)
//        {
//            IsBlock = is_block;
//            SendBufSize = send_buf_size;
//            RecvBufSize = recv_buf_size;
//
//            ProtoDelegate = protodelegate;
//        }

        #endregion

        #region 方法

	    /// <summary>
	    /// 创建客户端套接字
	    /// </summary>
        public SocketErrorRet CreatSocket(string remoteserverip, UInt16 remoteserverport)
        {
            try
            {
                // 创建TCP套接字
                Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // 客户端是不需要绑定套接字的，这些IP地址和端口都是随机的

                ////////////////////
                // 设置套接字的属性
                ////////////////////
//                Sock.ReceiveBufferSize = RecvBufSize;
//                Sock.SendBufferSize = SendBufSize;
//                Sock.Blocking = IsBlock;

                // 设置远程服务器的端点地址信息
                // 设置的时候如果有问题会抛出异常
                RemoteServerIP = remoteserverip;
                RemoteServerPort = remoteserverport;
            }
            catch (Exception e)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("创建套接字失败\n {0}", e.ToString()));
                return SocketErrorRet.SOCK_ERR;
            }

            return SocketErrorRet.SOCK_OK;
        }

        /// <summary>
        /// 连接到服务器
        /// </summary>
        public void Connect()
        {
            //CSysLog.Instance.Write(SysLogLevel.NOTICE, String.Format("连接服务器[{0}:{1}]", RemoteServerIP, RemoteServerPort));

            if (null == Sock)
            {
                throw new Exception("无效的套接字");
            }

            IPAddress IP = IPAddress.Parse(RemoteServerIP);

            // 客户端是没有必要绑定的

            // 连接服务器
            try
            {
				Sock.BeginConnect(IP, RemoteServerPort, new AsyncCallback(ConnectCallback), Sock);
            }
            catch (Exception e)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("连接服务器[{0}:{1}]失败\n {2}", RemoteServerIP, RemoteServerPort, e.ToString()));
                
                // 通知应用层连接失败
//                NetTcpConnectFailMsg tcpconnectfailmsg = new NetTcpConnectFailMsg(e.Message);
//                ModelToControlQueue.Instance.AddNetMsg(tcpconnectfailmsg);
            }
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            Socket s = (Socket)ar.AsyncState;

            try
            {
//                s.EndConnect(ar);
            }
            catch(SocketException se)
            {
                SocketError errcode = se.SocketErrorCode;
                //CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("连接服务器[{0}:{1}]失败\n 错误码: {2}, 描述: {3}", RemoteServerIP, RemoteServerPort, errcode, se.Message));

                // 连接超时
                if (SocketError.TimedOut == errcode)
                {
                    //TODO: 通知用户连接超时
//                    NetTcpConnectTimeoutMsg tcpconnecttimeoutmsg = new NetTcpConnectTimeoutMsg();
//                    ModelToControlQueue.Instance.AddNetMsg(tcpconnecttimeoutmsg);
                }
                // 连接失败
                else
                {
//                    NetTcpConnectFailMsg tcpconnectfailmsg = new NetTcpConnectFailMsg(se.Message);
//                    ModelToControlQueue.Instance.AddNetMsg(tcpconnectfailmsg);
                }

                return;
            }
            catch(Exception e)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("连接服务器[{0}:{1}]失败\n {2}", RemoteServerIP, RemoteServerPort, e.ToString()));

//                NetTcpConnectFailMsg tcpconnectfailmsg = new NetTcpConnectFailMsg(e.Message);
//                ModelToControlQueue.Instance.AddNetMsg(tcpconnectfailmsg);

                return;
            }

            // 连接服务器成功，根据情况启动心跳定时器
//            if (null == mHeartBeatTimer)
//            {
//                mHeartBeatTimer = new Timer(ProtocolConstant.HeartBeatPeriod);
//
//                mHeartBeatTimer.Elapsed += mHeartBeatTimer_Elapsed;
//                mHeartBeatTimer.Start();
//            }
            
            // 开始接收消息
            RecvMsg(Sock);
            // 连接成功
            //CSysLog.Instance.Write(SysLogLevel.NOTICE, String.Format("连接服务器[{0}:{1}]成功!", RemoteServerIP, RemoteServerPort));

            //TODO: TCP连接建立成功，通知应用层进行必要的处理
//            NetTcpConnectSuccessMsg tcpconnectsuccessmsg = new NetTcpConnectSuccessMsg();
//            ModelToControlQueue.Instance.AddNetMsg(tcpconnectsuccessmsg);
        }

        /// <summary>
        /// 心跳消息定时器处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
//        private void mHeartBeatTimer_Elapsed(object sender, ElapsedEventArgs e)
//        {
//            Byte[] sendbuf = ProtoDelegate.GenerateHeartBeatMsg();
//
//            Send(sendbuf, Marshal.SizeOf(typeof(MsgHead)));
//        }

        /// <summary>
        /// 关闭套接字
        /// </summary>
        /// <param name="errinfo">错误信息</param>
        protected override void __CloseSocket(String errinfo)
        {
            base.__CloseSocket(errinfo);

            // 关闭心跳发送定时器
            CloseHeatBeat();

            //TODO: 通知应用层建立已经断开
//            NetTcpDisconnectMsg tcpdiconnectmsg = new NetTcpDisconnectMsg(errinfo);
//            ModelToControlQueue.Instance.AddNetMsg(tcpdiconnectmsg);
        }

        /// <summary>
        /// 重新建立连接
        /// </summary>
        public void ReConnect()
        {
            //CSysLog.Instance.Write(SysLogLevel.NOTICE, "重新建立TCP连接");

            // 创建套接字
            if (SocketErrorRet.SOCK_OK != CreatSocket(RemoteServerIP, RemoteServerPort))
            {
                return;
            }

            // 建立TCP连接
            Connect();
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseConnect()
        {
            base.__CloseSocket("主动关闭连接");

            //关闭心跳发送定时器
            //CloseHeatBeat();      
        }

        private void CloseHeatBeat()
        {
            try
            {
                if (null != mHeartBeatTimer)
                {
                    // 关闭心跳发送定时器
                    mHeartBeatTimer.Stop();
                    mHeartBeatTimer.Dispose();
                    mHeartBeatTimer = null;
                }
            }
            catch (Exception e)
            {
                //CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("关闭心跳定时器异常, 异常原因:{0}", e.ToString()));
            }
        }
        #endregion
    }
}
