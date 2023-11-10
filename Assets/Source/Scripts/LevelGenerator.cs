using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using YG;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private LevelConfig levelsConfig;
    [SerializeField] private Enemy[] monsters;
    [SerializeField] private PowerUp[] powerUPs;
    [SerializeField] private Brick Brick;

    private int[,] Level = new int[11,19];

    private List<EBonusType> levelBonuses;
    private List<Monster> levelMonsters;

    
    private void Set()
    {
        int level = SaveExtension.player.level;
        if (level == 0 && YandexGame.EnvironmentData.isDesktop)
        {
            ControllHelp.Instance.ShowBasicHelp();
        }
    }
    private void OnYandexSDKInitialized()
    {
        Set();
    }

    void Start()
    {
        SaveExtension.game.OnYandexSDKInitialized += OnYandexSDKInitialized;

        int level = SaveExtension.player.level;
        string text = "";
        if (SaveExtension.player.language == ELanguages.RU)
        {
            text = "уровень ";
        }
        else if (SaveExtension.player.language == ELanguages.EN)
        {
            text = "level ";
        }
        else if (SaveExtension.player.language == ELanguages.TR)
        {
            text = "seviye ";
        }
        levelText.text = text + (level + 1).ToString();

        levelBonuses = new List<EBonusType>();
        foreach (var b in levelsConfig.levels[level].bonuses)
            levelBonuses.Add(b);

        levelMonsters = new List<Monster>();
        foreach (var m in levelsConfig.levels[level].monsters)
        {
            var newMonster = new Monster();
            newMonster.Clone(m);
            levelMonsters.Add(newMonster);
        }
            
        

        int count = 6;
        Level[2, 0] = 1;
        Level[0, 2] = 1;
        CreateBrick(new Vector2(-5, 6));
        CreateBrick(new Vector2(-7, 4));
        for (int i = 0; i < 11; i++)
        {
            for(int j = 0; j < 19; j++)
            {
                if(i == 0 && j <= 2)
                {
                    continue;
                }
                if(j==0 && i <= 2)
                {
                    continue;
                }
                if (i % 2 != 0 && j % 2 != 0)
                {
                    continue;
                }
                int randBrick = Random.Range(1, 101);
                if (randBrick >= 0 && randBrick <= 50)
                {
                    Level[i, j] = 1;
                }
                else
                {
                    Level[i, j] = 0;
                }


                if (Level[i, j] == 1)
                {
                    Vector2 pos;
                    pos.x = j - 7;
                    pos.y = count;
                    CreateBrick(pos);
                }
                else
                {
                    Vector2 pos = new Vector2(j - 7, count);
                    CreateMonster(pos);
                }
            }
            count--;
        }
    }
    private void CreateMonster(Vector2 pos)
    {
        int randomValue = Random.Range(0, 100);
        if (randomValue >= 80)
        {
            if (levelMonsters.Count != 0)
            {
                if (levelMonsters[0].count > 0)
                {
                    var monster = monsters.Where(p => p.monsterType == levelMonsters[0].monsterType).First();
                    levelMonsters[0].count--;
                    var newMonster = Instantiate(monster, pos, transform.rotation);
                    SaveExtension.game.MonsterCountInLevel++;
                }
                else
                {
                    levelMonsters.RemoveAt(0);
                }
            }
        }
    }
    private void CreateBrick(Vector2 pos)
    {
        var brick = Instantiate(Brick, pos, transform.rotation);
        CreateBonus(pos,brick);
    }
    private void CreateBonus(Vector2 pos,Brick brick)
    {
        int randomValue = Random.Range(0, 100);
        if(randomValue >= 90)
        {
            if (levelBonuses.Count != 0)
            {
                var powerUp = powerUPs.Where(p => p.Type == levelBonuses[0]).First();
                levelBonuses.RemoveAt(0);
                var newPowerUp = Instantiate(powerUp, pos, transform.rotation);
                brick.hiddenPowerUp = newPowerUp;
                brick.hiddenPowerUp.gameObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        SaveExtension.game.OnYandexSDKInitialized -= OnYandexSDKInitialized;
    }
}
