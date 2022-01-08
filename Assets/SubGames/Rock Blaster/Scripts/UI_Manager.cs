using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GS.RockBlaster
{
    public class UI_Manager : MonoBehaviour
    {
        public static UI_Manager Instance { get; private set; }

        [SerializeField] private Button backToMainMenuButton;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            backToMainMenuButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Menu");
            });
        }
    }
}