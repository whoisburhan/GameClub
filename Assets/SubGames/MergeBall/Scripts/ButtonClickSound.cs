using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();

        if(button != null)
        {
            button.onClick.AddListener(() => 
            {
                if(GS.GameKit.AudioManager.Instance != null)
                    GS.GameKit.AudioManager.Instance.AudioChangeFunc(GS.GameKit.SoundName.CLICK_4,0);
            });
        }
    }
}
