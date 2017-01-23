using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	private MapManager currMapManager;

	// Use this for initialization
	void Start () 
	{
		currMapManager = MapManager.instance;
		currMapManager.InitialzeMap ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.15f, Screen.width * 0.15f, Screen.height * 0.15f), "SelectMap"))
		{
			currMapManager.SelectMap(15);
		}
		if(GUI.Button(new Rect(Screen.width * 0.45f, Screen.height * 0.15f, Screen.width * 0.15f, Screen.height * 0.15f), "GenerateMap"))
		{
			currMapManager.GenerateMap ();
		}
		if(GUI.Button(new Rect(Screen.width * 0.85f, Screen.height * 0.15f, Screen.width * 0.15f, Screen.height * 0.15f), "ResetAll"))
		{
			currMapManager.ResetMap ();
		}
	}
	

}
