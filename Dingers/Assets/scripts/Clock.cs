using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {
    
    public Text ballsText;
    public Text[] scoreText;
    
    // Update is called once per frame
    void Update () {
        ballsText.text = Game.ballsLeft.ToString();
        foreach(Text item in scoreText)
        {
            item.text = Game.score.ToString();
        }
	}
}
