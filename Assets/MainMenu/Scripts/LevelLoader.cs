using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelLoader : MonoBehaviour
{
    [Header("GAME BUTTON")]
    [SerializeField] private Button m_AA;
    [SerializeField] private Button m_TilesMatch;
    [SerializeField] private Button m_MergeBall;
    [SerializeField] private Button m_TilesBreak;
    [SerializeField] private Button m_FreeSwipe;
    [SerializeField] private Button m_FluffyUnderWater;

    private void OnEnable()
    {
        if (GS.GameKit.AudioManager.Instance != null)
            GS.GameKit.AudioManager.Instance.BackgroundAudioFunc(GS.GameKit.MusicName.BEN_SOUND_BUDDY);
    }

    private void OnDisable()
    {
        if (GS.GameKit.AudioManager.Instance != null)
            GS.GameKit.AudioManager.Instance.StopBackgroundMusic();
    }

    private void Start()
    {
        

        m_AA.onClick.AddListener(() => { SceneManager.LoadScene("AA"); });
        m_TilesMatch.onClick.AddListener(() => { SceneManager.LoadScene("TilesMatch"); });
        m_MergeBall.onClick.AddListener(() => { SceneManager.LoadScene("MergeBall"); });
        m_TilesBreak.onClick.AddListener(() => { SceneManager.LoadScene("RockBlaster"); });
        m_FreeSwipe.onClick.AddListener(() => { SceneManager.LoadScene("FreeSwipe"); });
        m_FluffyUnderWater.onClick.AddListener(() => { SceneManager.LoadScene("Fluffy_UnderWater"); });

        try
        {
            AdmobAds.instance.hideBanner();
        }
        catch(Exception e)
        {

        }
    }
}
