using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour, IPooledObjects {
    
    public float pitchSpeed = 8f;


    public class BallStats
    {
        public bool contact, strike, inPlay, foul, homeRun;

        public BallStats(bool contact)
        {
            contact = false;
        }
    }

    public BallStats bs = new BallStats(false);

	// Use this for initialization
	public void OnObjectSpawn () {

        //gameObject.transform.Rotate(0, 45f, 0)

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
                Game.score += 3; // only result is homerun.
                break;
            case "Foul":
                if ( bs.foul || bs.strike || bs.inPlay || bs.homeRun)
                    break;

                bs.foul = true;
                bs.strike = true;
                break;
            case "Bat": // make sure its a fresh ball
                if ( bs.strike || bs.foul || bs.inPlay || bs.homeRun ) 
                    break;

                bs.contact = true;
                break;
            case "Inplay": // check to make sure only contact had been made
                if (bs.strike || bs.foul || bs.homeRun || bs.inPlay || !bs.contact)
                    break;
                
                bs.inPlay = true;
                Game.score += 1;
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
