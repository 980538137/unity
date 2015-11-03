using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Network.Cmd.Facade;
using Framework.OSAbstract;
using com.ourgame.texasSlots;
using UnityEngine;

namespace Assets.Scripts.Manager.Common
{
    /// <summary>
    /// 玩家个人数据记录的单例类，在联网后或游戏中的相关数据都保存在这里
    /// </summary>
    public class PlayerInfoManager : Framework.Singleton<PlayerInfoManager>
    {

        /// <summary>
        /// 当登陆成功时，将这个设置为true
        /// <summary>
        public bool LoginSuccess = false;

		/// <summary>
		/// 玩家Name
		/// <summary>
		public string  RoleName { get; set; }
        /// <summary>
		/// 玩家的等级
        /// <summary>
        public int Level { get; set; }

        /// <summary>
		/// 玩家当前经验
        /// <summary>
        public int Exp { get; set; }

        /// <summary>
		/// 玩家的金币
        /// <summary>
        public long Gold { get; set; }

        /// <summary>
		/// 玩家升级所需经验
        /// <summary>
        public int ReqExp { get; set; }

        /// <summary>
		/// 宠物格子数
        /// <summary>
        public int GridNum { get; set; }

        /// <summary>
		/// 免转次数
        /// <summary>
        public int FreeNum { get; set; }

        /// <summary>
		/// 抽奖次数
        /// <summary>
        public int Loginawardnum { get; set; }

        /// <summary>
		/// 喂食状态0不可以1可以
        /// <summary>
        public int Feed { get; set; }

        /// <summary>
		/// 抚摸状态0不可以1可以
        /// <summary>
        public int Fondle { get; set; }

        /// <summary>
		/// 游戏豆版本万能豆数
        /// <summary>
        public long NewGold{ get; set; }


        /// <summary>
        /// 更新玩家的信息
        /// <summary>
		public void UpdatePlayerUserInfo(OGAckRoleLoginMsg user_info)
        {
            if (null == user_info)
            {
                return;
            }
            this.LoginSuccess   = true;
			this.RoleName = user_info.roleName;
            this.Level      = user_info.level;
            this.Exp      = user_info.exp;
            this.Gold          = user_info.gold;
            this.ReqExp         = user_info.reqExp;
            this.GridNum          = user_info.gridNum;
            this.FreeNum     = user_info.freeNum;
            this.Loginawardnum      = user_info.loginawardnum;
            this.Feed     = user_info.feed;
            this.Fondle    = user_info.fondle;
            this.NewGold        = user_info.newGold;
        }
		public void updateGlod(long glod)
		{
			//Debug.LogError("更新加过来的值："+glod);
			this.Gold = glod;
		}
		public void updateLeve(int leve,int exp)
		{
			this.Level=leve;
			this.Exp=exp;
		}
    }
}
