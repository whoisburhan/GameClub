using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.MergerColorSortBall
{
    public class Spawner : MonoBehaviour
    {
        public static Spawner Instance { get; private set; }

        public bool IsGameOver = false;

        [HideInInspector] public GameObject CurrentBall;

        [SerializeField] private ParticleSystem OnBallDestroyParticle;

        [SerializeField] private GameObject[] ballPrefabs;

        [Header("Initlize Amount")]
        [SerializeField] private List<int> ballAmountList = new List<int>();

        [Header("Maximum Order Of Spawn Object")]
        [SerializeField] private int spawnOrder = 3;

        public int SpawnObjectNo = 0;

        public List<Queue<GameObject>> Balls = new List<Queue<GameObject>>();

        private void Awake()
        {
            Instance = this;

            InitializeSpawn();
            Spawn();
        }

        private void InitializeSpawn()
        {
            for(int i = 0; i < ballAmountList.Count; i++)
            {
                Queue<GameObject> _ballQueue = new Queue<GameObject>();
                for(int j = 0; j<ballAmountList[i]; j++)
                {
                    GameObject _go = Instantiate(ballPrefabs[i], transform.position, Quaternion.identity);
                    _go.SetActive(false);
                    _ballQueue.Enqueue(_go);
                }
                Balls.Add(_ballQueue);
            }
        }

        public void Spawn()
        {
            if (!IsGameOver)
            {
                StartCoroutine(Delay(0.5f));
            }
        }

        private IEnumerator Delay(float delayTime = 0.5f)
        {
            yield return new WaitForSeconds(delayTime);
            SpawnObjectNo++;
            int _index = UnityEngine.Random.Range(0, spawnOrder);
            GameObject _go = Balls[_index].Dequeue();
            _go.SetActive(true);
            _go.transform.position = transform.position;
            _go.transform.rotation = Quaternion.identity;

            _go.GetComponent<Rigidbody2D>().isKinematic = true;

            Ball _ball = _go.GetComponent<Ball>();
            if (_ball != null)
            {
                _ball.BallNo = SpawnObjectNo;
                _ball.IsDetectable = false;
            }

            CurrentBall = _go;
        }

        public void QuickSpawn(int ballOrder, Vector3 pos)
        {
            if (ballOrder <= 6)
            {
                GameObject _go = Balls[ballOrder].Dequeue();
                _go.SetActive(true);
                
                _go.transform.position = pos;
                _go.transform.rotation = Quaternion.identity;

                _go.GetComponent<Rigidbody2D>().isKinematic = false;

                SpawnObjectNo++;
                Ball _ball = _go.GetComponent<Ball>();
                if (_ball != null)
                {
                    _ball.BallNo = SpawnObjectNo;
                    _ball.IsDetectable = true;
                    OnBallDestroyParticle.transform.position = _ball.transform.position;
                    OnBallDestroyParticleSpawn(_ball.BallParticleColor);
                }
            }
            //else
            //{
            //    OnBallDestroyParticleSpawn();
            //}
        }

        private void OnBallDestroyParticleSpawn(Color color)
        {
            if (OnBallDestroyParticle != null)
            {
                var main = OnBallDestroyParticle.main;
                main.startColor = color;

                if (OnBallDestroyParticle.isPlaying)
                    OnBallDestroyParticle.Stop();
                OnBallDestroyParticle.Play();
            }
        }
    }
}