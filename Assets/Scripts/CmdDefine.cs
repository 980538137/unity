using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Network.Cmd.Protocol
{
    public class CmdDefine
    {
        #region 登录游戏，进入房间，获取奖励等相关消息定义

        public const UInt32 OGID_REQ = 0x00000000;		//请求消息类型

        public const UInt32 OGID_ACK = 0x08000000;		//应答消息类型

		public const UInt32 GATE_MESSAGE_BASE_ID       = (UInt32)134217728;

       /*!
        * \brief 用户登录到Gate
        * \remarks 对应于 LoginReq 和 LoginAck 消息，这两个消息的id号分别是 OGID_REQ | GATE_LOGIN_REQ 和 OGID_ACK | GATE_LOGIN_REQ 。
        */
        public const UInt32 GATE_LOGIN_REQ            = GATE_MESSAGE_BASE_ID + 0x01; 

       /*!
        * \brief 获取房间列表
        * \remarks 对应于 GetRoomListReq 和 GetRoomListAck 消息，这两个消息的id号分别是 OGID_REQ | GATE_GET_ROOM_LIST_REQ 和 OGID_ACK | GATE_GET_ROOM_LIST_REQ
        */
        public const UInt32 GATE_GET_ROOM_LIST_REQ    = GATE_MESSAGE_BASE_ID + 0x02; 

       /*!
        * \brief 获取房间动态信息
        * \remarks 对应于 GetRoomRunInfoReq 和 GetRoomRunInfoAck 消息，这两个消息的id号分别是　OGID_REQ | GATE_GET_ROOM_RUN_INFO_REQ 和 OGID_ACK | GATE_GET_ROOM_RUN_INFO_REQ
        */
        public const UInt32 GATE_GET_ROOM_RUN_INFO_REQ = GATE_MESSAGE_BASE_ID + 0x03; 

       /*!
        * \brief 请求进入房间
        * \remarks 对应于 EnterRoomReq 和 EnterRoomAck 消息，这两个消息的id号分别是 OGID_REQ | GATE_ENTER_ROOM_REQ 和 OGID_ACK | GATE_ENTER_ROOM_REQ
        */
        public const UInt32 GATE_ENTER_ROOM_REQ = GATE_MESSAGE_BASE_ID + 0x04;

       /*!
        * \brief 请求离开房间
        */
        public const UInt32 GATE_LEAVE_ROOM_REQ = GATE_MESSAGE_BASE_ID + 0x05;

       /*!
        * \brief 免冲消息
        * \remarks 对应于 FreeCoinsReq 、FreeCoinsNtf 、FreeCoinsNtf消息\n
        */
        public const UInt32 GATE_FREE_COINS = GATE_MESSAGE_BASE_ID + 0x06;

       /*!
        * \brief 服务器通知玩家坐下
        * \remarks 对应于 SitDownNtf 消息
        */
        public const UInt32 GATE_SIT_DOWN_NOTIFY = GATE_MESSAGE_BASE_ID + 0x07;

       /*!
        * \brief 服务器通知玩家站起
        * \remarks 对应于 StandUpNtf 消息
        */
        public const UInt32 GATE_STAND_UP_NOTIFY = GATE_MESSAGE_BASE_ID + 0x08;

       /*!
        * \brief 玩家准备
        * \remarks 对应于 PlayerReadyReq 和 PlayerReadyAck 消息，这两个消息的id号分别是 OGID_REQ | GATE_PLAYER_READY 和 OGID_ACK | GATE_PLAYER_READY
        */
        public const UInt32 GATE_PLAYER_READY = GATE_MESSAGE_BASE_ID + 0x09;

       /*!
        * \brief 玩家托管游戏
        * \remarks 对应于 TrustPlayReq / TrustPlayAck / TrustPlayNtf 消息，这几个消息的id号分别是 OGID_REQ | GATE_PLAYER_TRUST_PLAY , OGID_ACK | GATE_PLAYER_TRUST_PLAY , OGID_REQ | GATE_PLAYER_TRUST_PLAY
        */
        public const UInt32 GATE_PLAYER_TRUST_PLAY = GATE_MESSAGE_BASE_ID + 0x0A;

       /*!
        * \brief 玩家聊天
        * \remarks 对应于 ChatReq / ChatAck / ChatNtf 消息，这几个消息的id号分别是 OGID_REQ | GATE_PLAYER_CHAT , OGID_ACK | GATE_PLAYER_CHAT , OGID_REQ | GATE_PLAYER_CHAT
        */
        public const UInt32 GATE_PLAYER_CHAT = GATE_MESSAGE_BASE_ID + 0x0B;

       /*!
        * \brief 分配座位号
        * \remarks 这个消息是在玩家坐下时下发的，会比SitDownNtf消息要早
        */
		public const UInt32 GATE_ALLOT_SEAT = GATE_MESSAGE_BASE_ID + (UInt32)61;

       /*!
        * \brief 刷新用户信息
        */
		public const UInt32 GATE_REFRESH_USER_INFO = GATE_MESSAGE_BASE_ID + (UInt32)60;

       /*!
        * \brief 更新用户信息
        */
        public const UInt32 GATE_UPDATE_USER_INFO = GATE_MESSAGE_BASE_ID + (UInt32)61;

       /*!
        * \
        */
		public const UInt32 GATE_PURCHASE         = GATE_MESSAGE_BASE_ID +(UInt32)59;

       /*!
        * \brief
        */
		public const UInt32 GATE_GET_AVAILABLE_ROOM   = GATE_MESSAGE_BASE_ID + (UInt32)13;

       /*!
        * \brief 返回游戏结果
        */
        public const UInt32 GATE_CHANGE_USER_IMAGE    = GATE_MESSAGE_BASE_ID + (UInt32)50;

       /*!
        * \brief 心跳消息
        */
		public const UInt32 GATE_HEART_BEAT           = GATE_MESSAGE_BASE_ID + (UInt32)51;

       /*!
        * \brief 房间列表
        */
		public const UInt32 GATE_KICK_OUT             = GATE_MESSAGE_BASE_ID + (UInt32)3;

       /*!
        * \brief 比倍收分消息
        */
		public const UInt32 GATE_CHECK_IN	            = GATE_MESSAGE_BASE_ID + (UInt32)53;

       /*!
        * \brief 获取比倍结果
        */
		public const UInt32 GATE_USER_ACHIEVEMENT	    = GATE_MESSAGE_BASE_ID + (UInt32)52;

       /*!
        * \brief 关闭按钮
        */
		public const UInt32 GATE_TAKE_ACHIEVEMENT	    = GATE_MESSAGE_BASE_ID + (UInt32)15;

       /*!
        * \brief  登录信息
        */
		public const UInt32 GATE_REACH_ACHIEVEMENT    = GATE_MESSAGE_BASE_ID + (UInt32)1;

       /*!
        * \brief 玩家信息
        */
		public const UInt32 GATE_TAKE_NORMAL_AWAR     = GATE_MESSAGE_BASE_ID + (UInt32)2;

       /*!
        * \brief 房间配置消息
        */
		public const UInt32 GATE_BUY_MEMBERSHIP       = GATE_MESSAGE_BASE_ID + (UInt32)4;


		public const UInt32 GATE_BUY_MEMBERSTOP       = GATE_MESSAGE_BASE_ID + (UInt32)57;

        #endregion



        #region 游戏进行中相关消息定义

        public const UInt32 DZMAHJONG_GAME_MESSAGE_BASE_ID = 0x00005D00;

        /*!
         * \brief 游戏开始
         * \remarks 服务端下发REQ的游戏开始消息给客户端，客户端待初始化完毕后发送ACK的回复消息给服务端
         */
        public const UInt32 DZMAHJONG_GAME_START = DZMAHJONG_GAME_MESSAGE_BASE_ID + 0x01;

        /*!
         * \brief 激活玩家
         * \remarks 当需要某个玩家执行操作时，服务端下发REQ的消息给玩家
         */
        public const UInt32 DZMAHJONG_PLAYER_ACTIVE = DZMAHJONG_GAME_MESSAGE_BASE_ID + 0x02;

        /*!
         * \brief 激活玩家采取的操作
         * \remarks 客户端先上发REQ的操作给服务端，然后服务端再广播这个REQ消息给所有玩家
         */
        public const UInt32 DZMAHJONG_PLAYER_ACTION = DZMAHJONG_GAME_MESSAGE_BASE_ID + 0x03;

        /*!
         * \brief 游戏中，玩家金币变化后，服务端的通知
         * \remarks 服务端下发REQ的通知消息告知客户端玩家金币变化情况
         */
        public const UInt32 DZMAHJONG_COIN_CHANGE_NOTIFICATION = DZMAHJONG_GAME_MESSAGE_BASE_ID + 0x05;

        /*!
         * \brief 游戏结果消息
         * \remarks 服务端下发REQ的游戏结果消息给客户端
         */
        public const UInt32 DZMAHJONG_GAME_RESULT = DZMAHJONG_GAME_MESSAGE_BASE_ID + 0x06;

        /*!
         * \brief 断线续玩玩家收到的游戏数据
         * \remarks 服务端下发REQ的游戏同步数据消息给断线续玩回来的玩家
         */
        public const UInt32 DZMAHJONG_REENTER_GAME_DATA = DZMAHJONG_GAME_MESSAGE_BASE_ID + 0x07;

        // 客户端针对Action消息的动画执行完毕，通知服务端可以下发下一个Active消息 client->server ( OGID_REQ | MessageId_AnimationFinish )
        public const UInt32 MessageId_AnimationFinish = 0x00005D08;

        // 服务端要玩家放弃某些操作 server->all clients ( OGID_REQ | MessageId_PlayerActionReport )
        public const UInt32 MessageId_PlayerActionReport = 0x00005D04;

        #endregion


    }
}
