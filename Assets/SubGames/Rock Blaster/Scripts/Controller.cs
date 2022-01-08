using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.RockBlaster
{
    public class Controller : MonoBehaviour
    {

        private void Update()
        {
            if (GameManager.Instance.IsPlay)
            {
                if (Input.touchCount > 0)
                {
                    Touch[] touches = Input.touches;
                    foreach (Touch touch in touches)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
                            GameManager.Instance.HITPOINT(worldPoint);
                            // Debug.Log(Input.touchCount);
                        }
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    GameManager.Instance.HITPOINT(worldPoint);
                }
            }
        }

        
    }
}