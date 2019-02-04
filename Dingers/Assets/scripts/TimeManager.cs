using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	public Text timeDisplay;
	private float timer =  121f;
	private string minutes, seconds;
	private bool isPaused = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( !isPaused )
		{
		timer -= Time.deltaTime;
		minutes = Mathf.Floor(timer / 60).ToString("0");
		seconds = Mathf.Floor(timer % 60).ToString("00");
		timeDisplay.text = minutes + ":" + seconds;
		//Debug.Log(minutes + seconds + timer);
		}

		if (timer <= 0 && !isPaused) {
			timeDisplay.text = "0:00";
		}

	}

	public void PlayGame()
	{
		isPaused = false;
	}

	public void PauseGame()
	{
		isPaused = true;
	}

	public void RestartTimer()
	{
		timer = 121;
		isPaused = false;
	}
}
