using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GS.GameKit
{
    public class CinemachineShake : MonoBehaviour
    {
        public static CinemachineShake Instance { get; private set; }

        private CinemachineVirtualCamera cinemachineVirtualCamera;
        private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

        private float shakeTimer;

        private void Awake()
        {
            Instance = this;
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.
                GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void ShakeCamera(float _intensity, float _time, float _frequency = 1.5f)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _intensity;
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = _frequency;
            shakeTimer = _time;
        }

        private void Update()
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0)
                {
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
                }
            }
        }
    }
}