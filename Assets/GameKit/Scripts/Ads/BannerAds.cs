//using UnityEngine;
//using UnityEngine.Advertisements;
//using System.Collections;

//public class BannerAds : MonoBehaviour
//{
//#if UNITY_ANDROID
//    const string gameID = "4147765";
//#endif
//#if UNITY_IOS
//    const string gameID = "4147764";
//#endif

//    string placement = "Banner";

//    bool testMode = false;

//    void Start()
//    {
//#if UNITY_ANDROID || UNITY_IOS
//        if(!Advertisement.isInitialized)
//            Advertisement.Initialize(gameID, testMode);

//        StartCoroutine(ShowBannerWhenInitialized());
//#endif
//    }

//    IEnumerator ShowBannerWhenInitialized()
//    {
//        while (!Advertisement.isInitialized)
//        {
//            yield return new WaitForSeconds(0.5f);
//        }
//        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
//        Advertisement.Banner.Show(placement);
//        /*if (Advertisement.Banner.isLoaded)
//        {
//            Debug.Log("YES ITS LOADED");
//            Advertisement.Banner.Show(placement);
//        }*/
//    }
//}
