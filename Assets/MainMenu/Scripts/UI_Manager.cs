using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GS.CasualGameClub.MainMenu
{
    public class UI_Manager : MonoBehaviour
    {
        public static UI_Manager Instance { get; private set; }

        [Header("Player Profile")]
        [SerializeField] private Button playerProfileEditorButton;
        [SerializeField] private CanvasGroup playerProfileCanvasGroup;
        [SerializeField] private Button playerProfileCanvasExitButton;
        [SerializeField] private Button playerProfileCanvasExitPanelButton;
        [Space]
        [Space]
        [SerializeField] private Image playerProfilePicImg;
        [SerializeField] private TextMeshProUGUI playerNameText;
        [Space]
        [Space]
        [SerializeField] private Image playerProfileEditorCanvasProfilePic;
        [Space]
        [SerializeField] private TMP_InputField playerNameEditorInputField;
        [SerializeField] private TextMeshProUGUI playerNameHolderText;
        [Space]
        [SerializeField] private Button playerNameEditButton;
        [SerializeField] private Text playerNameEditButtonText;
        [Header("Bottom Panel Buttons UI")]
        [SerializeField] private Button settingsButton;
        [Space]
        [SerializeField] private Button leaderboardButton;
        //[Space]
        //[SerializeField] private Button reviewButton;
        [Space]
        [SerializeField] private Button shopButton;
        [Space]
        [SerializeField] private Button dailySpinButton;


        private bool isSavePlayerNameFromInputField = false;

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
                Instance = this;
        }

        private void Start()
        {
            SetPlayerName();
            SetPlayerAvatar();

            playerNameEditorInputField.enabled = false;

            #region Button Config
            playerProfileEditorButton.onClick.AddListener(() => 
            {
                playerNameEditorInputField.text = playerNameHolderText.text;

                playerProfileCanvasGroup.DOFade(1f, 0.5f).OnComplete(() => 
                {
                    playerProfileCanvasGroup.blocksRaycasts = true;

                });
            });

            playerProfileCanvasExitButton.onClick.AddListener(()=> 
            {
                PlayerProfileCanvasExit();
            });

            playerProfileCanvasExitPanelButton.onClick.AddListener(() =>
            {
                PlayerProfileCanvasExit();
            });

            playerNameEditButton.onClick.AddListener(() => 
            {
                SetPlayerNameButtonFunc();
            });
            #endregion

            BottomPanelButtonConfig();

        }

        private void PlayerProfileCanvasExit()
        {
            playerNameEditButtonText.text = "Edit";
            playerNameEditorInputField.enabled = false;
            isSavePlayerNameFromInputField = false;

            playerProfileCanvasGroup.DOFade(0f, 0.5f).OnComplete(() => 
            {
                playerProfileCanvasGroup.blocksRaycasts = false;
            });
        }

        private void SetPlayerNameButtonFunc()
        {
            if (!isSavePlayerNameFromInputField)
            {
                playerNameEditButtonText.text = "Save";
                playerNameEditorInputField.enabled = true;
                isSavePlayerNameFromInputField = true;
            }
            else
            {
                // Save And Show PlayerName

                GameConstant.Instance.PlayerName = playerNameEditorInputField.text;
                SetPlayerName();

                //---------------------------
                playerNameEditButtonText.text = "Edit";
                playerNameEditorInputField.enabled = false;
                isSavePlayerNameFromInputField = false;


                ToastNotification.Instance.Show("Name Updated!");
            }
        }

        public void SetPlayerName()
        {
            string _playerName = GameConstant.Instance.PlayerName;
            
            playerNameText.text = _playerName;
            playerNameHolderText.text = _playerName;
        }

        public void SetPlayerAvatarIndex(int index)
        {
            GameConstant.Instance.PlayerAvatarIndex = index;
            SetPlayerAvatar();
        }

        public void SetPlayerAvatar()
        {
            int _avatarIndex = GameConstant.Instance.PlayerAvatarIndex;
            Sprite _avatar = DataStoreManager.Instance.GetAvatar(_avatarIndex);

            playerProfilePicImg.sprite = _avatar;
            playerProfileEditorCanvasProfilePic.sprite = _avatar;
        }

        /// <summary>
        /// 
        /// </summary>
        public void BottomPanelButtonConfig()
        {
            settingsButton.onClick.AddListener(() => { });

            leaderboardButton.onClick.AddListener(() => { ToastNotification.Instance.Show("Coming Soon!"); });

            //reviewButton.onClick.AddListener(() => { ToastNotification.Instance.Show("Coming Soon!"); });

            shopButton.onClick.AddListener(() => { ToastNotification.Instance.Show("Coming Soon!"); });

            dailySpinButton.onClick.AddListener(() => { ToastNotification.Instance.Show("Coming Soon!"); });
        }
    }
}