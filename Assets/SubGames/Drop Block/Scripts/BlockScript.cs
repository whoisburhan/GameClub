using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.DropBlock
{
    public class BlockScript : MonoBehaviour
    {
        [HideInInspector] public bool isMovementDirectionRight = true;
        [HideInInspector] public int ColorIndex = 0;

        private float offset = 1f;
        private float speed = 1.5f;

        private void Update()
        {

            if (isMovementDirectionRight)
                transform.Translate(transform.right * speed * Time.deltaTime);
            else
                transform.Translate(-transform.right * speed * Time.deltaTime);

            if (transform.position.x >= offset)
                isMovementDirectionRight = false;

            if (transform.position.x <= -offset)
                isMovementDirectionRight = true;

        }
    }
}