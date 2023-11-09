using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public EMonsterType monsterType;
    public GameObject DeathEffect;
    [HideInInspector] public List<Vector2> CellsToMoveR;
    [HideInInspector] public List<Vector2> CellsToMoveL;
    [HideInInspector] public List<Vector2> CellsToMoveU;
    [HideInInspector] public List<Vector2> CellsToMoveD;
    [HideInInspector] public List<int> Directions;
    public LayerMask StopLayer;
    public float speed = 1f;
    public bool isMoving;
    public bool smart = false;

    private int MoveLength = 19;
    private int moveDirection;

    private Bomberman player;

    void Start()
    {
        player = FindObjectOfType<Bomberman>();
        isMoving = false;
    }

    public void Damage(int source)
    {
        if (source == 1)
        {
            AudioPlayer.Instance.PlaySound(ESoundType.monster);
            ScoreSystem.Instance.AddScore(EScoreType.monsterDefeat);
            SaveExtension.game.MonsterCountInLevel--;
            Instantiate(DeathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!isMoving)
        {
            CalculateMoveDirections();
        }
        if (CellsToMoveR.Count == 0 && CellsToMoveL.Count == 0 && CellsToMoveU.Count == 0 && CellsToMoveD.Count == 0)
        {
            return;
        }
        else
        {
            Move();
        }
    }

    void ClearDirections()
    {
        CellsToMoveD.Clear();
        CellsToMoveL.Clear();
        CellsToMoveR.Clear();
        CellsToMoveU.Clear();
        
    }

    Vector2 GetPosOnDirection(Vector2 pos, int direction)
    {
        switch (Directions[moveDirection])
        {
            case 2:

                break;
            case 4:
                break;
            case 6:
                break;
            case 8:
                break;

        }
        return Vector2.zero;
    }

    void Move()
    {
        Vector2 playerPos = player.transform.position;
        playerPos.x = Mathf.Round(playerPos.x);
        playerPos.y = Mathf.Round(playerPos.y);
        if (!isMoving)
        {
            if (smart)
            {
                if(Directions.Count > 1)
                {
                    moveDirection = 0;
                    for (int i = 0; i < Directions.Count; i++)
                    {
                        Vector2 pos = GetPosOnDirection(transform.position, Directions[i]);
                        Vector2 posCurrent = GetPosOnDirection(transform.position, Directions[moveDirection]);
                        if(Vector2.Distance(playerPos, pos) < Vector2.Distance(playerPos, posCurrent))
                        {
                            moveDirection = i;
                        }
                    }
                }
                else if (Directions.Count == 1)
                {
                    moveDirection = 0;
                }
                else
                {
                    return;
                }
            }
            else
            {
                moveDirection = Random.Range(0, Directions.Count);
            }
        }
        switch (Directions[moveDirection])
        {
            case 2:
                {
                    try
                    {
                        if (Vector2.Distance(transform.position, CellsToMoveD[CellsToMoveD.Count - 1]) > 0)
                        {
                            if (Vector2.Distance(transform.position, CellsToMoveD[0]) > 0)
                            {
                                transform.position = Vector2.MoveTowards(transform.position, CellsToMoveD[0], speed * Time.deltaTime);
                            }

                            else
                            {
                                ClearDirections();
                                CalculateMoveDirections();
                            }
                            isMoving = true;
                        }
                        else
                        {
                            isMoving = false;
                            ClearDirections();
                            Directions.Clear();
                            CalculateMoveDirections();
                        }
                    }
                    catch
                    {
                        isMoving = true;
                        ClearDirections();
                        Directions.Clear();
                        CalculateMoveDirections();
                        moveDirection = Random.Range(0, Directions.Count);
                        return;
                       
                    }
                    break;
                }
                
            case 4:
                {
                    try
                    {
                        if (Vector2.Distance(transform.position, CellsToMoveL[CellsToMoveL.Count - 1]) > 0)
                        {
                            if (Vector2.Distance(transform.position, CellsToMoveL[0]) > 0)
                            {
                                transform.position = Vector2.MoveTowards(transform.position, CellsToMoveL[0], speed * Time.deltaTime);
                            }

                            else
                            {
                                ClearDirections();
                                CalculateMoveDirections();
                            }
                            isMoving = true;
                        }
                        else
                        {
                            isMoving = false;
                            ClearDirections();
                            Directions.Clear();
                            CalculateMoveDirections();
                        }
                      
                    }
                    catch
                    {
                        
                        isMoving = true;
                        ClearDirections();
                        Directions.Clear();
                        CalculateMoveDirections();
                        moveDirection = Random.Range(0, Directions.Count);
                        return;
                    }
                    break;
                }
                
            case 6:
                {
                    try
                    {
                        if (Vector2.Distance(transform.position, CellsToMoveR[CellsToMoveR.Count - 1]) > 0)
                        {
                            if (Vector2.Distance(transform.position, CellsToMoveR[0]) > 0)
                            {
                                transform.position = Vector2.MoveTowards(transform.position, CellsToMoveR[0], speed * Time.deltaTime);
                            }

                            else
                            {
                                ClearDirections();
                                CalculateMoveDirections();
                            }
                            isMoving = true;
                        }
                        else
                        {
                            isMoving = false;
                            ClearDirections();
                            Directions.Clear();
                            CalculateMoveDirections();
                        }
                    }
                    catch
                    {
                     
                        isMoving = true;
                        ClearDirections();
                        Directions.Clear();
                        CalculateMoveDirections();
                        moveDirection = Random.Range(0, Directions.Count);
                        return;
                    }
                    break;
                }
                
            case 8:
                {
                    try
                    {
                        if (Vector2.Distance(transform.position, CellsToMoveU[CellsToMoveU.Count - 1]) > 0)
                        {
                            if (Vector2.Distance(transform.position, CellsToMoveU[0]) > 0)
                            {
                                transform.position = Vector2.MoveTowards(transform.position, CellsToMoveU[0], speed * Time.deltaTime);
                            }

                            else
                            {
                                ClearDirections();
                                CalculateMoveDirections();
                            }
                            isMoving = true;
                        }
                        else
                        {
                            isMoving = false;
                            ClearDirections();
                            Directions.Clear();
                            CalculateMoveDirections();
                        }
                    }
                    catch
                    {
                     
                        isMoving = true;
                        ClearDirections();
                        Directions.Clear();
                        CalculateMoveDirections();
                        moveDirection = Random.Range(0, Directions.Count);
                        return;
                    }
                    break;
                }
        }

     
    }
    public void FindPlayer()
    {
        Vector2 playerPos = FindObjectOfType<Bomberman>().transform.position;
        playerPos.x = Mathf.Round(playerPos.x);
        playerPos.y = Mathf.Round(playerPos.y);

        Vector2 bossPos = transform.position;
        bossPos.x = Mathf.Round(bossPos.x);
        bossPos.y = Mathf.Round(bossPos.y);

        var roundCells = new List<Vector2>();

        //L
        for (int i = 1; i <= MoveLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(bossPos.x - i, bossPos.y), 0.1f, StopLayer))
            {
                break;
            }
            roundCells.Add(new Vector2(bossPos.x - i, bossPos.y));
        }
        //R
        for (int i = 1; i <= MoveLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(bossPos.x + i, bossPos.y), 0.1f, StopLayer))
            {
                break;
            }
            roundCells.Add(new Vector2(bossPos.x + i, bossPos.y));
        }
        //U
        for (int i = 1; i <= MoveLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(bossPos.x, bossPos.y + i), 0.1f, StopLayer))
            {
                break;
            }
            roundCells.Add(new Vector2(bossPos.x, bossPos.y + i));
        }
        //D
        for (int i = 1; i <= MoveLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(bossPos.x, bossPos.y - i), 0.1f, StopLayer))
            {
                break;
            }
            roundCells.Add(new Vector2(bossPos.x, bossPos.y - i));
        }

        if (CellsToMoveD.Count > 0)
        {
            Directions.Add(2);
        }
        if (CellsToMoveL.Count > 0)
        {
            Directions.Add(4);
        }
        if (CellsToMoveR.Count > 0)
        {
            Directions.Add(6);
        }
        if (CellsToMoveU.Count > 0)
        {
            Directions.Add(8);
        }
    }

    public void CalculateMoveDirections()
    {
        //L
        for (int i = 1; i <= MoveLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x - i, transform.position.y), 0.1f, StopLayer))
            {
                break;
            }
            CellsToMoveL.Add(new Vector2(Mathf.Round(transform.position.x - i), Mathf.Round(transform.position.y)));
        }
        //R
        for (int i = 1; i <= MoveLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x + i, transform.position.y), 0.1f, StopLayer))
            {
                break;
            }
            CellsToMoveR.Add(new Vector2(Mathf.Round(transform.position.x + i), Mathf.Round(transform.position.y)));
        }
        //U
        for (int i = 1; i <= MoveLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + i), 0.1f, StopLayer))
            {
                break;
            }
            CellsToMoveU.Add(new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y + i)));
        }
        //D
        for (int i = 1; i <= MoveLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - i), 0.1f, StopLayer))
            {
                break;
            }
            CellsToMoveD.Add(new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y - i)));
        }

        if (CellsToMoveD.Count > 0)
        {
            Directions.Add(2);
        }
        if (CellsToMoveL.Count > 0)
        {
            Directions.Add(4);
        }
        if (CellsToMoveR.Count > 0)
        {
            Directions.Add(6);
        }
        if (CellsToMoveU.Count > 0)
        {
            Directions.Add(8);
        }
    }
    void OnDrawGizmos()
    {
        if (CellsToMoveR != null)
        {
            foreach (Vector2 item in CellsToMoveR)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(item, 0.2f);
            }
        }
        if (CellsToMoveL != null)
        {
            foreach (Vector2 item in CellsToMoveL)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(item, 0.2f);
            }
        }
        if (CellsToMoveU != null)
        {
            foreach (Vector2 item in CellsToMoveU)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(item, 0.2f);
            }
        }
        if (CellsToMoveD != null)
        {
            foreach (Vector2 item in CellsToMoveD)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(item, 0.2f);
            }
        }

    }
}