using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static int ballStart = 20;
    [Range(.1f, 5f)]
    public float pitchInterval;
    public BallSpawner ballSpawner;
    public Slider slider;

    private void Awake()
    {
        Game.intervalSpeed = pitchInterval;
    }

    // Use this for initialization
    void Start ()
    {
        //Game.isPlaying = true;
        Game.ballsLeft = ballStart;
	}
    

    public void Pause()
    {
        Game.isPlaying = false;
    }

    public void Restart()
    {
        Game.isPlaying = true;
        Game.ballsLeft = ballStart;
        ballSpawner.StartSpawn();
    }

    public void Play()
    {
        Game.isPlaying = true;
        ballSpawner.StartSpawn();
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
