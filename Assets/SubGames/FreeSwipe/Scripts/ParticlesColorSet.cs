using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.Beauty
{
    public class ParticlesColorSet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem myParticleSystem;

        private void Start()
        {
            myParticleSystem = GetComponent<ParticleSystem>();
        }

        public void SetColor(Color _color)
        {
            var main = myParticleSystem.main;
            main.startColor = _color;
        }
    }
}