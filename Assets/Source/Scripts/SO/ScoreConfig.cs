using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ScoreConfig", fileName = "ScoreConfig")]
public class ScoreConfig : ScriptableObject
{
    public ScoreSettings[] scoreSettings;
}

[Serializable]
public class ScoreSettings
{
    public EScoreType scoreType;
    public int addScore;
}
