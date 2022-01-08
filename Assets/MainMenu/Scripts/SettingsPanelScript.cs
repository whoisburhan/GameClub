using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GS.GameKit;

namespace GS.CasualGameClub.MainMenu
{
    public class SettingsPanelScript : MonoBehaviour
    {
        public static SettingsPanelScript Instance { get;private set; }

        [SerializeField] private Button settingsButton;
        [SerializeField] private CanvasGroup settingPanelCanvasGroup;
        [SerializeField] private Transform settingsPanelTransform;
        [SerializeField] private Transform[] settingsObjectPostions;  // 0- start , 1 - Target Destination

        [Header("Souund/Music Button Color")]
        [SerializeField] private Color muteCOlor;
        [SerializeField] private Color unMuteColor;

        [Header("RectTransform")]
        [SerializeField] private RectTransform soundCircle;
        [SerializeField] private RectTransform musicCircle;

        [Header("Button")]
        [SerializeField] private Button soundButton;
        [SerializeField] private Button musicButton;

        [Header("Image")]
        [SerializeField] private Image soundButtonUIImg;
        [SerializeField] private Image musicButtonUIImg;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            settingsButton.onClick.AddListener(() => 
            {
                settingPanelCanvasGroup.DOFade(1f, 0.5f).OnComplete(() =>
                {
                    settingPanelCanvasGroup.blocksRaycasts = true;
                    settingsPanelTransform.DOMove(settingsObjectPostions[1].position, 0.5f);
                });
            });

            SoundButtonAnimate(GS.GameKit.AudioManager.Instance.audioSource[0].mute ? muteCOlor : unMuteColor, GS.GameKit.AudioManager.Instance.audioSource[0].mute);
            MusicButtonAnimate(GS.GameKit.AudioManager.Instance.backgroundAudio.mute ? muteCOlor : unMuteColor, GS.GameKit.AudioManager.Instance.backgroundAudio.mute);

            // Sound & Music Buttons
            soundButton.onClick.AddListener(() =>
            {
                SoundButtonAnimate(GS.GameKit.AudioManager.Instance.audioSource[0].mute ? unMuteColor : muteCOlor, !GS.GameKit.AudioManager.Instance.audioSource[0].mute);
            });

            musicButton.onClick.AddListener(() =>
            {
                MusicButtonAnimate(GS.GameKit.AudioManager.Instance.backgroundAudio.mute ? unMuteColor : muteCOlor, !GS.GameKit.AudioManager.Instance.backgroundAudio.mute);
            });
        }

        public void SettingPanelExit()
        {
            settingPanelCanvasGroup.DOFade(0f, 0.5f).OnComplete(() =>
            {
                settingPanelCanvasGroup.blocksRaycasts = false;
            });

            settingsPanelTransform.DOMove(settingsObjectPostions[0].position, 0.5f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_color"></param>
        /// <param name="mute"></param>
        private void SoundButtonAnimate(Color _color, bool mute)
        {
            soundButton.interactable = false;
            soundCircle.DOAnchorPosX(soundCircle.anchoredPosition.x * -1f, 0.2f).OnComplete(() =>
            {
                foreach (var _audioSource in GS.GameKit.AudioManager.Instance.audioSource)
                    _audioSource.mute = mute;
                soundButton.interactable = true;
            });

            soundButtonUIImg.DOColor(_color, 0.2f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_color"></param>
        /// <param name="mute"></param>
        private void MusicButtonAnimate(Color _color, bool mute)
        {
            musicButton.interactable = false;
            musicCircle.DOAnchorPosX(musicCircle.anchoredPosition.x * -1f, 0.2f).OnComplete(() =>
            {
                GS.GameKit.AudioManager.Instance.backgroundAudio.mute = mute;
                musicButton.interactable = true;
            });

            musicButtonUIImg.DOColor(_color, 0.2f);
        }
    }
}