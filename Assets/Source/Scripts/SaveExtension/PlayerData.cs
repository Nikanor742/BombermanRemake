using System;

[Serializable]
public class PlayerData
{
    public int score = 0;
    public int level = 0;
    public int fireLevel = 1;
    public int bombLevel = 1;
    public bool hasDetonator;
    public bool hasNoClipFire;
    public bool hasNoClipWall;
    public bool hasNoClipBomb;
    public bool firstDeathAd = true;
    public int lifeCount = 3;

    public bool sound = true;
    public bool firstStart = true;
}