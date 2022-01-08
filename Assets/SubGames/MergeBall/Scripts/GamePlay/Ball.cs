using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.MergerColorSortBall
{
    public class Ball : MonoBehaviour
    {
        public int BallOrder;
        public Color BallParticleColor;

        [HideInInspector] public int BallNo;
        [HideInInspector] public bool IsDetectable;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag != "Barrier")
            {
                IsDetectable = true;
            }

            if (collision.collider.tag == gameObject.tag)
            {
                if (BallNo > collision.gameObject.GetComponent<Ball>().BallNo)
                {
                    if(GS.GameKit.AudioManager.Instance != null)
                    {
                        GS.GameKit.AudioManager.Instance.AudioChangeFunc(GameKit.SoundName.CLICK_2,0);
                    }

                    Destroyer.Instance.score += (BallOrder + 1) * 5;
                    Spawner.Instance.QuickSpawn(BallOrder + 1, collision.transform.position);
                    Spawner.Instance.Balls[BallOrder].Enqueue(collision.gameObject);
                    collision.gameObject.SetActive(false);
                    Spawner.Instance.Balls[BallOrder].Enqueue(gameObject);
                    gameObject.SetActive(false);
                }
                
            }
        }
    }
}