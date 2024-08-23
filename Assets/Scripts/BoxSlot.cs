using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class BoxSlot : MonoBehaviour
{
    public TMP_Text boxText;
    public GameObject boxFrame;
    private RectTransform rectTransform;
    private Vector2 initAnchoredPosition;
    private Image frameImage;
    private void Awake()
    {
       
        rectTransform = GetComponent<RectTransform>();
        initAnchoredPosition = rectTransform.anchoredPosition; // Lưu giá trị ban đầu của anchoredPosition
    }
    public void EmptyBox()
    {
        frameImage = boxFrame.GetComponent<Image>();
        boxText.text = "";
        gameObject.SetActive(false);
        frameImage.color = Color.white;
    }
    public void UpdateBox(string value)
    {
        frameImage = boxFrame.GetComponent<Image>();
        boxText.text = value;
        ChangeFrameColor(value);
        gameObject.SetActive(true);
    }
    public void MoveEffect(Vector3 target, float duration)
    {
        transform.DOMove(target, duration)
             .OnComplete(() =>
             {
                 gameObject.SetActive(false);
                 frameImage.color = Color.white;
                 rectTransform.anchoredPosition = initAnchoredPosition;

             });
    }
    public void ChangeFrameColor(string value)
    {
        Color color1;
        Color color2;


        float lerpFactor = 0;

        switch (value)
        {
            case "2": color1 = Color.white; color2 = Color.yellow; lerpFactor = 0.05f; break;  
            case "4": color1 = Color.white; color2 = Color.yellow; lerpFactor = 0.2f; break;
            case "8": color1 = Color.white; color2 = Color.yellow; lerpFactor = 0.35f; break;
            case "16": color1 = Color.white; color2 = Color.yellow; lerpFactor = 0.5f; break;
            case "32": color1 = Color.white; color2 = Color.yellow; lerpFactor = 0.65f; break;
            case "64": color1 = Color.white; color2 = Color.yellow; lerpFactor = 0.80f; break;
            case "128": color1 = Color.yellow; color2 = Color.red; lerpFactor = 0.05f; break;
            case "256": color1 = Color.yellow; color2 = Color.red; lerpFactor = 0.2f; break;
            case "512": color1 = Color.yellow; color2 = Color.red; lerpFactor = 0.35f; break;
            case "1024": color1 = Color.yellow; color2 = Color.red; lerpFactor = 0.5f; break; 
            case "2048": color1 = Color.yellow; color2 = Color.red; lerpFactor = 0.65f; break; 
            case "4096": color1 = Color.yellow; color2 = Color.red; lerpFactor = 0.80f; break; 
            default: color1 = Color.yellow; color2 = Color.red; lerpFactor = 1f; break;
        }

        frameImage.color = Color.Lerp(color1, color2, lerpFactor);
    }
}