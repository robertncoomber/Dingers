using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWVR_InteractionObject : MonoBehaviour {

    protected Transform cachedTransform;
    [HideInInspector]
    public RWVR_InteractionController currentController;

    public virtual void OnTriggerWasPressed(RWVR_InteractionController controller)
    {
        currentController = controller;
    }

    public virtual void OnTriggerIsBeingPressed(RWVR_InteractionController controller)
    {

    }

    public virtual void OnTriggerWasReleased(RWVR_InteractionController controller)
    {
        currentController = null;
    }

    public virtual void Awake()
    {
        cachedTransform = transform;
        if(!gameObject.CompareTag("InteractionObject"))
        {
            Debug.LogWarning("This InteractionObject does not ahve the correct tag, setting it now.", gameObject);
            gameObject.tag = "InteractionObject";
        }
    }

    public bool IsFree()
    {
        return currentController == null;
    }

    public virtual void OnDestroy()
    {
        if(currentController)
        {
            OnTriggerWasReleased(currentController);
        }
    }
}
