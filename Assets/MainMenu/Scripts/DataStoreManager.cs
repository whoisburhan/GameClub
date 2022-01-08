using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStoreManager : MonoBehaviour
{
    public static DataStoreManager Instance { get; private set; }

    [Header("Avatar List")]
    [SerializeField] private List<Sprite> avatarList; 

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
          //  DontDestroyOnLoad(this.gameObject);
        }
    }

    public Sprite GetAvatar(int index)
    {
        if (index < 0 || index > avatarList.Count)
        {
            Debug.LogError("Avatar Index Out Of Range!!");
            return null;
        }
        else
        {
            return avatarList[index];
        }
    }
}
