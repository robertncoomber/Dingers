using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

    public List<Material> bats;
    Material batMat;
    int i = 0;
    int numBats;

    GameObject bat;

    private void Start()
    {
        bat = gameObject;
        numBats = bats.Count;
        batMat = GetComponent<Renderer>().material;
    }

    public void ChangeBat()
    {
        if(i == (numBats-1))
        {
            i = 0;
        }
        else
        {
            i++;
        }

        bat.GetComponent<Renderer>().material = bats[i];
        batMat = bats[i];
        
    }
}
