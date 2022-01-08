using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FreeSweeper
{
    public class Controller : MonoBehaviour
    {
        private Vector2 fingerUpPosition, fingerDownPosition;

        private float minimumDistanceForSwipe = 2f;

        private void Update()
        {
            if (GameManager.Instance.IsPlay) 
            {
                if(Input.touchCount > 0)
                {
                    Touch[] touches = Input.touches;
                    foreach(Touch touch in touches)
                    {
                        if(touch.phase == TouchPhase.Began)
                        {
                            fingerUpPosition = touch.position;
                            fingerDownPosition = touch.position;
                        }

                        if(touch.phase == TouchPhase.Ended)
                        {
                            fingerDownPosition = touch.position;
                            DetectSwipe();
                        }
                    }
                }
            }

#if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.LeftArrow)) GameManager.Instance.ChangeBallPosition(Vector2.left);
            else if (Input.GetKeyDown(KeyCode.RightArrow)) GameManager.Instance.ChangeBallPosition(Vector2.right);
            else if (Input.GetKeyDown(KeyCode.UpArrow)) GameManager.Instance.ChangeBallPosition(Vector2.up);
            else if (Input.GetKeyDown(KeyCode.DownArrow)) GameManager.Instance.ChangeBallPosition(Vector2.down);
#endif
        }

        private void DetectSwipe()
        {
            if (SwipeDistanceCheckMet())
            {
                Vector2 _direction = Vector2.zero;

                if (IsVerticalSwipe())
                {
                    _direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? Vector2.up : Vector2.down;
                    
                }
                else
                {
                    _direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? Vector2.right : Vector2.left;
                }

                GameManager.Instance.ChangeBallPosition(_direction);
            }
        }

        private bool IsVerticalSwipe()
        {
            return VerticalMovementDistance() > HorizontalMovementDistance();
        }

        private bool SwipeDistanceCheckMet()
        {
            return VerticalMovementDistance() > minimumDistanceForSwipe || HorizontalMovementDistance() > minimumDistanceForSwipe;
        }

        private float VerticalMovementDistance()
        {
            return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
        }

        private float HorizontalMovementDistance()
        {
            return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
        }
    }
}