﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using System;
using GS.CommonStuff;

namespace GS.TilesBreak
{
    public class Controller : MonoBehaviour
    {

        public const string GAME_DATA_STORE_KEY = "HIGHSCORE_TILESBREAK";

        public int LevelNO;

        public static Controller instance;

        [SerializeField] private Image sliderBar;

        [SerializeField] private ColorPlate myColors;

        public GameObject finalScorePanel;
        public GameObject showRewardAds;

        public Text scoreText;
        public Text finalScoreText;
        public Text finalScoreTittleText;

        public float tileSpeed = 40f;
        public float tileSpawnerIntervalTime = 0.5f;
        public int score = 0;
        public int life = 3;

        public bool isPlay = false;
        public bool isGameOver = false;
        private bool flag = true;
        public AudioSource sound;
        public AudioSource backgroundMusic;

        public GameObject[] hitParticles;

        private bool allowRwdAdsShow = true;

        private void OnEnable()
        {
            //AdsManager_Unity.OnRewardGiving += RewardSuccess;
            //AdsManager_Unity.OnRewardCancel += RewardFailed;
            //AdsManager_Unity.OnRewardError += RewardError;

            //AdmobAds.instance.OnReward += RewardSuccess;
            //AdmobAds.instance.OnRewardFailed += RewardFailed;
        }

        private void OnDisable()
        {
            //AdsManager_Unity.OnRewardGiving -= RewardSuccess;
            //AdsManager_Unity.OnRewardCancel -= RewardFailed;
            //AdsManager_Unity.OnRewardError -= RewardError;

            //AdmobAds.instance.OnReward -= RewardSuccess;
            //AdmobAds.instance.OnRewardFailed -= RewardFailed;
        }

        private void Start()
        {
            Time.timeScale = 1f;
            //Advertisement.Initialize(gameID, testMode);

            if (AdmobAds.instance != null)
            {
                AdmobAds.instance.reqBannerAd(GoogleMobileAds.Api.AdPosition.Bottom);
                AdmobAds.instance.requestInterstital();
                AdmobAds.instance.loadRewardVideo();
            }

            isPlay = true;

            if (finalScorePanel.activeSelf)
            {
                finalScorePanel.SetActive(false);
            }
            instance = this;
            isGameOver = false;
            flag = true;

            if (GS.GameKit.AudioManager.Instance != null)
            {
               // GS.GameKit.AudioManager.Instance.ResetAudio();
                //GS.GameKit.AudioManager.Instance.BackgroundAudioFunc(0);
            }
        }

        private void Update()
        {

            if (isGameOver && flag)
            {
                if (AdmobAds.instance!= null &&  AdmobAds.instance.IsRewarededVideoLoaded() && allowRwdAdsShow)
                {
                    if (!showRewardAds.activeSelf)
                    {
                        showRewardAds.SetActive(true);
                      //  Time.timeScale = 0f;
                    }

                    FinalScoreCal();

                    flag = false;
                }
                else
                {

                    if (!finalScorePanel.activeSelf)
                    {
                        finalScorePanel.SetActive(true);

                        if (finalScoreText.gameObject.activeSelf)
                        {
                            int hs = PlayerPrefs.GetInt(GAME_DATA_STORE_KEY, 0);
                            Debug.Log("123  hs:" + hs);
                            Debug.Log("123 score:" + score);
                            if (score > hs)
                            {
                                finalScoreTittleText.text = "HighScore";
                                PlayerPrefs.SetInt(GAME_DATA_STORE_KEY, score);

                            }
                            else
                            {
                                finalScoreTittleText.text = "Score";
                            }
                            finalScoreText.text = score.ToString();
                        }
                    }

                    if (scoreText.gameObject.activeSelf)
                    {
                        scoreText.gameObject.SetActive(false);
                        sliderBar.transform.parent.gameObject.SetActive(false);
                    }

                    flag = false;
                }
            }

            if (scoreText.gameObject.activeSelf)
            {
                scoreText.text = score.ToString();
            }

            if (isPlay)
            {
                if (Input.touchCount > 0)
                {
                    Touch[] touches = Input.touches;
                    foreach (Touch touch in touches)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
                            HITPOINT(worldPoint);
                            // Debug.Log(Input.touchCount);
                        }
                    }
                }

