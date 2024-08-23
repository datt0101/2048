using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;

    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    void Update()
    {
        Swipe();
    }

    void Swipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                currentTouchPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                currentTouchPosition = touch.position;
                Vector2 distance = currentTouchPosition - startTouchPosition;

                if (!stopTouch)
                {
                    if (distance.x < -swipeRange)
                    {
                        stopTouch = true;
                        Debug.Log("Left Swipe");
                        Table.instance.LeftDrag();
                        HandleLogicAfterDrag();
                    }
                    else if (distance.x > swipeRange)
                    {
                        stopTouch = true;
                        Debug.Log("Right Swipe");
                        Table.instance.RightDrag();
                        HandleLogicAfterDrag();
                    }
                    else if (distance.y > swipeRange)
                    {
                        stopTouch = true;
                        Debug.Log("Up Swipe");
                        Table.instance.UpDrag();
                        HandleLogicAfterDrag();
                    }
                    else if (distance.y < -swipeRange)
                    {
                        stopTouch = true;
                        Debug.Log("Down Swipe");
                        Table.instance.DownDrag();
                        HandleLogicAfterDrag();
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                stopTouch = false;
                endTouchPosition = touch.position;
                Vector2 distance = endTouchPosition - startTouchPosition;

                if (Mathf.Abs(distance.x) < tapRange && Mathf.Abs(distance.y) < tapRange)
                {
                    Debug.Log("Tap");
                }
            }
        }
    }
    
    public void HandleLogicAfterDrag()
    {
        AudioManager.instance.PlayDragSound();
        Table.instance.CheckBestScore();
        StartCoroutine(HandleUI());
    }
    public IEnumerator HandleUI()
    {

        yield return new WaitForSeconds(0.5f);
        UIManager.instance.UpdateTableUI();
        Table.instance.AppearNumber();
    }
}
