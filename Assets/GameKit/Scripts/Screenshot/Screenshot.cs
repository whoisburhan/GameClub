using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.GameKit
{
    public class Screenshot : MonoBehaviour
    {
        public static Screenshot Instance { get; private set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Home))
            {
                ScreenCapture.CaptureScreenshot(System.DateTime.Now.ToString("YYYY_MM_DD_hh_mm_ss") + ".png");
                //Debug.Log(Application.persistentDataPath);
            }
        }
    }
}