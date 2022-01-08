using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FreeSweeper
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> spawnPositions;
        [SerializeField] private List<GameObject> enemyObjects;

        
        [SerializeField] float spawnInterverMin = 5f;
        [SerializeField] float spawnIntervalMax = 10f;
        float timer;

        int enemyIndex;

        private void Start()
        {
            enemyIndex = 0;
            timer = spawnInterverMin;
        }

        private void Update()
        {
            if (GameManager.Instance.IsPlay)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    int _index = Random.Range(0, spawnPositions.Count);

                    GameObject _go = enemyObjects[enemyIndex];
                    _go.transform.position = spawnPositions[_index].position;
                    _go.transform.rotation = spawnPositions[_index].rotation;
                    _go.SetActive(true);

                    _go.transform.DOMove((_index % 2 == 0 ? spawnPositions[_index + 1].position : spawnPositions[_index - 1].position), 8f).OnComplete(() =>
                    {
                        _go.SetActive(false);
                    });

                    enemyIndex++;

                    if (enemyIndex >= enemyObjects.Count)
                        enemyIndex = 0;

                    timer = Random.Range(spawnInterverMin, spawnIntervalMax);
                }
            }
        }
    }
}