using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public int ballStart = 20;
    [Range(.1f, 5f)]
    public float pitchInterval;
    public BallSpawner ballSpawner;
    public Animator ZonesAnim;
    public Animator MenuAnim;

    private void Awake()
    {
        Game.intervalSpeed = pitchInterval;
    }
    
    void Start ()
    {
        Game.ballsLeft = ballStart;
	}
    

    public void Pause()
    {
        Game.isPlaying = false;
        MenuAnim.SetTrigger("PauseMenuFadeIn");
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
            ZonesAnim.SetBool("ArcadeIsPlaying", true);
            MenuAnim.SetTrigger("MainMenuFadeOut");
            Game.isPlaying = true;
            ballSpawner.StartSpawn();
        }
        
    }

    public void StartPractice()
    {
        CleanBalls();
        MenuAnim.Play("MenuFadeOut");
        Game.isPlaying = true;
        ballSpawner.StartSpawn();
    }

    public void GameOver()
    {
        MenuAnim.SetTrigger("RestartMenuFadeIn");
        Debug.Log("Game is Over");
    }

    public void CleanBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
        foreach (GameObject ball in balls)
        {
            ball.SetActive(false);
        }
    }

    public void MenuButtonPressed()
    {
        if (Game.isPlaying == true)
            Pause();
    }

}
