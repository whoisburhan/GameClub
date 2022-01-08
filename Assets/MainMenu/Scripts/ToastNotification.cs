using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastNotification : MonoBehaviour
{
    public static ToastNotification Instance { get; private set; }

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Text toastNotificationText;
    [SerializeField] private GameObject toastObject;
    [SerializeField] private Transform[] toastObjectPostions;  // 0- start , 1 - Target Destination

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Show("Coming Soon..");
        }
    }

    public void Show(string text)
    {
        toastNotificationText.text = text;
        canvasGroup.DOFade(1f, 0.5f);
        canvasGroup.blocksRaycasts = true;

        Sequence _sequence = DOTween.Sequence();
        _sequence.Append(toastObject.transform.DOMove(toastObjectPostions[1].position, 0.5f));
        _sequence.AppendInterval(1f);
        _sequence.Append(toastObject.transform.DOMove(toastObjectPostions[0].position, 0.5f));
        _sequence.Append(canvasGroup.DOFade(0f, 0.5f)).OnComplete(()=> 
        {
            canvasGroup.blocksRaycasts = false;
        });
        
    }
}
