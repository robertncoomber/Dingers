using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public enum GameState { MainMenu, InGame, GameOver };

    public int ballStart;
    [Range(.1f, 5f)]
    public float pitchInterval;
    public BallSpawner ballSpawner;
    public Animator ZonesAnim;
    public Animator MenuAnim;
    
    GameState state;

    private void Awake()
    {
        Game.intervalSpeed = pitchInterval;
    }
    
    void Start ()
    {
        Game.isPaused = false;
        state = GameState.MainMenu;
        Game.ballsLeft = ballStart;
    }

    public void Pause()
    {
        if(state == GameState.InGame)
        {
            Game.isPaused = true;
            ballSpawner.CancelInvoke();
            Game.isPlaying = false;
            MenuAnim.SetTrigger("PauseMenuFadeIn");
        }
    }

    public void Restart()
    {
        state = GameState.InGame;
        CleanBalls();
        Game.score = 0;
        Game.isPlaying = true;
        Game.ballsLeft = ballStart;
        
        if (state == GameState.InGame)
        {
            MenuAnim.SetTrigger("PauseMenuFadeOut");
        }
        

        
                MenuAnim.SetTrigger("RestartMenuFadeOut");
        

        StartPitch();
    }

    public void Play()
    {
        state = GameState.InGame;
        Game.ballsLeft = ballStart;

        ZonesAnim.SetBool("ArcadeIsPlaying", true);
        MenuAnim.SetTrigger("MainMenuFadeOut");
        Game.isPlaying = true;

        StartPitch();
    }

    public void Continue()
    {
        Game.isPlaying = true;
        MenuAnim.SetTrigger("PauseMenuFadeOut");
        StartPitch();
    }

    public void Quit()
    {
        Debug.Log("Quit pause is called");
        MenuAnim.SetTrigger("PauseMenuFadeOut");
        Invoke("QuitTransition", 1f);
        state = GameState.MainMenu;
    }

    public void RestartQuit()
    {
        MenuAnim.SetTrigger("RestartMenuFadeOut");
        Invoke("QuitTransition", 1f);

        state = GameState.MainMenu;
    }

    private void QuitTransition()
    {
        MenuAnim.SetTrigger("MainMenuFadeIn");
    }

    public void GameOver()
    {
        state = GameState.GameOver;
        MenuAnim.SetTrigger("RestartMenuFadeIn");
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

    public void StartPitch()
    {
        InvokeRepeating("Toss", Game.intervalSpeed,Game.intervalSpeed);
    }

    public void Toss()
    {
        if(Game.ballsLeft > 1 && !Game.isPaused)
        {
            Game.ballsLeft -= 1;
            ballSpawner.SpawnBall();
        }
        else if(Game.ballsLeft == 1 && !Game.isPaused)
        {
            ballSpawner.SpawnBall();
            Invoke("GameOver", .1f);
            Game.ballsLeft = 0;

            CancelInvoke("Toss");
        }
        
    }
}
