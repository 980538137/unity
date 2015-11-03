using UnityEngine;
using System.Collections;

public class Playerjes : Framework.Singleton<Playerjes> {

	public string jes;
	public void getjes(string j)
	{
		this.jes = j;
	}
}
