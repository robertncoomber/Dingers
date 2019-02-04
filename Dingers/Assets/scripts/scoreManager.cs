using UnityEngine.UI;
using UnityEngine;

public class scoreManager : MonoBehaviour {

	public Text score;
	public static int scoreNum;

	void Awake()
	{
		score = GetComponent<Text> ();
		scoreNum = 0;
	}

	void Update () {
		score.text = scoreNum.ToString();
	}


	public void RestartScore()
	{
		scoreNum = 0;
	}


}
