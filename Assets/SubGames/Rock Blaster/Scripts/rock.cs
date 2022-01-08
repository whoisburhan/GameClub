using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.RockBlaster
{
    public class rock : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private SpriteRenderer sr;

        private void OnEnable()
        {
            transform.rotation *= Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f)));
            StartCoroutine(WaitBeforeDeactivate());
        }

        private void OnDisable()
        {
            StopCoroutine(WaitBeforeDeactivate());
        }

        public void SetGravityScale(float gravityScale)
        {
            rb2d.gravityScale = gravityScale;
        }

        public void SetRockColor(Color rockColor)
        {
            sr.color = rockColor;
        }

        public Color GetRockColor()
        {
            return sr.color;
        }

        private IEnumerator WaitBeforeDeactivate(float activateTime = 5f)
        {
            yield return new WaitForSeconds(activateTime);
            this.gameObject.SetActive(false);
        }
    }
}