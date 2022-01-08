using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.Beauty;

namespace GS.FreeSweeper
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static Action<int> OnScoreUpdate;

        [HideInInspector] public bool IsPlay;

        [Header("Player")]
        [SerializeField] private Transform ball;
        [SerializeField] private Transform gem;
        public GameObject GemParticle;
        [SerializeField] private GameObject gameOverEffect;


        [Header("Board Index Transform Positions")]
        [SerializeField] private List<Transform> boardIndexPositions;

        [Header("Gem's Color")]
        [SerializeField] private Color[] gemsColor; // 3 Types of color for 3 differnt points

        [Header("Score Particles")]
        [SerializeField] private GameObject[] scoreParticles;

        private int ballPosIndex;

        private int gemsValue = 1;

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
                OnScoreUpdate?.Invoke(Score);
            }
        }

        int score;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Play();

            if (AdmobAds.instance != null)
                AdmobAds.instance.reqBannerAd(GoogleMobileAds.Api.AdPosition.Bottom);
        }

        private void Play()
        {
            score = 0;
            ballPosIndex = UnityEngine.Random.Range(0,9);
            ball.position = boardIndexPositions[ballPosIndex].position;
            IsPlay = true;
            SpawnGem();
        }

        public void ChangeBallPosition(Vector2 dir)
        {
            if(dir == Vector2.up && ballPosIndex != 0 && ballPosIndex != 1 && ballPosIndex != 2)
            {
                ballPosIndex -= 3;
                ball.DOMove(boardIndexPositions[ballPosIndex].position, 0.5f);
            }
            else if(dir == Vector2.down && ballPosIndex != 6 && ballPosIndex != 7 && ballPosIndex != 8)
            {
                ballPosIndex += 3;
                ball.DOMove(boardIndexPositions[ballPosIndex].position, 0.5f);
            }
            else if (dir == Vector2.right && ballPosIndex != 2 && ballPosIndex != 5 && ballPosIndex != 8)
            {
                ballPosIndex += 1;
                ball.DOMove(boardIndexPositions[ballPosIndex].position, 0.5f);
            }
            else if (dir == Vector2.left && ballPosIndex != 0 && ballPosIndex != 3 && ballPosIndex != 6)
            {
                ballPosIndex -= 1;
                ball.DOMove(boardIndexPositions[ballPosIndex].position, 0.5f);
            }
            else
            {
                Debug.Log("[GS] Invalid Input! Check This function");
            }
        }

        private void SpawnGem()
        {
            int _gemSpawnIndex = GetIndexForSpawnGem();
            gem.position = boardIndexPositions[_gemSpawnIndex].position;

            gemsValue = UnityEngine.Random.Range(0, 3);

            gem.gameObject.GetComponent<SpriteRenderer>().color = gemsColor[gemsValue];
            gem.gameObject.SetActive(true);

            UI_Manager.Instance.SetScoreTextColor(gemsColor[gemsValue]);
        }

        public void ScorePoint()
        {
            Score += gemsValue + 1;
            Debug.Log($"Gems Value:{gemsValue}");
            GameObject _go = Instantiate(GameManager.Instance.GemParticle, ball.position, Quaternion.identity);
            _go.GetComponent<ParticlesColorSet>().SetColor(gemsColor[gemsValue]);
            _go.GetComponent<ParticleSystem>().Play();
            Destroy(_go, 1f);
            Destroy(Instantiate(scoreParticles[gemsValue], ball.position, Quaternion.identity), 3f);
            SpawnGem();
        }

        private int GetIndexForSpawnGem()
        {
            int _index = UnityEngine.Random.Range(0, 9);
            return _index != ballPosIndex ? _index : GetIndexForSpawnGem();
        }

        public void GameOverEffects()
        {
            gameOverEffect.transform.position = ball.transform.position;
            gameOverEffect.SetActive(true);
        }
    }
}