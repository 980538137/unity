using UnityEngine;
using System.Collections;
using System;
using com.ourgame.texasSlots;
using System.Collections.Generic;

public class PlayerRoomManager : Framework.Singleton<PlayerRoomManager> {
	/// <summary>
	/// 房间列表
	/// <summary>
	public List<OGAckRoomList.Room> room { get; set;}


	//房间列表
	public void RoomList(OGAckRoomList Listroom)
	{
		this.room = Listroom.roomList;
	}
}
