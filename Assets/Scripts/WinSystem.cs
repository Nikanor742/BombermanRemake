using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSystem : MonoBehaviour
{

    private void Start()
    {
        SaveExtension.game.OnMonsterCountChanged += MonsterCountChanged;
    }
    private void MonsterCountChanged()
    {
        Debug.Log("Changed:"+SaveExtension.game.MonsterCountInLevel);
    }

    private void OnDestroy()
    {
        SaveExtension.game.OnMonsterCountChanged -= MonsterCountChanged;
    }
}
