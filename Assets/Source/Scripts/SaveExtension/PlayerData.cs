using System;

[Serializable]
public class PlayerData
{
    public int score = 0;
    public int level = 0;
    public int fireLevel = 1;
    public int bombLevel = 1;
    public int lifeCount = 3;

    public ELanguages language;
    public bool sound = true;
    public bool firstStart = true;
}