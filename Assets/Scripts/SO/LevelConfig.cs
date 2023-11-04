using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable/LevelsConfig",fileName ="LevelsConfig")]
public class LevelConfig : ScriptableObject
{
    public LevelSettings[] levels;
}

[Serializable]
public class LevelSettings
{
    public Monster[] monsters;
    public EBonusType[] bonuses;
    
}

[Serializable]
public class Monster
{
    public EMonsterType monsterType;
    public int count;
    public void Clone(Monster clonableMonster)
    {
        monsterType = clonableMonster.monsterType;
        count = clonableMonster.count;
    }
}

