using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class WinSystem : MonoBehaviour
{
    [SerializeField] private Portal portal;
    [SerializeField] private GameObject yellowStar;
    [SerializeField] private GameObject redStar;

    private void Start()
    {
        SaveExtension.game.OnMonsterCountChanged += MonsterCountChanged;
    }
    private void MonsterCountChanged()
    {
        if(SaveExtension.game.MonsterCountInLevel == 0)
        {
            YandexGame.NewLeaderboardScores("MainLeaderboard", SaveExtension.player.score);
            AudioPlayer.Instance.PlaySound(ESoundType.levelComplete);
            ScoreSystem.Instance.AddScore(EScoreType.levelComplete,Vector3.zero);
            var allBricksNoBonus = FindObjectsOfType<Brick>().Where(b => b.hiddenPowerUp == null).ToArray();
            var allBricksBonus = FindObjectsOfType<Brick>().Where(b => b.hiddenPowerUp != null).ToArray();
            foreach (var b in allBricksBonus)
            {
                var newStar = Instantiate(redStar, b.transform);
                newStar.transform.localPosition = Vector3.zero;
            }
            if (allBricksNoBonus.Length != 0)
            {
                int randomCell = Random.Range(0, allBricksNoBonus.Length);
                var pos = allBricksNoBonus[randomCell].transform.position;
                var newPortal = Instantiate(portal, new Vector3(pos.x,pos.y,-2f), Quaternion.identity);
                newPortal.rb.simulated = false;
                allBricksNoBonus[randomCell].hiddenPortal = newPortal;
                var newStar = Instantiate(yellowStar, allBricksNoBonus[randomCell].transform);
                newStar.transform.localPosition = Vector3.zero;
            }
            else
            {
                var newPortal = Instantiate(portal, new Vector3(-7, 6, -2f), Quaternion.identity);
            }
        }
    }

    private void OnDestroy()
    {
        SaveExtension.game.OnMonsterCountChanged -= MonsterCountChanged;
    }
}
