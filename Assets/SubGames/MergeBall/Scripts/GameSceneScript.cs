using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GS.MergerColorSortBall
{
    public class GameSceneScript : MonoBehaviour
    {

        public void RetryButtonFunc()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BackToMainMenu()
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
        }
    }
}