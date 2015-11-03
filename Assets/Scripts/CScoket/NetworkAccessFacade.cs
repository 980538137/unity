using UnityEngine;
using System.Collections;
using com.ourgame.texasSlots;
namespace Assets.Scripts.Network.Cmd.Facade
{
    /// <summary>
    /// 向上层提供网络访问的外观
    /// </summary>
    public class NetworkAccessFacade : Framework.Singleton<NetworkAccessFacade>
    {
        /// <summary>
		/// 请求登录服务器
        /// </summary>
		/// <param name="loginreq">登录服务器</param>
		public void SendLoginReq(OGReqTRLogin1 req)
        {
			ClientMsgLog1 login = new ClientMsgLog1(req);
            login.SendMessage();
        }


		#region 获取登录信息
        /// <summary>
		/// 获取登录信息
        /// </summary>
		public delegate void GetRoomListRsp(OGAckLinkServer rsp);
        public event GetRoomListRsp EvtGetRoomListRsp;

		public void DoGetRoomListRsp(OGAckLinkServer getRoomListRsp)
        {
            if (null != EvtGetRoomListRsp)
            {
                EvtGetRoomListRsp(getRoomListRsp);
            }
        }

        /// <summary>
		/// 玩家登录验证
        /// </summary>
		/// <param name="req">玩家登录验证</param>
		public void SendGetRoomListReq(OGReqTRLogin2 req)
        {
			ClientMsgLog2 getRoomList = new ClientMsgLog2(req);
            getRoomList.SendMessage();
        }

        #endregion

		#region 获取玩家信息
        /// <summary>
		/// 获取玩家信息
        /// </summary>
		public delegate void GetRoomRunInfoRsp(OGAckRoleLoginMsg rsp);
        public event GetRoomRunInfoRsp EvtGetRoomRunInfoRsp;

		public void DoGetRoomRunInfoRsp(OGAckRoleLoginMsg getRoomRunInforsp)
        {
            if (null != EvtGetRoomRunInfoRsp)
            {
                EvtGetRoomRunInfoRsp(getRoomRunInforsp);
            }
        }

        /// <summary>
		/// 点击进入房间的操作 
        /// </summary>
		/// <param name="req">点击进入房间的操作 </param>
		public void SendGetRoomRunInfoReq(OGReqJoinRoom req)
        {
            ClientMsgOGReqJoinRoom getRoomRunInfo = new ClientMsgOGReqJoinRoom(req);
            getRoomRunInfo.SendMessage();
        }

        #endregion

		#region 更新玩家信息
        /// <summary>
		/// 更新玩家信息
        /// </summary>
		public delegate void EnterRoomRsp(OGAckRoleMsg rsp);
        public event EnterRoomRsp EvtEnterRoomRsp;

		public void DoEnterRoomRsp(OGAckRoleMsg enterRoomRsp)
        {
            if (null != EvtEnterRoomRsp)
            {
                EvtEnterRoomRsp(enterRoomRsp);
            }
        }

        #endregion

		#region 发送房间列表
        /// <summary>
		/// 发送房间列表
        /// </summary>
		public delegate void LeaveRoomRsp(OGAckRoomList rsp);
        public event LeaveRoomRsp EvtLeaveRoomRsp;

		public void DoLevelRoomRsp(OGAckRoomList leaveRoomRsp)
        {
            if (null != EvtLeaveRoomRsp)
            {
                EvtLeaveRoomRsp(leaveRoomRsp);
            }
        }

        /// <summary>
        /// 发送退出
        /// </summary>
        /// <param name="req"></param>
		public void SendLevelRoomReq(OGReqExitType req)
        {
			ClientMsgOGReqExitType leaveRoom = new ClientMsgOGReqExitType(req);
            leaveRoom.SendMessage();
        }

        /// <summary>
		/// 返回房间配置消息
        /// </summary>
		public delegate void LeaveRoomNotify(OGAckRoom ntf);
        public event LeaveRoomNotify EvtLeaveRoomNotify;

		public void DoLevelRoomNotify(OGAckRoom leaveRoomNtf)
        {
            if (null != EvtLeaveRoomNotify)
            {
                EvtLeaveRoomNotify(leaveRoomNtf);
            }
        }

        #endregion

		#region 关闭按钮返回消息
        /// <summary>
		/// 关闭按钮返回消息
        /// </summary>
		public delegate void FreeCoinsRsp(OGAckExit rsp);
        public event FreeCoinsRsp EvtFreeCoinsRsp;

		public void DoFreeCoinsRsp(OGAckExit freeCoinsRsp)
        {
            if (null != EvtFreeCoinsRsp)
            {
                EvtFreeCoinsRsp(freeCoinsRsp);
            }
        }

        /// <summary>
		/// 开始转动游戏 
        /// </summary>
		/// <param name="req">开始转动游戏 </param>
		public void SendFreeCoinsReq(OGReqStartGame req)
        {
			ClientMsgOGReqStartGame freeCoins = new ClientMsgOGReqStartGame(req);
            freeCoins.SendMessage();
        }

        #endregion

