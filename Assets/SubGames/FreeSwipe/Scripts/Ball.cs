using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FreeSweeper
{
    public class Ball : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Tiles"))
            {
                GameManager.Instance.ScorePoint();
                GS.GameKit.AudioManager.Instance.Play(GameKit.SoundName.CLICK_2);
            }

            if (col.CompareTag("Bomb"))
            {
                Debug.Log("GAME OVER");
                
                UI_Manager.Instance.ActivateGameOverCanvas();
                GameManager.Instance.GameOverEffects();
                GS.GameKit.AudioManager.Instance.Play(GameKit.SoundName.GAME_OVER_2);
                this.gameObject.SetActive(false);
            }
        }
    }
}