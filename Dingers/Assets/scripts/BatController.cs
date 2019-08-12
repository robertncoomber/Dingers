using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatController : MonoBehaviour
{

    public List<Bat> bats;
    private int maxBatIndex;
    Material currentMat;
    int currentBat = 0;
    bool canHitSound = true;

    private void Start()
    {
        ChangeBat(0);
    }

    public void ChangeBat(int currentBatIndex)
    {
        currentBat = currentBatIndex;
        gameObject.GetComponent<Renderer>().material = bats[currentBatIndex].batMaterial;
        gameObject.GetComponent<CapsuleCollider>().material = bats[currentBatIndex].batPhysicsMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ball" && canHitSound)
        {
            float relativeVelocity = collision.impulse.magnitude;
            float volume = relativeVelocity / 20;
            canHitSound = false;
            Invoke("DoubleHitCheckReset", 0.5f);
        }
    }

    private void DoubleHitCheckReset()
    {
        canHitSound = true;
    }
}