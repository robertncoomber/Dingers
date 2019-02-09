using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Script controls the balls state and the ball logic while it is in play

public class ball : MonoBehaviour, IPooledObjects {
    
    public float pitchSpeed;
    BallAnimation ballAnimation;

    private void Awake()
    {
        ballAnimation = GetComponent<BallAnimation>();
    }

    public class BallStats
    {
        public bool contact, strike, inPlay, foul, homeRun; // current state of ball

        public BallStats(bool contact)
        {
            contact = false;
        }
    }

    public BallStats bs = new BallStats(false);

    // When the each ball is being spawned into the scene
	public void OnObjectSpawn () { 

        Rigidbody rb = new Rigidbody();
        rb = GetComponent<Rigidbody>();

        rb.transform.Rotate(-30f, Random.Range(133f, 137f), 0);
        rb.velocity = transform.forward * pitchSpeed;
        ResetBallStats();
	}

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        BallLogic(tag);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        BallLogic(tag);
    }

    public void BallLogic(string tag)
    {
        switch (tag) // checks which tag the ball interacts with. 
        {
            case "Homerun":
                if (bs.homeRun || bs.foul || bs.strike || !bs.contact)
                    break;
                if (bs.inPlay) // ground rule double
                {
                    Game.score += 1;
                    bs.homeRun = true;
                    break;
                }

                bs.homeRun = true;
                ballAnimation.HomeRun();
                Game.score += 3;
                break;

            case "Foul":
                if ( bs.foul || bs.strike || bs.inPlay || bs.homeRun)
                    break;

                bs.foul = true;
                bs.strike = true;
                break;

            case "Bat": // make sure its a fresh ball
                if ( bs.strike || bs.foul || bs.inPlay || bs.homeRun || bs.contact ) 
                    break;

                bs.contact = true;
                break;

            case "Inplay": // check to make sure only contact had been made
                if (bs.strike || bs.foul || bs.homeRun || bs.inPlay || !bs.contact)
                    break;

                bs.inPlay = true;
                Game.score += 1;
                ballAnimation.InPlay();
                Debug.Log("Inplay Score is: " + Game.score);
                break;

            case "Strike":
                if (bs.contact || bs.strike || bs.foul || bs.homeRun || bs.inPlay) // foul ball
                    break;
                else // true strike
                {
                    bs.strike = true;
                    Game.score -= 1;
                    break;
                }

            default: // non point scoring surface was touched
                break;
        }
    }

    public void ResetBallStats()
    {
        bs.contact = false;
        bs.strike = false;
        bs.inPlay = false;
        bs.foul = false;
        bs.homeRun = false;
    }
}
