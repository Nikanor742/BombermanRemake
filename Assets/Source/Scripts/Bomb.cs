using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject FireMid;
    public GameObject FireTop;
    public GameObject FireBottom;
    public GameObject FireLeft;
    public GameObject FireRight;
    public GameObject FireVertical;
    public GameObject FireHorizontal;

    public float Delay;

    private bool AutoDetonate;
    private bool Animate;

    private float Counter;
    private int FireLength;
    private Bomberman Bomber;

    public LayerMask StoneLayer;
    public LayerMask BlowableLayer;
    public List<Vector2> CellsToBlowR;
    public List<Vector2> CellsToBlowL;
    public List<Vector2> CellsToBlowU;
    public List<Vector2> CellsToBlowD;


    private AudioSource BlowAudioSound;
    
    // Start is called before the first frame update
    void Start()
    {
        Counter = Delay;
        Bomber = FindObjectOfType<Bomberman>();
        AutoDetonate = Bomber.CheckDetonator();
        CellsToBlowR = new List<Vector2>();
        CellsToBlowL = new List<Vector2>();
        CellsToBlowU = new List<Vector2>();
        CellsToBlowD = new List<Vector2>();
        if (Bomber.CheckDetonator())
        {
            Animate = false;
        }
        else
        {
            Animate = true;
        }
        var animator = GetComponent<Animator>();
        animator.SetBool("Animate", Animate);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Counter > 0)
        {
            if (!AutoDetonate)
            {
                Counter -= Time.deltaTime;
            }
            
        }
        else
        {
            Blow();
        }
            
    }

  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire")
        {
            Blow();
        }

    }
    public void Blow()
    {
        AudioPlayer.Instance.PlaySound(ESoundType.explosion);
        CalculateFireDirections();
        Instantiate(FireMid,transform.position,transform.rotation);
        if (CellsToBlowL.Count > 0)
        {
            for (int i = 0; i < CellsToBlowL.Count; i++)
            {
                if (i == CellsToBlowL.Count - 1)
                {
                    Instantiate(FireLeft, CellsToBlowL[i], transform.rotation);
                }
                else
                {
                    Instantiate(FireHorizontal, CellsToBlowL[i], transform.rotation);
                }
            }
        }
        //R
        if (CellsToBlowR.Count > 0)
        {
            for (int i = 0; i < CellsToBlowR.Count; i++)
            {
                if (i == CellsToBlowR.Count - 1)
                {
                    Instantiate(FireRight, CellsToBlowR[i], transform.rotation);
                }
                else
                {
                    Instantiate(FireHorizontal, CellsToBlowR[i], transform.rotation);
                }
            }
        }

        //U
        if (CellsToBlowU.Count > 0)
        {
            for (int i = 0; i < CellsToBlowU.Count; i++)
            {
                if (i == CellsToBlowU.Count - 1)
                {
                    Instantiate(FireTop, CellsToBlowU[i], transform.rotation);
                }
                else
                {
                    Instantiate(FireVertical, CellsToBlowU[i], transform.rotation);
                }
            }
        }

        //D
        if (CellsToBlowD.Count > 0)
        {
            for (int i = 0; i < CellsToBlowD.Count; i++)
            {
                if (i == CellsToBlowD.Count - 1)
                {
                    Instantiate(FireBottom, CellsToBlowD[i], transform.rotation);
                }
                else
                {
                    Instantiate(FireVertical, CellsToBlowD[i], transform.rotation);
                }
            }
        }

        Destroy(gameObject);
        
    }
    void CalculateFireDirections()
    {
        FireLength = SaveExtension.player.fireLevel;
        //L
        for (int i = 1; i <= FireLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x - i, transform.position.y), 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x - i, transform.position.y), 0.1f, BlowableLayer))
            {
                CellsToBlowL.Add(new Vector2(transform.position.x - i, transform.position.y));
                break;
            }
            CellsToBlowL.Add(new Vector2(transform.position.x - i, transform.position.y));
        }
        //R
        for (int i = 1; i <= FireLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x + i, transform.position.y), 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x + i, transform.position.y), 0.1f, BlowableLayer))
            {
                CellsToBlowR.Add(new Vector2(transform.position.x + i, transform.position.y));
                break;
            }
            CellsToBlowR.Add(new Vector2(transform.position.x + i, transform.position.y));
        }
        //U
        for (int i = 1; i <= FireLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + i), 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + i), 0.1f, BlowableLayer))
            {
                CellsToBlowU.Add(new Vector2(transform.position.x, transform.position.y + i));
                break;
            }
            CellsToBlowU.Add(new Vector2(transform.position.x, transform.position.y + i));
        }
        //D
        for (int i = 1; i <= FireLength; i++)
        {
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - i), 0.1f, StoneLayer))
            {
                break;
            }
            if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - i), 0.1f, BlowableLayer))
            {
                CellsToBlowD.Add(new Vector2(transform.position.x, transform.position.y - i));
                break;
            }
            CellsToBlowD.Add(new Vector2(transform.position.x, transform.position.y - i));
        }
    }
    void OnDrawGizmos()
    {
        if (CellsToBlowL != null)
        {
            foreach(var item in CellsToBlowL)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(item,0.2f);
            }
        }
        if (CellsToBlowR != null)
        {
            foreach (var item in CellsToBlowR)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(item, 0.2f);
            }
        }
        if (CellsToBlowU != null)
        {
            foreach (var item in CellsToBlowU)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(item, 0.2f);
            }
        }
        if (CellsToBlowD != null)
        {
            foreach (var item in CellsToBlowD)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(item, 0.2f);
            }
        }
    }
}
