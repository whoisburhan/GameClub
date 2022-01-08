using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.RockBlaster
{
    public class Spawner : MonoBehaviour
    {
        public static Spawner Instance { get; private set; }

        [SerializeField] private float spawnZoneOnXAxis = 2f;

        [SerializeField] private List<rock> rocks;
        [SerializeField] private ColorPlate rockColors;

        private float WaitBeforeNextSpawnTime = 2f;

        private float timer, rockGravityScale;

        private int rockIndex;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            timer = WaitBeforeNextSpawnTime;
            rockIndex = 0;
            rockGravityScale = 0.2f;
        }

        private void Update()
        {
            if (GameManager.Instance.IsPlay)
            {
                timer -= Time.deltaTime;

                if(timer <= 0)
                {
                    Spawn();
                    timer = WaitBeforeNextSpawnTime;
                }
            }
        }

        private void Spawn()
        {
            Vector3 _spawnPosition = new Vector3(Random.Range(-spawnZoneOnXAxis, spawnZoneOnXAxis), transform.position.y);

            rocks[rockIndex].transform.position = _spawnPosition;
            rocks[rockIndex].gameObject.SetActive(false);
            rocks[rockIndex].gameObject.SetActive(true);    // need it
            rocks[rockIndex].SetGravityScale(rockGravityScale);
            rocks[rockIndex].SetRockColor(rockColors.colors[Random.Range(0, rockColors.colors.Count)]);

            int _rockIndex = rockIndex + 1;
            rockIndex = (_rockIndex) % rocks.Count;
        }
    }
}