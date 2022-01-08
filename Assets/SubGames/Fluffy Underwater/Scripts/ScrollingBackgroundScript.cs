using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.Fluffy_UnderWater
{
    public class ScrollingBackgroundScript : MonoBehaviour
    {
        [SerializeField] private Transform[] partsTransform;
        [SerializeField] private float scrollSpeed = 2f;
        [SerializeField] private float rePositionOffsetX = 22.3f;

        private bool IsPartOneToSwitch = true;

        private void Update()
        {
            transform.Translate(-transform.right * Time.deltaTime * scrollSpeed);
        }

        public void RePositionChild()
        {
            if (IsPartOneToSwitch)
            {
                partsTransform[0].position = new Vector3(partsTransform[1].position.x + rePositionOffsetX, partsTransform[1].position.y);
                IsPartOneToSwitch = false;
            }
            else
            {
                partsTransform[1].position = new Vector3(partsTransform[0].position.x + rePositionOffsetX, partsTransform[0].position.y);
                IsPartOneToSwitch = true;
            }
        }
    }
}