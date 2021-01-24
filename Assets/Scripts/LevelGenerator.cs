using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Monster1;
    [SerializeField] private GameObject Monster2;
    [SerializeField] private GameObject Brick;

    private int monsterCount=5;
    private int[,] Level=new int[11,19];
    private int count = 6;//Координата
   
    void Start()
    {
        int count = 6;
        Level[2, 0] = 1;
        Level[0, 2] = 1;
        Instantiate(Brick, new Vector2(-5, 6), transform.rotation);
        Instantiate(Brick, new Vector2(-7, 4), transform.rotation);
        for (int i = 0; i < 11; i++)
        {
            for(int j = 0; j < 19; j++)
            {
                if(i==0 && j <= 2)
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
                    pos.x = j-7;
                    pos.y = count;
                    Instantiate(Brick, pos, transform.rotation);
                }
                else
                {
                    if (monsterCount != 0)
                    {
                        int randM = Random.Range(0, 101);
                        if (randM>=0 && randM<=8)
                        {
                            int randMonsterType = Random.Range(1, 3);
                            if (randMonsterType == 1)
                            {
                                Vector2 pos;
                                pos.x = j - 7;
                                pos.y = count;
                                Instantiate(Monster1, pos, transform.rotation);
                                monsterCount--;
                            }
                            else
                            {
                                Vector2 pos;
                                pos.x = j - 7;
                                pos.y = count;
                                Instantiate(Monster2, pos, transform.rotation);
                                monsterCount--;
                            }
                        }
                        
                    }
                    
                }
            }
            count--;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
