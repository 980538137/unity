using UnityEngine;
using System.Collections;
using Framework.SocketMsgHandle;
using Assets.Scripts.Network.Cmd.Protocol;
using com.ourgame.texasSlots;
using System;
using Assets.Scripts.Network.Cmd.Facade;

public partial class CmdAutoInstance : Framework.Singleton<CmdAutoInstance>
{
	private CmdRegisterFactory<MsgServerOGAckRank> CmdRegisterFactoryCmdServerAckRank = new CmdRegisterFactory<MsgServerOGAckRank>(CmdDefine.OGID_REQ | CmdDefine.GATE_GET_AVAILABLE_ROOM);
}
public class MsgServerOGAckRank : CmdServer {
	
	private const string NOTIFY_MSG = "服务器 --> 客户端 玩家信息通知:";
	
	private OGAckRank recvMsg;
	
	#region 构造函数
	
	public MsgServerOGAckRank()
		: base("MsgServerOGAckRank", CmdDefine.OGID_REQ | CmdDefine.GATE_GET_AVAILABLE_ROOM)
	{
		
	}
	
	#endregion
	
	#region 方法
	/// <summary>
	/// 获取请求消息，主要是反序列化消息
	/// </summary>
	/// <param name="msg">网络消息</param>
	/// <param name="len">网络消息长度</param>
	/// <returns></returns>
	protected override bool GetReq(Byte[] msg, Int32 len)
	{
		recvMsg = UtilNet.DeserializeNetMsg<OGAckRank>(msg);
		if (null == recvMsg)
		{
			//CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("{0} 反序列化响应消息失败", NOTIFY_MSG));
			return false;
		}
		
		//CSysLog.Instance.Write(SysLogLevel.DEBUG, String.Format("{0} self_seat={1}", NOTIFY_MSG, recvMsg.self_seat));
		return true;
	}
	
	/// <summary>
	/// 进行服务器命令的逻辑处理
	/// </summary>
	protected override void LogicDo()
	{
		if (null == recvMsg)
		{
			return;
		}
		
		NetworkAccessFacade.Instance.DoAllotTop(recvMsg);         
	}
	
	#endregion
}