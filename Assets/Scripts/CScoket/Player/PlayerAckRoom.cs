using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using com.ourgame.texasSlots;

public class PlayerAckRoom : Framework.Singleton<PlayerAckRoom> {

	/// <summary>
	/// 房间id
	/// <summary>
	public Int32 roomId{ get; set;}
	
	/// <summary>
	/// 房间名称
	/// <summary>
	public string  roomName { get; set; }
	/// <summary>
	/// 最大线数
	/// <summary>
	public Int32  roomMaxLine { get; set; }
	
	/// <summary>
	///最大倍数
	/// <summary>
	public Int32 roomMaxTimes { get; set; }
	/// <summary>
	/// 底金
	/// <summary>
	public Int64 roomBaseGold{ get; set;}
	
	/// <summary>
	/// 房间图片
	/// <summary>
	public Int32  roomPic { get; set; }
	/// <summary>
	/// 比倍开关
	/// <summary>
	public Int32  roomDoubleOn { get; set; }
	
	/// <summary>
	///免转开关
	/// <summary>
	public Int32 freeOn { get; set; }
	/// <summary>
	/// 四倍开关
	/// <summary>
	public Int32  fourTimesOn { get; set; }
	
	/// <summary>
	///免费次数
	/// <summary>
	public Int32 freeTimes { get; set; }
	/// <summary>
	///数
	/// <summary>
	public Int32 bonusStar{ get; set;}
	/// <summary>
	///宝藏值
	/// <summary>
	public Int64 pot{ get; set;}
	/// <summary>
	///宝藏百分比
	/// <summary>
	public Int32 percent{ get; set;}
	/// <summary>
	///免费的时候每条线压的万能豆数
	/// <summary>
	public Int32 lastbetUnit{ get; set;}
	/// <summary>
	///免费的时候压了多少条线
	/// <summary>
	public Int32 lastbetLineNum{ get; set;}
	/// <summary>
	///
	/// <summary>
	public List<OGAckRoom.RoomBet> betList { get; set; }

	public void UpdateAckroom(OGAckRoom room)
	{
		this.roomId = room.roomId;
		this.roomName = room.roomName;
		this.roomMaxLine = room.roomMaxLine;
		this.roomMaxTimes = room.roomMaxTimes;
		this.roomBaseGold = room.roomBaseGold;
		this.roomPic = room.roomPic;
		this.roomDoubleOn = room.roomDoubleOn;
		this.freeOn = room.freeOn;
		this.fourTimesOn = room.fourTimesOn;
		this.freeTimes = room.freeTimes;
		this.bonusStar = room.bonusStar;
		this.pot = room.pot;
		this.percent = room.percent;
//		Debug.LogError (room.percent);
		this.lastbetUnit = room.lastbetUnit;
		this.lastbetLineNum = room.lastbetLineNum;
		this.betList = room.betList;
		ResultInfoDescent sd = new ResultInfoDescent();
		this.betList.Sort(sd);
	}
	public class ResultInfoDescent : IComparer<OGAckRoom.RoomBet>
	{
		public int Compare(OGAckRoom.RoomBet x, OGAckRoom.RoomBet y)
		{
			return x.bet.CompareTo (y.bet);
		}
	}

}
