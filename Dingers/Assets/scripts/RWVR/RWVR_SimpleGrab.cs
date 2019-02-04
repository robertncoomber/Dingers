using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWVR_SimpleGrab : RWVR_InteractionObject {

    public bool hideControllerModelOnGrab;
    private Rigidbody rb;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    private void AddFixedJointToController(RWVR_InteractionController controller) // 1
    {
        FixedJoint fx = controller.gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        fx.connectedBody = rb;
    }

    private void RemoveFixedJointFromController(RWVR_InteractionController controller) // 2
    {
        if (controller.gameObject.GetComponent<FixedJoint>())
        {
            FixedJoint fx = controller.gameObject.GetComponent<FixedJoint>();
            fx.connectedBody = null;
            Destroy(fx);
        }
    }

    public override void OnTriggerWasPressed(RWVR_InteractionController controller) // 1
    {
        base.OnTriggerWasPressed(controller); // 2

        if (hideControllerModelOnGrab) // 3
        {
            controller.HideControllerModel();
        }

        AddFixedJointToController(controller); // 4
    }

    public override void OnTriggerWasReleased(RWVR_InteractionController controller) // 1
    {
        base.OnTriggerWasReleased(controller); //2

        if (hideControllerModelOnGrab) // 3
        {
            controller.ShowControllerModel();
        }

        rb.velocity = controller.velocity; // 4
        rb.angularVelocity = controller.angularVelocity;

        RemoveFixedJointFromController(controller); // 5
    }

}
