using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		Button btn = GetComponent<Button> ();
//		btn.onClick.AddListener (OnClick2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick()
	{
		Debug.Log("Button Clicked ");
	}

	private void OnClick2()
	{
		Debug.Log ("Button Clicked  2");
	}
}
