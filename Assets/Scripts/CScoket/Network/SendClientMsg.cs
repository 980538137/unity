using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network;


	public  class SendClientMsg: Framework.Singleton<SendClientMsg>
    {
        /// <summary>
        /// 发送请求消息
        /// </summary>
        public void SendMsg(byte[] sendbuf, Int32 msglen, UInt32 msgid, UInt32 cseq)
        {
                SystemSocket.Instance.TcpClient.SendMsg(sendbuf, sendbuf.Length, msgid, cseq);          
        }
    }
