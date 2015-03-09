using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Info : MonoBehaviour {

	public Text infoText;

	// Use this for initialization
	void Start () {

		string infoString = "Level: " + Application.loadedLevelName +
			"\nPlatform: " + Application.platform;

		infoText.text = infoString;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
	}
}
