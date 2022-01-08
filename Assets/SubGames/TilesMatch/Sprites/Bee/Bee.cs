using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.Beauty
{
    public class Bee : MonoBehaviour
    {
        [SerializeField] private Transform[] beeStopPositions;
        [SerializeField] private float minDelay = 1f, maxDelay = 2f, minFriction = 8f, maxFriction = 12f;

        int _index = 0;

        private void Start()
        {
            if(beeStopPositions.Length > 0)
            {
                BeeMove();
            }
        }

        private void Update()
        {
            Vector3 vectorToTarget = beeStopPositions[_index].position - transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);

        }

        private void BeeMove()
        {
             _index = Random.Range(0, beeStopPositions.Length);
            float _delay = Random.Range(minDelay, maxDelay);
            float _friction = Random.Range(minFriction, maxFriction);

            transform.DOMove(beeStopPositions[_index].position, _friction).SetDelay(_delay).OnComplete(()=> 
            {
                BeeMove();
            });
        }
    }
}