using UnityEngine;

public class SaveExtension
{
    public static PlayerData player;
    public static GameData game;
    public const string playerDataKey = "PlayerData";

    public static void Save()
    {
        var @string = JsonUtility.ToJson(player);
        PlayerPrefs.SetString(playerDataKey, @string);
    }

    public static void Override()
    {
        player = new PlayerData();
        game = new GameData();
        if (PlayerPrefs.HasKey(playerDataKey))
        {
            var @string = PlayerPrefs.GetString(playerDataKey);
            JsonUtility.FromJsonOverwrite(@string, player);
        }
    }
}