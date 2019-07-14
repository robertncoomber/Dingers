using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour {

    public bool isPlaying;

    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;

    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update () {
        if (!isPlaying)
            UpdateLine();
        else
            RemoveLine();
	}

    private void UpdateLine()
    {
        PointerEventData data = m_InputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;

        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if (hit.collider != null)
            endPosition = hit.point;

        m_Dot.transform.position = endPosition;

        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    public void RemoveLine()
    {
        Debug.Log("Remove Line Called");
        if (m_LineRenderer.startColor != Color.clear)
        {
            m_LineRenderer.startColor = Color.clear;
            m_Dot.SetActive(false);
        }
    }

    public void ReturnLine()
    {
        if (m_LineRenderer.startColor != Color.white)
        {
            m_LineRenderer.startColor = Color.white;
            m_Dot.SetActive(true);
        }
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);
        
        return hit;
    }
}
