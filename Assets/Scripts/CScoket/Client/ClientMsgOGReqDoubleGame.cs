using UnityEngine;
using System.Collections;
using Framework.SocketMsgHandle;
using com.ourgame.texasSlots;
using Assets.Scripts.Network.Cmd.Protocol;
using Assets.Scripts.Network.Cmd.Facade;
using System;

public class ClientMsgOGReqDoubleGame : CmdClientBase {

	private const string SEND_MSG = "客户端 --> 服务器 准备开始游戏请求:";
	private const string RECV_MSG = "服务器 --> 客户端 准备开始游戏响应:";
	
	/// <summary>
	/// 玩家准备
	/// </summary>
	private OGReqDoubleGame Login1;

	private UInt32 Client=(UInt32)12345+(UInt32)51;
	#region 构造函数
	
	public ClientMsgOGReqDoubleGame(OGReqDoubleGame log1)
		: base("CmdClientLogin", CmdDefine.OGID_ACK | CmdDefine.GATE_LOGIN_REQ)
	{
		Login1 = log1;
	}
	
	#endregion
	
	#region 方法
	/// <summary>
	/// 具有要实现的发送消息的逻辑
	/// </summary>
	protected override void DoSendMsg()
	{
		if (null == Login1)
		{
			return;
		}
		
		byte[] sendByte = UtilNet.SerializeNetMsg<OGReqDoubleGame>(Login1);
		if (null == sendByte)
		{
			//CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("{0} 序列化请求消息失败", SEND_MSG));
			return;
		}
		
		SendClientMsg.Instance.SendMsg(sendByte, sendByte.Length, Client, Cseq);
		
		//CSysLog.Instance.Write(SysLogLevel.DEBUG, String.Format("{0} seat={1}", SEND_MSG, PlayerReadyReq.seat));
	}
	
	/// <summary>
	/// 超时处理
	/// </summary>
	protected override void OnTimeOut()
	{
		
	}
	
	/// <summary>
	/// 成功响应处理
	/// </summary>
	/// <param name="msg">响应消息</param>
	protected override void OnSuccessRespond(byte[] msg)
	{
		OGReqDoubleGame recvMsg = UtilNet.DeserializeNetMsg<OGReqDoubleGame>(msg);
		if (null == recvMsg)
		{
			//CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("{0} 反序列化响应消息失败", RECV_MSG));
			return;
		}       
		
		//CSysLog.Instance.Write(SysLogLevel.DEBUG, String.Format("{0} result={1} ", RECV_MSG, recvMsg.result));
		
		//			if (0 != recvMsg.result)
		//			{
		//				string errMessage = MsgErrCodeManager.Instance.GetErrCodeMessage(CmdDefine.OGID_ACK | CmdDefine.GATE_PLAYER_READY, recvMsg.result);
		//				//CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("{0} 消息返回失败 错误原因:{1}", RECV_MSG, errMessage));
		//				return;
		//			}
		
		NetworkAccessFacade.Instance.SendPlayerReadyReq(recvMsg);
	}
	
	/// <summary>
	/// 失败响应处理
	/// </summary>
	/// <param name="msg">响应消息</param>
	protected override void OnFailRespond(byte[] msg)
	{
		
	}
	
	#endregion
}