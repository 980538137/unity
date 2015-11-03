using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.OSAbstract;
using Assets.Scripts.Network;

namespace Assets.Scripts.Manager.Common
{
    /// <summary>
    /// 一些公用函数
    /// </summary>
    class UtilFuncManager : Framework.Singleton<UtilFuncManager>
    {
        /// <summary>
        /// 根据传入的金币数，返回一个字符串
        /// </summary>
        public string GetStrInfoByCoinNum(long coinNum)
        {
            string str = "";
            if (coinNum > 100000000)
            {
                long temp = coinNum / 100000000;
                str += temp + "亿";
                coinNum = coinNum % 100000000;
            }
       
            if (coinNum > 10000)
            {
                long temp = coinNum / 10000;
                str += temp + "万";
                coinNum = coinNum % 10000;
            }

            if (coinNum > 0)
            {
                str += coinNum;
            }

            return str;
        }

        /// <summary>
        /// 根据传入的金币数，返回一个字符串,最多只显示5位
        /// </summary>
        public string GetStrInfoByCoinNumWithFive(long coinNum)
        {
            string str = "";
            if (coinNum >= 100000000)
            {
                long temp = coinNum / 100000000;
                str += temp + "亿";
                coinNum = coinNum % 100000000;
                return str;
            }

            if (coinNum >= 10000)
            {
                long temp = coinNum / 10000;
                str += temp + "万";
                coinNum = coinNum % 10000;
                return str;
            }

            str += coinNum;
            return str;
        }

		/// <summary>
		/// 全局调用充值购买
		/// </summary>
		public void GlobalRecharge()
		{
			#if UNITY_ANDROID
                if (ProductMgr.productList.ContainsKey(SystemManager.Instance.GlobalRecharProductID))
			    {
                    int runid           = 0;
                    string userRoleName = PrefsData.Instance.loginStatusData.roleName;
                    int payType         = int.Parse(ProductMgr.sharedInstance.GetSdkPackageId(SystemManager.Instance.GlobalRecharProductID));
                    string orderID      = "";
                    string extendData   = "";

                    AndroidHelper.Instance.startPayAction(runid, userRoleName, payType, SystemManager.Instance.GlobalRecharProductID, orderID, extendData);
                    CSysLog.Instance.Write(SysLogLevel.DEBUG, "调用全局支付功能购买金币,开始支付! productId=" + SystemManager.Instance.GlobalRecharProductID);
			    }
                else
                {
                    CSysLog.Instance.Write(SysLogLevel.DEBUG, String.Format("调用全局支付功能购买金币失败,没有获取到正确的productId! 要充值的productId={0}", SystemManager.Instance.GlobalRecharProductID));
                }
            #endif
        }

		/// <summary>
		/// 全局调用切换账号后的重新登录
		/// </summary>
		public void GlobalChangeAccountLogin()
		{
           // if (PlayerInfoManager.Instance.LoginSuccess)
//            {
//                SystemManager.Instance.SelfStopConnect = true;
//                SystemSocket.Instance.TcpClient.CloseConnect();
//                SystemSocket.Instance.Start();
//            }
		}

        /// <summary>
        /// 根据秒的总和，返回一个时间值的字符串
        /// <summary>
        public string GetStringFromVipTime(long time)
        {
            string str = "";

            long dayTime = 24*60*60;

            if(time >= dayTime)
            {
                long  tempDay = time/dayTime;
                str += tempDay + "天";

                time = time % dayTime;
            }
            else
            {
                str += 0 + "天";
            }

            long hourTime = 60*60;
            if(time >= hourTime)
            {
                long tempHour = time / hourTime;
                str += tempHour + "小时";

                time = time % dayTime;
            }
            else if (time < hourTime)
            {
                str += 0 + "小时";
            }

            return str;
        }

        /// <summary>
        /// 根据传入的货币类型，返回对应的字符串
        /// <summary>
//        public string GetStringFromCurrencyType(int type)
//        {
//            string coinInfo = "未知类型";
//            switch(type)
//            {
//                case (int)Position.CurrencyType.COIN:
//                    coinInfo = "金币";
//                    break;
//                case (int)Position.CurrencyType.DIAMOND:
//                    coinInfo = "钻石";
//                    break;
//                case (int)Position.CurrencyType.SILVER:
//                    coinInfo = "银币";
//                    break;
//                case (int)Position.CurrencyType.TICKET:
//                    coinInfo = "门票";
//                    break;
//                case (int)Position.CurrencyType.MEMBER:
//                    coinInfo = "会员";
//                    break;
//                case (int)Position.CurrencyType.BEAN:
//                    coinInfo = "万能豆";
//                    break;
//                case (int)Position.CurrencyType.GIFT:
//                    coinInfo = "礼包";
//                    break;
//            }
//
//            return coinInfo;
//        }
//
//        //全局快速开始的调用
//        public void StartGameWithMaxCoin()
//        {
//            if (NetGameInfoManager.Instance.GameRoomList.Count == 0)
//            {
//                return;
//            }
//            SystemManager.Instance.RoomPanelQuiklyStartGame = false;
//
//            //得到玩家的金币
//            long playerCoins = 0;
//            for (int i = 0; i < PlayerInfoManager.Instance.coins.Count; i++)
//            {
//                if ((int)Position.CurrencyType.COIN == PlayerInfoManager.Instance.coins[i].type)
//                {
//                    playerCoins = PlayerInfoManager.Instance.coins[i].number;
//                }
//            }
//
//            int index = 0;
//            //获取玩家的金币可进入的最大房间Index
//            for (int i = NetGameInfoManager.Instance.GameRoomList.Count - 1; i > 0; i--)
//            {
//                if (playerCoins > NetGameInfoManager.Instance.GameRoomList[i].min_coins)
//                {
//                    index = i;
//                    break;
//                }
//            }
//
//            if (NetGameInfoManager.Instance.GameRoomList.Count < index + 1)
//            {
//                CSysLog.Instance.Write(SysLogLevel.ERR, "发送进入房间的请求失败,服务器没有当前选择的房间信息!");
//                return;
//            }
//
//            Position.EnterRoomReq req = new Position.EnterRoomReq();
//            req.room_level = NetGameInfoManager.Instance.GameRoomList[index].room_level;
//            req.room_type = NetGameInfoManager.Instance.GameRoomList[index].room_type;
//            NetworkAccessFacade.Instance.SendEnterRoomReq(req);
//        }
//
//        //发送获取房间列表的消息
//        public void SendGetRoomList()
//        {
//            Position.GetRoomListReq req = new Position.GetRoomListReq();
//            req.room_type = 0;
//            NetworkAccessFacade.Instance.SendGetRoomListReq(req);
//        }
    }
}
