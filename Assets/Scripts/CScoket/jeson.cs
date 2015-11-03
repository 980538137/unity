using UnityEngine;
using System.Collections;

public class jeson : MonoBehaviour {
	static jeson instance;
	public static jeson Instance {
				get {
						if (instance == null) {
						instance = new jeson ();
						}
						return instance;
				}
		}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public IEnumerator Star(string path)
	{
		WWW www = new WWW (path);
		yield return www;
		Playerjes.Instance.getjes (www.text);
	}
}
