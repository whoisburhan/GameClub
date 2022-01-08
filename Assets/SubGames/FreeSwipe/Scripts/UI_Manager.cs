using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace GS.FreeSweeper
{
    public class UI_Manager : MonoBehaviour
    {
        public static UI_Manager Instance { get; private set; }

        [SerializeField] private Button backToMainMenuButton;
        [SerializeField] private Text scoreText;
        [SerializeField] private Outline scoreTextOutline;
        [Header("GameOver Canvas")]
        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] private Text gameOverCanvasScoreText;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button backToMainMenuFromGameOverCanvasButton;

        private void OnEnable()
        {
            GameManager.OnScoreUpdate += UpdateScore;
        }

        private void OnDisable()
        {
            GameManager.OnScoreUpdate -= UpdateScore;
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            backToMainMenuButton.onClick.AddListener(() => 
            {
                try
                {
                    AdmobAds.instance.hideBanner();
                }
                catch (Exception e) { }
                finally
                {
                    SceneManager.LoadScene("Menu");
                }
            });
            backToMainMenuFromGameOverCanvasButton.onClick.AddListener(() => 
            {
                try
                {
                    AdmobAds.instance.hideBanner();
                }
                catch (Exception e) { }
                finally
                {
                    SceneManager.LoadScene("Menu");
                }
            });
            retryButton.onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });

            gameOverCanvas.SetActive(false);
        }

        public void ActivateGameOverCanvas()
        {
            gameOverCanvasScoreText.text = scoreText.text;
            scoreText.gameObject.SetActive(false);
            backToMainMenuButton.gameObject.SetActive(false);
            gameOverCanvas.SetActive(true);
        }

        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
            Debug.Log("SCORE IS : " + score);
        }

        public void SetScoreTextColor(Color _color)
        {
            scoreText.color = _color;
            scoreTextOutline.effectColor = _color;
        }
    }
}