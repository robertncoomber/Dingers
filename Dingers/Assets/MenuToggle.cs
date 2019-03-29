using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MenuToggle : MonoBehaviour
{

    public VRTK_Pointer pointer;
    public VRTK_StraightPointerRenderer straightPointerRenderer;
    public VRTK_ControllerEvents controllerEvents;
    //public GameObject menu;

    public bool menuState = true;

    void OnEnable()
    {
        controllerEvents.ButtonTwoReleased += ControllerEvents_ButtonTwoReleased;
    }

    void OnDisable()
    {
        controllerEvents.ButtonTwoReleased -= ControllerEvents_ButtonTwoReleased;
    }

    private void ControllerEvents_ButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
    {
        if(menuState == false)
        {
            menuState = true;
            pointer.enabled = true;
            pointer.pointerRenderer.enabled = true;
        }
    }

    public void enablePointer()
    {
        menuState = true;
        pointer.enabled = true;
        pointer.pointerRenderer.enabled = true;
    }

    public void disablePointer()
    {
        menuState = false;
        pointer.enabled = false;
        pointer.pointerRenderer.enabled = false;
    }
}
