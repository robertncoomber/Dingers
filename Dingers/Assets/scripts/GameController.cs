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
    public Animator MenuAnim;
    public GameObject pointer; // whether it be the entire object or just line and 
    private Pointer controllerPointer;

    GameState state;

    private void Awake()
    {
        Game.intervalSpeed = pitchInterval;
        controllerPointer = pointer.GetComponent<Pointer>();
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
            //pointer.setActive(true);
            CancelInvoke("Toss");
            Game.isPaused = true;
            ballSpawner.CancelInvoke();
            Game.isPlaying = false;
            MenuAnim.SetTrigger("PauseMenuFadeIn");
            controllerPointer.ReturnLine();
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
            MenuAnim.SetTrigger("InGameNoMenu");
        }

        StartPitch();

        controllerPointer.RemoveLine();
    }

    public void Play()
    {
        //pointer.SetActive(false);
        state = GameState.InGame;
        Game.ballsLeft = ballStart;
        MenuAnim.SetTrigger("InGameNoMenu");
        Game.isPlaying = true;

        StartPitch();

        controllerPointer.RemoveLine();
    }

    public void Continue()
    {
        Game.isPlaying = true;
        MenuAnim.SetTrigger("InGameNoMenu");
        StartPitch();
        controllerPointer.RemoveLine();
    }

    public void Quit()
    {
        Debug.Log("Quit pause is called");
        MenuAnim.SetTrigger("MainMenu");
        state = GameState.MainMenu;
    }

    public void GameOver()
    {
        state = GameState.GameOver;
        Game.isPlaying = false;
        MenuAnim.SetTrigger("GameOverMenu");

        controllerPointer.ReturnLine();
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
        {
            Game.isPlaying = false;
            Pause();
            MenuAnim.SetTrigger("PauseMenu");
        }
    }

    public void StartPitch()
    {
        InvokeRepeating("Toss", Game.intervalSpeed,Game.intervalSpeed);
        Debug.Log("StartPitch is called");
    }

    public void Toss()
    {
        if(Game.ballsLeft > 1 && Game.isPlaying)
        {
            Game.ballsLeft -= 1;
            ballSpawner.SpawnBall();
        }
        else if(Game.ballsLeft == 1 && Game.isPlaying)
        {
            ballSpawner.SpawnBall();
            Invoke("GameOver", 2.5f);
            Game.ballsLeft = 0;

            CancelInvoke("Toss");
        }
    }
}
