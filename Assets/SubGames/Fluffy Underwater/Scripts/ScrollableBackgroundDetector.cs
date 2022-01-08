using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.Fluffy_UnderWater
{
    public class ScrollableBackgroundDetector : MonoBehaviour
    {
        [SerializeField] private ScrollingBackgroundScript scrollingBackgroundScript;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Respawn"))
            {
                scrollingBackgroundScript.RePositionChild();
            }
        }
    }
}