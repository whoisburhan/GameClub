using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.Fluffy_UnderWater
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public bool IsPlay { get; set; }
        public bool IsGameOver { get; set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            IsPlay = true;
            IsGameOver = false;
        }
    }
}