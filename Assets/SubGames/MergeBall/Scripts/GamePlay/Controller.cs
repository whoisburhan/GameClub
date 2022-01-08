using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.MergerColorSortBall
{
    public class Controller : MonoBehaviour
    {
        private Touch touch;

        private void Update()
        {
            if (!Spawner.Instance.IsGameOver)
            {
                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);

                    if (Spawner.Instance.CurrentBall != null)
                    {
                        var _pos = Camera.main.ScreenToWorldPoint(touch.position);
                        Spawner.Instance.CurrentBall.transform.position = new Vector3(_pos.x, Spawner.Instance.CurrentBall.transform.position.y);

                        if (touch.phase == TouchPhase.Ended)
                        {
                            Spawner.Instance.CurrentBall.GetComponent<Rigidbody2D>().isKinematic = false;
                            Spawner.Instance.CurrentBall = null;
                            Spawner.Instance.Spawn();

                           // if (AudioManager.Instance != null)
                           //     AudioManager.Instance.AudioChangeFunc(0, 1);
                        }
                    }
                }

            }
        }
    }
}