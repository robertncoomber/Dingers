﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color32 normalColor;
    public Color32 hoverColor;
    public Color32 downColor;

    public SteamVR_Action_Vibration hapticAction;

    private Image m_Image = null;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
        //m_Image.sprite = normalImage;

        m_Image.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hapticAction.Execute(0, .01f, 150, 75, SteamVR_Input_Sources.RightHand);
        //m_Image.sprite = hoverImage;

        m_Image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //m_Image.sprite = normalImage;

        m_Image.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //m_Image.sprite = downImage;

        m_Image.color = downColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_Image.color = hoverColor;
    }


}
