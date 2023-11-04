﻿using System;

public class GameData
{
    private int monsterCountInLevel;
    public int MonsterCountInLevel
    {
        get => monsterCountInLevel;
        set
        {
            monsterCountInLevel = value;
            OnMonsterCountChanged?.Invoke();
        }
    }

    public event Action OnMonsterCountChanged;
    
}