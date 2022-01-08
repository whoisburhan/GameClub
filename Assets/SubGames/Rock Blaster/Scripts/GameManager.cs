using GS.Beauty;
using GS.GameKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.RockBlaster
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject rockEffect;


        [HideInInspector] public bool IsPlay { get; set; }
        [HideInInspector] public bool IsGameOver { get; set; }
        [HideInInspector] public int Score { get; set; }

        private float waitBeforeNextCameraShakeMinimumTime = 8f;
        private float waitBeforeNextCameraShakeMaximumTime = 10f;
        private float camerShakeTimer;
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            Score = 0;
            IsPlay = true;
            IsGameOver = false;
            camerShakeTimer = 1f;
        }


        private void Update()
        {
            if (IsPlay)
            {
                camerShakeTimer -= Time.deltaTime;

                if(camerShakeTimer <= 0)
                {
                    CinemachineShake.Instance.ShakeCamera(1f, 1.5f);
                    GS.GameKit.AudioManager.Instance.AudioChangeFunc(SoundName.SHAKE, 1);
                    camerShakeTimer = Random.Range(waitBeforeNextCameraShakeMinimumTime, waitBeforeNextCameraShakeMaximumTime);
                }
            }
        }

        public void HITPOINT(Vector2 worldPoint)
        {

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)      //for avoiding uncolider object touching error
            {

                if (hit.collider.transform.tag == "Tiles")
                {
                    if (GS.GameKit.AudioManager.Instance != null)
                        GS.GameKit.AudioManager.Instance.AudioChangeFunc(GameKit.SoundName.CLICK_3);

                    GameManager.Instance.Score++;

                    //Add Particle Effects;
                    hit.collider.gameObject.SetActive(false);
                    GameObject _go = Instantiate(GameManager.Instance.rockEffect, hit.collider.transform.position, Quaternion.identity);
                    _go.GetComponent<ParticlesColorSet>().SetColor(hit.collider.GetComponent<SpriteRenderer>().color);
                    _go.GetComponent<ParticleSystem>().Play();
                    Destroy(_go, 1f);

                }
                else if (hit.collider.transform.tag == "Bomb")
                {
                    GS.GameKit.AudioManager.Instance.AudioChangeFunc(GameKit.SoundName.GAME_OVER);

                    GameManager.Instance.IsGameOver = true;
                    GameManager.Instance.IsPlay = false;
                }
            }

        }

    }
}