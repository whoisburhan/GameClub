using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.Fluffy_UnderWater
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private float forceAmount = 2f;

        private bool readyToAddForce = false;

        private void Update()
        {
            if (GameManager.Instance.IsPlay)
            {
                if (Input.touchCount > 0)
                {
                    Touch[] touches = Input.touches;

                    if (touches[0].phase == TouchPhase.Began)
                    {
                        readyToAddForce = true;
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    readyToAddForce = true;
                }
            }
        }

        private void FixedUpdate()
        {
            if (readyToAddForce)
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(transform.up * forceAmount);
                readyToAddForce = false;
            }
        }

    }
}