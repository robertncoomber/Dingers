using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatController : MonoBehaviour
{

    public List<Bat> bats;
    private int maxBatIndex;
    public Text batName;
    Material currentMat;
    int currentBat = 0;
    public Animator animator;
    public AudioSource batAudio;
    bool canHitSound = true;

    GameObject bat;

    private void Start()
    {
        bat = gameObject;
        ChangeBat(0);
    }

    public void ChangeBat(int currentBatIndex)
    {
        currentBat = currentBatIndex;
        bat.GetComponent<Renderer>().material = bats[currentBatIndex].batMaterial;
        bat.GetComponent<CapsuleCollider>().material = bats[currentBatIndex].batPhysicsMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ball" && canHitSound)
        {
            float relativeVelocity = collision.impulse.magnitude;
            float volume = relativeVelocity / 20;
            //bats[currentBat].batSound.volume = volume;
            //bats[currentBat].batSound.Play();
            canHitSound = false;
            Invoke("DoubleHitCheck", 0.5f);
        }
    }

    private void DoubleHitCheck()
    {
        canHitSound = true;
    }
}