                TileSpeedUpdate();
            }
        }

        private void TileSpeedUpdate()
        {
            #region switch
            /*switch(score)
            {
                case 5:
                    tileSpeed = 6.5f;
                    tileSpawnerIntervalTime = 1.45f;
                    break;
                case 10:
                    tileSpeed = 6.75f;
                    tileSpawnerIntervalTime = 1.4f;
                    break;
                case 15:
                    tileSpeed = 7.0f;
                    tileSpawnerIntervalTime = 1.35f;
                    break;
                case 20:
                    tileSpeed = 7.25f;
                    tileSpawnerIntervalTime = 1.3f;
                    break;
                case 30:
                    tileSpeed = 7.5f;
                    tileSpawnerIntervalTime = 1.25f;
                    break;
                case 40:
                    tileSpeed = 7.75f;
                    tileSpawnerIntervalTime = 1.20f;
                    break;
                case 50:
                    tileSpeed = 8.0f;
                    tileSpawnerIntervalTime = 1.1f;
                    break;
                case 60:
                    tileSpeed = 8.5f;
                    tileSpawnerIntervalTime = 1.0f;
                    break;
                case 70:
                    tileSpeed = 9.0f;
                    tileSpawnerIntervalTime = 0.9f;
                    break;
                case 80:
                    tileSpeed = 9.5f;
                    tileSpawnerIntervalTime = 0.85f;
                    break;
                case 90:
                    tileSpeed = 10.0f;
                    tileSpawnerIntervalTime = 0.8f;
                    break;
                case 100:
                    tileSpeed = 10.5f;
                    tileSpawnerIntervalTime = 0.77f;
                    break;
                case 120:
                    tileSpeed = 11.0f;
                    tileSpawnerIntervalTime = 0.75f;
                    break;
            }*/
            #endregion

            if (score > 560)
            {
                Time.timeScale = 1.7f;
            }
            else if (score > 420)
            {
                Time.timeScale = 1.6f;
            }
            else if (score > 350)
            {
                Time.timeScale = 1.5f;
            }
            else if (score > 280)
            {
                Time.timeScale = 1.4f;
            }
            else if (score > 210)
            {
                Time.timeScale = 1.3f;
            }
            else if (score > 140)
            {
                Time.timeScale = 1.2f;
            }
            else if (score > 70)
            {
                Time.timeScale = 1.1f;
            }
            backgroundMusic.pitch = Time.timeScale;
        }

        private void HITPOINT(Vector2 worldPoint)
        {

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)      //for avoiding uncolider object touching error
            {

                if (hit.collider.transform.tag == "Tiles")
                {
                    GS.GameKit.AudioManager.Instance.AudioChangeFunc(GameKit.SoundName.CLICK_3);

                    score += 1;

                    if (score > 599) scoreText.color = myColors.colors[2];
                    else if (score > 299) scoreText.color = myColors.colors[1];
                    else scoreText.color = myColors.colors[0];

                    Debug.Log(Mathf.Clamp((score / 1000f), 0f, 1f));
                    sliderBar.fillAmount = Mathf.Clamp((score / 1000f), 0f, 1f);

                    /* if(hit.transform.GetComponent<TileHealth>().tileLvl == 1)
                     {
                         Destroy(Instantiate(hitParticles[0],hit.transform.position,Quaternion.identity), 1f);
                     }
                     else if (hit.transform.GetComponent<TileHealth>().tileLvl == 2)
                     {
                         Destroy(Instantiate(hitParticles[1], hit.transform.position, Quaternion.identity), 1f);
                     }
                     else if (hit.transform.GetComponent<TileHealth>().tileLvl == 3)
                     {
                         Destroy(Instantiate(hitParticles[2], hit.transform.position, Quaternion.identity), 1f);
                     }*/

                    //hit.transform.GetComponent<TileHealth>().TileHealthUpdate();
                   // hit.transform.GetComponent<TileHealth>().SetIsDissolving();
                }
                else if (hit.collider.transform.tag == "Bomb")
                {
                    GS.GameKit.AudioManager.Instance.AudioChangeFunc(GameKit.SoundName.GAME_OVER);

                    //score += 1;
                    isGameOver = true;
                    isPlay = false;
                }/*
             else if (hit.collider.transform.tag == "BOMB")
             {

             }*/
            }
            /* else
             {
                 AudioManager.Instance.AudioChangeFunc(0, 1, false, 1.4f);
                 //score += 1;
                 isGameOver = true;
             }*/

        }

        private void FinalScoreCal()
        {
            int hs = PlayerPrefs.GetInt(GAME_DATA_STORE_KEY, 0);

            if (score >= hs)
            {
                finalScoreTittleText.text = "HighScore";
                PlayerPrefs.SetInt(GAME_DATA_STORE_KEY, score);

            }
            else
            {
                finalScoreTittleText.text = "Score";
            }
            finalScoreText.text = score.ToString();
        }

        public void ShowRwdAds()
        {
            // ShowRewardedRegularAd(OnRewardedAdClosed);
            //AdsManager_Unity.Instance.ShowRewardedVideo();
            AdmobAds.instance.ShowRewardedAd(()=> { RewardSuccess(); } ,()=> { RewardFailed(); });
        }

       

        


        public void RewardedAdPanelCrossButton()
        {
            //Time.timeScale = 1f;
        }

        public void Play()
        {
            isPlay = true;
        }

        public void NewGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #region Rewarded Video Functions

        public void RewardSuccess()
        {
            Debug.Log("Ad Finished,reward Player");
            Time.timeScale = 1f;
            isGameOver = false;
            if (!isGameOver)
            {
                flag = true;
            }
            allowRwdAdsShow = false;
            isPlay = true;
        }

        public void RewardFailed()
        {
            Debug.Log("Ad skipped, posa manus :(");
            Time.timeScale = 1f;
            if (!finalScorePanel.activeSelf)
            {
                finalScorePanel.SetActive(true);
            }
        }

        public void RewardError()
        {
            Debug.Log("Yaaa! play oylo nani ad");
            Time.timeScale = 1f;
            if (!finalScorePanel.activeSelf)
            {
                finalScorePanel.SetActive(true);
            }
        }
        #endregion
    }
}