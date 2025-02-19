using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GS.GameKit
{
    [RequireComponent(typeof(Button))]
    public class SoundButtonScript : MonoBehaviour
    {
        [Header("Color")]
        [SerializeField] private Color muteCOlor;
        [SerializeField] private Color unMuteColor;

        [Header("RectTransform")]
        [SerializeField] private RectTransform circle;

        [Header("Image")]
        [SerializeField] private Image buttonUIImg;

        [Header("AudioSource")]
        [SerializeField] private AudioSource[] audioSource;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            Animate(audioSource[0].mute ? muteCOlor : unMuteColor, audioSource[0].mute);

            button.onClick.AddListener(() => 
            {
                Animate(audioSource[0].mute ? unMuteColor : muteCOlor, !audioSource[0].mute);
            });
        }

        private void Animate(Color _color, bool mute)
        {
            //button.interactable = false;
            circle.DOAnchorPosX(circle.anchoredPosition.x * -1f, 0.2f).OnComplete(() => 
            {
                foreach(var _audioSource in audioSource)
                    _audioSource.mute = mute;
              //  button.interactable = true;
            });

            buttonUIImg.DOColor(_color, 0.2f);
        }
    }
}