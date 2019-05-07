using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SwitchHands : MonoBehaviour {

    public GameObject leftBox;
    public GameObject rightBox;
    private bool isTeleporting = false;

    private bool currentHand = true; //right hand is the true hand
    

    public void TryTeleport(bool hand)
    {
        if (currentHand == hand)
            return;

        if (isTeleporting)
            return;
        
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translateVector = rightBox.transform.position - groundPosition;

        currentHand = true;
        if (!hand) // left hand
        {
            currentHand = false;
            translateVector = leftBox.transform.position - groundPosition;
        }


        StartCoroutine(ChangeBox(cameraRig, translateVector));
    }

    private IEnumerator ChangeBox(Transform cameraRig, Vector3 translation)
    {
        isTeleporting = true;

        SteamVR_Fade.Start(Color.black, .3f, true);

        yield return new WaitForSeconds(.3f);
        cameraRig.position += translation;

        SteamVR_Fade.Start(Color.clear, .3f, true);

        isTeleporting = false;
    }
}
