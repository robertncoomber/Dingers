using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static int ballStart = 20;
    [Range(.1f, 5f)]
    public float pitchInterval;
    public BallSpawner ballSpawner;

    private void Awake()
    {
        print(Game.gameHasEnded);
        Game.intervalSpeed = pitchInterval;
    }
    
    void Start ()
    {
        Game.ballsLeft = ballStart;
	}
    

    public void Pause()
    {
        Game.isPlaying = false;
    }

    public void Restart()
    {
        CleanBalls();
        Game.score = 0;
        Game.isPlaying = true;
        Game.ballsLeft = ballStart;
        ballSpawner.StartSpawn();
    }

    public void Play()
    {
        if(Game.gameHasEnded)
        {
            Restart();
            Game.gameHasEnded = false;
        }
        else
        {
            Game.isPlaying = true;
            ballSpawner.StartSpawn();
        }
        
    }

    public void CleanBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
        foreach (GameObject ball in balls)
        {
            ball.SetActive(false);
        }
    }

}
