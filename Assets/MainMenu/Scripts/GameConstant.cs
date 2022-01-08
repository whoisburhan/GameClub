using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstant : MonoBehaviour
{
    public static GameConstant Instance { get; private set; }

    private const string PlayerNameKey = "PLAYER_NAME";
    private const string playerAvatarIndexKey = "PLAYER_AVATAR_INDEX";

    private string playerName;

    public string PlayerName
    {
        get { return playerName; }

        set 
        {
            playerName = value;
            PlayerPrefs.SetString(PlayerNameKey,value); 
        }
    }

    public int PlayerAvatarIndex
    {
        get { return PlayerPrefs.GetInt(playerAvatarIndexKey, 0); }

        set { PlayerPrefs.SetInt(playerAvatarIndexKey, value); }
    }

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

    private void Start()
    {
        playerName = PlayerPrefs.GetString(PlayerNameKey, "Guest" + UnityEngine.Random.Range(1000, 9999).ToString());
        PlayerName = playerName;
    }
}
