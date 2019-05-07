using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    ObjectPooler objectPooler;

    public GameController gameController;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    public void SpawnBall()
    {
        objectPooler.SpawnFromPool("ball", transform.position, Quaternion.identity);
    }
}