		#region 返回游戏结果
        /// <summary>
		/// 返回游戏结果
        /// </summary>
		public delegate void PlayerReadyRsp(OGAckGameResult rsp);
        public event PlayerReadyRsp EvtPlayerReadyRsp;

		public void DoPlayerReadyRsp(OGAckGameResult playerReadyRsp)
        {
            if (null != EvtPlayerReadyRsp)
            {
                EvtPlayerReadyRsp(playerReadyRsp);
            }
        }

        /// <summary>
		/// 开始比倍游戏
        /// </summary>
		/// <param name="req">开始比倍游戏</param>
		public void SendPlayerReadyReq(OGReqDoubleGame req)
        {
			ClientMsgOGReqDoubleGame playerReady = new ClientMsgOGReqDoubleGame(req);
            playerReady.SendMessage();
        }

        /// <summary>
		/// 服务器返回比倍结果
        /// </summary>
		public delegate void PlayerReadyNtf(OGAckDoubleResult ntf);
        public event PlayerReadyNtf EvtPlayerReadyNtf;

		public void DoPlayerReadyNtf(OGAckDoubleResult playerReadyNtf)
        {
            if (null != EvtPlayerReadyNtf)
            {
                EvtPlayerReadyNtf(playerReadyNtf);
            }
        }

        #endregion

		#region 比倍
        /// <summary>
		/// 比倍收分消息
        /// </summary>
        /// <param name="req">请求进入房间请求</param>
		public void SendTrustPlayReq(OGReqDoubleGetGold req)
        {
			ClientMsgOGReqDoubleGetGold playerTrustPlay = new ClientMsgOGReqDoubleGetGold(req);
            playerTrustPlay.SendMessage();
        }

        /// <summary>
		/// 比倍收分返回消息
        /// </summary>
		public delegate void PlayerTrustPlayNtf(OGAckDoubleGetGold ntf);
        public event PlayerTrustPlayNtf EvtPlayerTrustPlayNtf;

		public void DoPlayerTrustPlayNtf(OGAckDoubleGetGold trustPlayNtf)
        {
            if (null != EvtPlayerTrustPlayNtf)
            {
                EvtPlayerTrustPlayNtf(trustPlayNtf);
            }
        }

        #endregion

		#region 请求jp获奖记录
		/// <summary>
		/// 请求jp获奖记录
		/// </summary>
		
		public void SendPlayerChatReq(OGReqJpRecord rolename)
		{
			ClientMsgReqJP playerChat = new ClientMsgReqJP(rolename);
			playerChat.SendMessage();
		}
        /// <summary>
		/// 返回jp获奖记录
        /// </summary>
		public delegate void AllotSeatNotify(OGAckJpRecord ntf);
        public event AllotSeatNotify EvtAllotSeatNotify;

		public void DoAllotSeatNotify(OGAckJpRecord allotSeatNtf)
        {
			if (null != EvtAllotSeatNotify)
            {
				EvtAllotSeatNotify(allotSeatNtf);
            }
        }

        #endregion

		#region 请求排行榜
		/// <summary>
		/// 请求排行榜
		/// </summary>
		
		public void SendTop(OGReqRank rolename)
		{
			ClientMsgTop playerChat = new ClientMsgTop(rolename);
			playerChat.SendMessage();
		}
		/// <summary>
		/// 返回排行榜
		/// </summary>
		public delegate void AllotTop(OGAckRank ntf);
		public event AllotTop EvtAllotTop;
		
		public void DoAllotTop(OGAckRank allotSeatNtf)
		{
			if (null != EvtAllotTop)
			{
				EvtAllotTop(allotSeatNtf);
			}
		}
		
		#endregion

		/// <summary>
		/// 返回jp奖池信息
		/// </summary>
		public delegate void RewardAckJP(OGAckJP ntf);
		public event RewardAckJP EvtRewardAckJP;
		
		public void DoRewardAckJP(OGAckJP allotSeatNtf)
		{
			if (null != EvtRewardAckJP)
			{
				EvtRewardAckJP(allotSeatNtf);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public delegate void LeaveGameNotify(OGAckLeaveGame ntf);
		public event LeaveGameNotify EvtLeaveGameNotify;
		
		public void DoLeaveGame(OGAckLeaveGame leaveRoomNtf)
		{
			if (null != EvtLeaveGameNotify)
			{
				EvtLeaveGameNotify(leaveRoomNtf);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public delegate void JpResultNotify(OGAckDoubleJpResult ntf);
		public event JpResultNotify EvtJpResultNotify;
		
		public void DoJpResult(OGAckDoubleJpResult leaveRoomNtf)
		{
			if (null != EvtJpResultNotify)
			{
				EvtJpResultNotify(leaveRoomNtf);
			}
		}
		/// <summary>
		/// 返回止损消息
		/// </summary>
		public delegate void Stop(OGAckStopGame ntf);
		public event Stop EvtStop;
		
		public void Dostop(OGAckStopGame allotSeatNtf)
		{
			if (null != EvtStop)
			{
				EvtStop(allotSeatNtf);
			}
		}
    }
}