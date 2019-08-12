using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;


public class VRInputModule : BaseInputModule
{

    public Camera m_Camera;
    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_ClickAction;
    public SteamVR_Action_Boolean m_PauseAction;

    [SerializeField]
    private GameController gameController;

    private GameObject m_CurrentObject = null;
    private PointerEventData m_Data = null;


    protected override void Awake()
    {
        base.Awake();

        m_Data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        // Reset data, set camera
        m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);

        // Raycast 
        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

        // Clear Raycast
        m_RaycastResultCache.Clear();

        // Hover 
        HandlePointerExitAndEnter(m_Data, m_CurrentObject);

        // Menu Interact Press
        if (m_ClickAction.GetStateDown(m_TargetSource))
            ProcessMenuPress(m_Data);

        // Menu Interact Release
        if (m_ClickAction.GetStateUp(m_TargetSource))
            ProcessMenuRelease(m_Data);

        // Pause Menu Interact Press
        if (m_PauseAction.GetStateDown(m_TargetSource))
            ProcessPausePress();
  
    }

    public PointerEventData GetData()
    {
        return m_Data;
    }

    private void ProcessMenuPress(PointerEventData data)
    {
        // set raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        // check for object hit, get down handler, call
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);

        // if no down handler, try and get click handler
        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        // set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_CurrentObject;

        //This was in below if statement, called upon release, we want on click...
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
    }

    private void ProcessMenuRelease(PointerEventData data)
    {
        // Execute Pointer up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // check for click handler
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        // check if actual
        if (data.pointerPress == pointerUpHandler)
        {
            //ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler); // moved above
        }

        // clear selected gameObject
        eventSystem.SetSelectedGameObject(null);

        // reset
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }

    private void ProcessPausePress()
    {
        gameController.Pause();
    }
}
