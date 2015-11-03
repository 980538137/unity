using UnityEngine;
using System.Collections;
using Assets.Scripts.Network.Cmd.Facade;
using com.ourgame.texasSlots;
using Assets.Scripts.Network;
using Framework.OSAbstract;
using System;

namespace Assets.Scripts.Manager.Common
{
	public class NetMsgManger : Framework.Singleton<NetMsgManger> {

		// Use this for initialization
		public  void InitNetMsgDelegate () {
			NetworkAccessFacade.Instance.EvtGetRoomListRsp += LoginRsp;
			NetworkAccessFacade.Instance.EvtGetRoomRunInfoRsp += LoginMsg;
			NetworkAccessFacade.Instance.EvtLeaveRoomRsp += RoomList;
			NetworkAccessFacade.Instance.EvtLeaveRoomNotify += AckRoom;
			NetworkAccessFacade.Instance.EvtPlayerReadyRsp += GameResult;
			NetworkAccessFacade.Instance.EvtFreeCoinsRsp += Exit;
			NetworkAccessFacade.Instance.EvtPlayerReadyNtf += DoubleResult;
			NetworkAccessFacade.Instance.EvtPlayerTrustPlayNtf += GetGold;
			NetworkAccessFacade.Instance.EvtAllotSeatNotify += GetJP;
			NetworkAccessFacade.Instance.EvtAllotTop += GetTop;
			NetworkAccessFacade.Instance.EvtRewardAckJP += Reward;
			NetworkAccessFacade.Instance.EvtLeaveGameNotify += LeaveGame;
			NetworkAccessFacade.Instance.EvtJpResultNotify += JPResult;
			NetworkAccessFacade.Instance.EvtStop+=Stop;
		}
		/// <summary>
		/// 删除网络委托事件的函数绑定
		/// </summary>
		public void DestoryNetMsgDelegate()
		{
			NetworkAccessFacade.Instance.EvtGetRoomListRsp -= LoginRsp;
			NetworkAccessFacade.Instance.EvtGetRoomRunInfoRsp -= LoginMsg;
			NetworkAccessFacade.Instance.EvtLeaveRoomRsp -= RoomList;
			NetworkAccessFacade.Instance.EvtLeaveRoomNotify -= AckRoom;
			NetworkAccessFacade.Instance.EvtPlayerReadyRsp -= GameResult;
			NetworkAccessFacade.Instance.EvtFreeCoinsRsp -= Exit;
			NetworkAccessFacade.Instance.EvtPlayerReadyNtf -= DoubleResult;
			NetworkAccessFacade.Instance.EvtPlayerTrustPlayNtf -= GetGold;
			NetworkAccessFacade.Instance.EvtAllotSeatNotify -= GetJP;
			NetworkAccessFacade.Instance.EvtAllotTop -= GetTop;
			NetworkAccessFacade.Instance.EvtRewardAckJP -= Reward;
			NetworkAccessFacade.Instance.EvtLeaveGameNotify -= LeaveGame;
			NetworkAccessFacade.Instance.EvtJpResultNotify -= JPResult;
			NetworkAccessFacade.Instance.EvtStop-=Stop;
		}
		//请求登录服务器
		public  void UserLogin(string userId)
		{
			OGReqTRLogin1 log1 = new OGReqTRLogin1 ();
			log1.UserId = userId;
			NetworkAccessFacade.Instance.SendLoginReq(log1);
		}
		// 登陆信息的返回
		public  void LoginRsp(OGAckLinkServer recvMsg)
		{
			if (recvMsg.Result == OGAckLinkServer.E_TRResult.ET_SUCCESS) {
//						SystemSocket.Instance.ConClose();
						EventBindingManager.Instance.DoGlobalGlobalConnectGame(recvMsg);
						Playersocket.Instance.socketP(recvMsg.LinkIp,recvMsg.LinkPort);
					} else {
						Debug.Log(recvMsg.Reason);
					}
		}
		//登录验证 
		public void ReqLogin(OGReqTRLogin2 reqLog)
		{
			NetworkAccessFacade.Instance.SendGetRoomListReq(reqLog);
		}
		//验证响应玩家信息
		public void LoginMsg(OGAckRoleLoginMsg loginMsg)
		{
			if (loginMsg.Result == OGAckRoleLoginMsg.E_TRResult.ET_SUCCESS) {
								EventBindingManager.Instance.DoGlobalRoleLoginMsg(loginMsg);
								Debug.Log ("验证成功！");
						} else {

							Debug.Log("验证失败!!!!!");
						}
			Debug.Log (loginMsg.Result);
			Debug.Log (loginMsg.Reason);
			Debug.Log (loginMsg.userId);
			Debug.Log (loginMsg.roleName);
			Debug.Log (loginMsg.level);
			Debug.Log (loginMsg.exp);
			Debug.Log (loginMsg.gold);
		}
		//房间列表
		public void RoomList(OGAckRoomList roomList)
		{
			EventBindingManager.Instance.DoGlobalRoomList (roomList);
		}
		//点击进入房间
		public void ReqJoinRoom(Int32 Id)
		{
			OGReqJoinRoom room = new OGReqJoinRoom ();
			room.roomId = Id;
			NetworkAccessFacade.Instance.SendGetRoomRunInfoReq (room);
		}
		//房间配置消息
		public void AckRoom(OGAckRoom Room)
		{
			if (Room.Result == OGAckRoom.E_TRResult.ET_SUCCESS) {
								EventBindingManager.Instance.DoGlobalActivtyPanel (Room);
						} else {
							Debug.Log("权限不够");
						}
		}
		//关闭按钮
		public void ExitType(OGReqExitType.E_MsgType type)
		{
			OGReqExitType exit = new OGReqExitType ();
			exit.msgType = type;
			NetworkAccessFacade.Instance.SendLevelRoomReq (exit);
		}
		// 关闭按钮
		public void Exit(OGAckExit exit)
		{
			EventBindingManager.Instance.DoGlobalExit (exit);
		}
		//开始转动游戏
		public void Startgame(Int32 line,Int32 times,bool auto)
		{
			OGReqStartGame game = new OGReqStartGame ();
			game.line = line;
			game.times = times;
			game.auto = auto;
			NetworkAccessFacade.Instance.SendFreeCoinsReq (game);
		}
		// 返回游戏结果
		public void GameResult(OGAckGameResult Result)
		{
			EventBindingManager.Instance.DoGlobalGameResult(Result);
		}
		// 开始比倍游戏
		public void Doublegame(Int32 times,Int32 color)
		{     
			OGReqDoubleGame game = new OGReqDoubleGame ();
			game.times = times;
			game.colour = color;
			NetworkAccessFacade.Instance.SendPlayerReadyReq (game);
		}
		// 返回比倍结果
		public void DoubleResult(OGAckDoubleResult Result)
		{
			EventBindingManager.Instance.DoGlobalDoubleResult(Result);
		}
		//比倍收分
		public void GetChip()
		{
			OGReqDoubleGetGold chip = new OGReqDoubleGetGold ();
			NetworkAccessFacade.Instance.SendTrustPlayReq (chip);
		}
		// 比倍收分消息
		public void GetGold(OGAckDoubleGetGold getGold)
		{
			EventBindingManager.Instance.DoGlobalDoubleGetGold (getGold);
		}
		//send  jp
		public void ReqJP(string  name)
		{
			OGReqJpRecord jp = new OGReqJpRecord ();
			jp.roleName = name;
			NetworkAccessFacade.Instance.SendPlayerChatReq (jp);
		}
		//jp获奖记录
		public void GetJP(OGAckJpRecord jp)
		{
			EventBindingManager.Instance.DoGlobalJP (jp);
		}

		public void ReqTop(string  name)
		{
			OGReqRank tp = new OGReqRank ();
			tp.roleName = name;
			NetworkAccessFacade.Instance.SendTop (tp);
		}

		public void GetTop(OGAckRank tp)
		{
			EventBindingManager.Instance.DoGlobalTop (tp);
		}
		public void  Reward(OGAckJP jp)
		{
			EventBindingManager.Instance.DojpReward (jp);
		}
		public void  LeaveGame(OGAckLeaveGame jp)
		{
			EventBindingManager.Instance.DoLeaveGame (jp);
		}
		public void  JPResult(OGAckDoubleJpResult jp)
		{
			EventBindingManager.Instance.DoJpResult (jp);
		}
		public void Stop(OGAckStopGame stop)
		{
			EventBindingManager.Instance.DoStopGame (stop);
		}


	}
}
