using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    ObjectPooler objectPooler;
    GameObject prePitch;

    private void Start()
    {
        prePitch = GameObject.Find("pitching light");

        objectPooler = ObjectPooler.Instance;

        InvokeRepeating("SpawnBall", Game.intervalSpeed, Game.intervalSpeed);
    }
    
    public void StartSpawn()
    {
        prePitch.SendMessage("isPlaying");
        CancelInvoke();
        InvokeRepeating("SpawnBall", Game.intervalSpeed, Game.intervalSpeed);
    }
    

    void SpawnBall()
    {
        if(Game.isPlaying)
        {
            if (Game.ballsLeft >= 1)
            {
                prePitch.SendMessage("BallSpawned");
                objectPooler.SpawnFromPool("ball", transform.position, Quaternion.identity);
                Game.ballsLeft -= 1;
            }
            else
            {
                CancelInvoke();// out of balls
            }
        }
        else
        {
            CancelInvoke(); // paused
        }
    }
}
