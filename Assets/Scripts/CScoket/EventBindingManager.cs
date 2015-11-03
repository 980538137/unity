using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.ourgame.texasSlots;

namespace Assets.Scripts.Manager.Common
{
    /// <summary>
    /// 游戏中非网络的绑定事件
    /// </summary>
    class EventBindingManager : Framework.Singleton<EventBindingManager>
    {
		#region 比倍收分返回消息
		/// <summary>
		/// 比倍收分返回消息
		/// </summary>
		public delegate void GlobalDoubleGetGold(OGAckDoubleGetGold msg);
		public event GlobalDoubleGetGold EvtGlobalDoubleGetGold;

		public void DoGlobalDoubleGetGold(OGAckDoubleGetGold msg)
        {
			if (null != EvtGlobalDoubleGetGold)
			{
				EvtGlobalDoubleGetGold(msg);
            }
        }

        #endregion

		#region 比倍结果
		/// <summary>
		/// 比倍结果
		/// </summary>
		public delegate void GlobalDoubleResult(OGAckDoubleResult msg);
		public event GlobalDoubleResult EvtGlobalDoubleResult;
		
		public void DoGlobalDoubleResult(OGAckDoubleResult msg)
        {
			if (null != EvtGlobalDoubleResult)
			{
				EvtGlobalDoubleResult(msg);
            }
        }

        #endregion

		#region 游戏结果
		/// <summary>
		/// 游戏结果
		/// </summary>
		public delegate void GlobalGameResult(OGAckGameResult msg);
		public event GlobalGameResult EvtGlobalGameResult;
		
		public void DoGlobalGameResult(OGAckGameResult msg)
		{
			if (null != EvtGlobalGameResult)
			{
				EvtGlobalGameResult(msg);
			}
		}
		
		#endregion

		#region 关闭按钮返回消息
		/// <summary>
		/// 关闭按钮返回消息
		/// </summary>
		public delegate void GlobalExit(OGAckExit msg);
		public event GlobalExit EvtGlobalExit;

		public void DoGlobalExit(OGAckExit msg)
		{
			if (null != EvtGlobalExit)
			{
				EvtGlobalExit(msg);
            }
        }

        #endregion

		#region 房间配置消息
		/// <summary>
		/// 房间配置消息
		/// </summary>
		public delegate void GlobalRoom(OGAckRoom room);
		public event GlobalRoom EvtGlobalGlobalRoom;

		public void DoGlobalActivtyPanel(OGAckRoom room)
		{
			if (null != EvtGlobalGlobalRoom)
			{
				EvtGlobalGlobalRoom(room);
            }
        }

        #endregion

		#region 房间列表
		/// <summary>
		/// 房间列表
		/// </summary>
		public delegate void GlobalRoomList(OGAckRoomList canGoBack);
		public event GlobalRoomList EvtGlobalRoomList;
		
		public void DoGlobalRoomList(OGAckRoomList canGoBack)
		{
			if (null != EvtGlobalRoomList)
			{
				EvtGlobalRoomList(canGoBack);
			}
		}
		
		#endregion

        #region 玩家信息
        /// <summary>
        /// 	玩家信息
        /// </summary>
		public delegate void GlobalRoleLoginMsg(OGAckRoleLoginMsg logMsg);
		public event GlobalRoleLoginMsg EvtGlobalRoleLoginMsg;
		public void DoGlobalRoleLoginMsg(OGAckRoleLoginMsg logMsg)
        {
			if (null != EvtGlobalRoleLoginMsg)
            {
				EvtGlobalRoleLoginMsg(logMsg);
            }
        }
        #endregion
		#region 连接game服务器
		/// <summary>
		/// 连接game服务器
		/// </summary>
		public delegate void GlobalConnectGame(OGAckLinkServer Linkserver);
		public event GlobalConnectGame EvtGlobalConnectGame;
		public void DoGlobalGlobalConnectGame(OGAckLinkServer Linkserver)
		{
			if (null != EvtGlobalConnectGame)
			{
				EvtGlobalConnectGame(Linkserver);
			}
		}
		#endregion
		#region jp奖池信息 
		/// <summary>
		/// jp奖池信息 
		/// </summary>
		public delegate void GlobalJP(OGAckJpRecord jp);
		public event GlobalJP EvtGlobalJP;
		public void DoGlobalJP(OGAckJpRecord jp)
		{
			if (null != EvtGlobalJP)
			{
				EvtGlobalJP(jp);
			}
		}
		#endregion
		#region 排行榜
		/// <summary>
		/// 排行榜
		/// </summary>
		public delegate void GlobalTop(OGAckRank jp);
		public event GlobalTop EvtGlobalTop;
		public void DoGlobalTop(OGAckRank jp)
		{
			if (null != EvtGlobalTop)
			{
				EvtGlobalTop(jp);
			}
		}
		#endregion
		#region jp奖池
		/// <summary>
		/// jp奖池
		/// </summary>
		public delegate void jpReward(OGAckJP jp);
		public event jpReward EvtjpReward;
		public void DojpReward(OGAckJP jp)
		{
			if (null != EvtjpReward)
			{
				EvtjpReward(jp);
			}
		}
		#endregion
		public delegate void LeaveGame(OGAckLeaveGame jp);
		public event LeaveGame EvtLeaveGame;
		public void DoLeaveGame(OGAckLeaveGame jp)
		{
			if (null != EvtLeaveGame)
			{
				EvtLeaveGame(jp);
			}
		}
		public delegate void JpResult(OGAckDoubleJpResult jp);
		public event JpResult EvtJpResult;
		public void DoJpResult(OGAckDoubleJpResult jp)
		{
			if (null != EvtJpResult)
			{
				EvtJpResult(jp);
			}
		}
		public delegate void StopGame(OGAckStopGame stop);
		public event StopGame EvtStopGame;
		public void DoStopGame(OGAckStopGame stop)
		{
			if (null != EvtStopGame)
			{
				EvtStopGame(stop);
			}
		}
    }
}
