using UnityEngine;
using System.Collections;
using Assets.Scripts.Network;
using com.ourgame.texasSlots;
using System;

public class HitClient : MonoBehaviour {

	string IP="127.0.0.1";
	int Port=8888;
	// Use this for initialization
	void Start () {
		SystemSocket.Instance.Start (IP,Port);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
