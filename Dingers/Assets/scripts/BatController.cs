using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatController : MonoBehaviour
{

    public List<Bat> bats;
    private int maxBatIndex;
    private int currentBatIndex;
    public Text batName, batAtt1, batAtt2, batAtt3, nickName;
    Material currentMat;

    public Animator animator;
    

    GameObject bat;

    private void Start()
    {
        bat = gameObject;
        maxBatIndex = bats.Count;
        currentBatIndex = 0;
        bat.GetComponent<Renderer>().material = bats[currentBatIndex].batMaterial;
        batName.text = bats[currentBatIndex].batName;
        batAtt1.text = bats[currentBatIndex].attribute1;
        batAtt2.text = bats[currentBatIndex].attribute2;
        batAtt3.text = bats[currentBatIndex].attribute3;
        nickName.text = bats[currentBatIndex].nickName;
        bat.GetComponent<CapsuleCollider>().material = bats[currentBatIndex].batPhysicsMaterial;
    }

    public void ChangeBat()
    {
        if (currentBatIndex == (maxBatIndex - 1))
        {
            currentBatIndex = 0;
        }
        else
        {
            currentBatIndex++;
        }
        
        bat.GetComponent<Renderer>().material = bats[currentBatIndex].batMaterial;
        batName.text = bats[currentBatIndex].batName;
        batAtt1.text = bats[currentBatIndex].attribute1;
        batAtt2.text = bats[currentBatIndex].attribute2;
        batAtt3.text = bats[currentBatIndex].attribute3;
        nickName.text = bats[currentBatIndex].nickName;
        bat.GetComponent<CapsuleCollider>().material = bats[currentBatIndex].batPhysicsMaterial;

        animator.SetTrigger("Change Bat");
    }
}