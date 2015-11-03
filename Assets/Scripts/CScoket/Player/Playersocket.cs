using UnityEngine;
using System.Collections;

public class Playersocket : Framework.Singleton<Playersocket> {
	
	/// <summary>
	/// IP
	/// <summary>
	public string  IP{ get; set;}
	
	/// <summary>
	/// Port
	/// <summary>
	public int  Port { get; set; }

	public void socketP(string ip,int port)
	{
		this.IP = ip;
		this.Port = port;
	}
}
