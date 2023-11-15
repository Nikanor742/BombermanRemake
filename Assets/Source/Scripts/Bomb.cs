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
        GameObject fire = Instantiate(FireMid,transform.position,transform.rotation);
        BoxCollider2D leftBox = fire.AddComponent<BoxCollider2D>();
        leftBox.isTrigger = true;
        float xLSize = 0.9f * (CellsToBlowL.Count + 1);
        float xLPos = -(0.9f * CellsToBlowL.Count) / 2f;
        leftBox.size = new Vector2(xLSize, 0.7f);
        leftBox.offset = new Vector2(xLPos, 0);
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
        BoxCollider2D rigtBox = fire.AddComponent<BoxCollider2D>();
        rigtBox.isTrigger = true;
        float xRSize = 0.9f * (CellsToBlowR.Count + 1);
        float xRPos = (0.9f * CellsToBlowR.Count) / 2f;
        rigtBox.size = new Vector2(xRSize, 0.7f);
        rigtBox.offset = new Vector2(xRPos, 0);
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
        BoxCollider2D upBox = fire.AddComponent<BoxCollider2D>();
        upBox.isTrigger = true;
        float yUSize = 0.9f * (CellsToBlowU.Count + 1);
        float yUPos = (0.9f * CellsToBlowU.Count) / 2f;
        upBox.size = new Vector2(0.7f, yUSize);
        upBox.offset = new Vector2(0, yUPos);
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

        BoxCollider2D downBox = fire.AddComponent<BoxCollider2D>();
        downBox.isTrigger = true;
        float yDSize = 0.9f * (CellsToBlowD.Count + 1);
        float yDPos = -(0.9f * CellsToBlowD.Count) / 2f;
        downBox.size = new Vector2(0.7f, yDSize);
        downBox.offset = new Vector2(0, yDPos);
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
