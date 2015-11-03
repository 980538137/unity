using UnityEngine;
using System.Collections;

public class player:  Framework.Singleton<player> {

	public string  pay{ get; set;}

	public string IP{ get; set;}
	public bool Room{ get; set;}
	
	public void getpay(string p)
	{
		this.pay = p;
	}
	public void getIp(string ip)
	{
		this.IP = ip;
	}
	public void getRoom(bool room)
	{
		this.Room = room;
	}
}