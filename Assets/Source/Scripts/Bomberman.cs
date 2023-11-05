using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Bomberman : MonoBehaviour
{
    private bool buttonLeft;
    private bool buttonRight;
    private bool buttonUp;
    private bool buttonDown;
    private bool buttonBomb;
    private bool buttonDetonate;

    private int SpeedBoots;
    private int Life;
    private bool NoclipWalls;
    private bool NoclipFire;
    private bool NoclipBombs;
    private bool HasDetonator;

    private bool canMove;
    private bool InsideBomb;
    private bool InsideFire;
    private bool InsideBrick;
    [SerializeField] private Text TextBomb;
    [SerializeField] private Text TextFire;
    [SerializeField] private Text TextLife;


    public int Direction;// 4<||6>||8^||2v
    public int lastDirectionX;
    public Transform Sensor;
    public float sensorSize = 0.7f;
    public float sensorRange = 0.4f;
    public float MoveSpeed = 2;

    public LayerMask StoneLayer;
    public LayerMask BombLayer;
    public LayerMask BrickLayer;
    public LayerMask FireLayer;
    public GameObject Bomb;
    public GameObject DeathEffect;
    public GameObject LoseAudio;

    public GameObject guiDetonator;
    public GameObject guiBoots;
    public GameObject guiNoClipBomb;
    public GameObject guiNoClipFire;
    public GameObject guiNoClipWals;
    public GameObject guiDeath;

    private SpriteRenderer sprite;
    private Animator animator;

    private bool playerActive = true;


    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        SpeedBoots = 0;
        Life = SaveExtension.player.lifeCount;
        HasDetonator = false;
        TextBomb.text = SaveExtension.player.bombLevel.ToString();
        TextFire.text = SaveExtension.player.fireLevel.ToString();
        TextLife.text = Life.ToString();
        SaveExtension.game.startBombLevel = SaveExtension.player.bombLevel;
        SaveExtension.game.startFireLevel = SaveExtension.player.fireLevel;
    }


    void Update()
    {
        if (playerActive)
        {
            GetInput();
            GetDirection();
            HandleSensor();
            HandleBombs();
            Move();
            Animate();
        }
    }

    IEnumerator ReloadScene()
    {
        AudioPlayer.Instance.PlaySound(ESoundType.fail);
        SaveExtension.player.fireLevel = SaveExtension.game.startFireLevel;
        SaveExtension.player.bombLevel = SaveExtension.game.startBombLevel;
        SaveExtension.Save();
        yield return new WaitForSeconds(1f);
        Die();
    }
    public void Damage(int source)
    {
        if (source == 2)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Bomberman>().enabled = false;
            Instantiate(DeathEffect, transform.position, transform.rotation);
            StartCoroutine(ReloadScene());
            
        }
        else if(source == 1 && !NoclipFire)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Bomberman>().enabled = false;
            Instantiate(DeathEffect, transform.position, transform.rotation);
            StartCoroutine(ReloadScene());   
        }
    }
   
    void Die()
    {
        Life--;
        TextLife.text = Life.ToString();
        SaveExtension.player.lifeCount = Life;
        SaveExtension.Save();
        Destroy(gameObject);
        if (Life <= 0)
        {
            AudioPlayer.Instance.StopAllSounds();
            AudioPlayer.Instance.PlaySound(ESoundType.dead);
            SaveExtension.player.lifeCount = 3;
            SaveExtension.Save();
            guiDeath.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PowerUp")
        {
            AudioPlayer.Instance.PlaySound(ESoundType.powerUp);
            switch (other.GetComponent<PowerUp>().Type)
            {
                case (EBonusType)0:
                    {
                        GetBombPowerUp();
                        TextBomb.text = SaveExtension.player.bombLevel.ToString();
                        break;
                    }
                case (EBonusType)1:
                    {
                        GetFirePowerUp();
                        TextFire.text = SaveExtension.player.fireLevel.ToString();
                        break;
                    }
                case (EBonusType)2:
                    {
                        GetSpeedPowerUp();
                        guiBoots.SetActive(true);
                        break;
                    }
                case (EBonusType)3:
                    {
                        GetNoclipWallsPowerUp();
                        guiNoClipWals.SetActive(true);
                        break;
                    }
                case (EBonusType)4:
                    {
                        GetNoclipFirePowerUp();
                        guiNoClipFire.SetActive(true);
                        break;
                    }
                case (EBonusType)5:
                    {
                        GetNoclipBombsPowerUp();
                        guiNoClipBomb.SetActive(true);
                        break;
                    }
                case (EBonusType)6:
                    {
                        GetDetonatorPowerUp();
                        guiDetonator.SetActive(true);
                        break;
                    }
            }
            Destroy(other.gameObject);
        }
        
    }

    void GetBombPowerUp()
    {
        SaveExtension.player.bombLevel++;
        SaveExtension.Save();
    }

    void GetFirePowerUp()
    {
        SaveExtension.player.fireLevel++;
        SaveExtension.Save();
    }

    void GetSpeedPowerUp()
    {
        SpeedBoots++;
        MoveSpeed += SpeedBoots;
    }

    void GetNoclipWallsPowerUp()
    {
        NoclipWalls = true;
    }

    void GetNoclipFirePowerUp()
    {
        NoclipFire = true;
    }

    void GetNoclipBombsPowerUp()
    {
        NoclipBombs = true;
    }

    void GetDetonatorPowerUp()
    {
        HasDetonator = true;
    }

    public bool CheckDetonator()
    {
        return HasDetonator;
    }
    void Move()
    {
        if (Direction == 4)
        {
            sprite.flipX = true;
        }
        if (Direction == 6)
        {
            sprite.flipX = false;
        }
        if (!canMove)
        {
            return;
        }

        switch (Direction)
        {
            case 2:
                transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y - MoveSpeed * Time.deltaTime);
                break;
            case 4:
                transform.position = new Vector2(transform.position.x - MoveSpeed * Time.deltaTime, Mathf.Round(transform.position.y));
                break;
            case 6:
                transform.position = new Vector2(transform.position.x + MoveSpeed * Time.deltaTime, Mathf.Round(transform.position.y));
                break;
            case 8:
                transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y + MoveSpeed * Time.deltaTime);
                break;
        }
    }
    void HandleBombs()
    {
        if (buttonBomb && GameObject.FindGameObjectsWithTag("Bomb").Length < SaveExtension.player.bombLevel
            && !InsideBomb
            && !InsideFire
            && !InsideBrick)
        {
            Instantiate(Bomb, new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)), transform.rotation);
        }
        if (buttonDetonate && HasDetonator)
        {
            var bombs = FindObjectsOfType<Bomb>();
            foreach(var bomb in bombs)
            {
                bomb.Blow();
            }
        }
    }
    void HandleSensor()
    {
        Sensor.transform.localPosition = new Vector2(0, 0);
        InsideBomb = Physics2D.OverlapBox(Sensor.position, new Vector2(sensorSize, sensorSize), 0, BombLayer);
        InsideFire= Physics2D.OverlapBox(Sensor.position, new Vector2(sensorSize, sensorSize), 0, FireLayer);
        InsideBrick = Physics2D.OverlapBox(Sensor.position, new Vector2(sensorSize, sensorSize), 0, BrickLayer);
        Vector2 sensorPos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        switch (Direction)
        {
            case 2:
                Sensor.transform.position = new Vector2(sensorPos.x, -sensorRange);
                Sensor.transform.localPosition = new Vector2(Sensor.transform.localPosition.x, -sensorRange);
                break;
            case 4:
                Sensor.transform.position = new Vector2(-sensorRange, sensorPos.y);
                Sensor.transform.localPosition = new Vector2(-sensorRange, 0);
                break;
            case 6:
                Sensor.transform.position = new Vector2(sensorRange, sensorPos.y);
                Sensor.transform.localPosition = new Vector2(sensorRange, 0);
                break;
            case 8:
                Sensor.transform.position = new Vector2(sensorPos.x, sensorRange);
                Sensor.transform.localPosition = new Vector2(Sensor.transform.localPosition.x, sensorRange);
                break;
        }
        canMove = !Physics2D.OverlapBox(Sensor.position, new Vector2(sensorSize, sensorSize), 0, StoneLayer);
        if (canMove)
        {
            if (!NoclipWalls)
            {
                canMove = !Physics2D.OverlapBox(Sensor.position, new Vector2(sensorSize, sensorSize), 0, BrickLayer);
            }
            
        }
        if (canMove &&!InsideBomb)
        {
            if (!NoclipBombs)
            {
                canMove = !Physics2D.OverlapBox(Sensor.position, new Vector2(sensorSize, sensorSize), 0, BombLayer);
            }
            
        }
    }
    void GetDirection()
    {
        Direction = 5;
        if (buttonLeft)
        {
            lastDirectionX = -1;
            Direction = 4;
        }
        if (buttonRight)
        {
            lastDirectionX = 1;
            Direction = 6;
        }
        if (buttonUp) Direction = 8;
        if (buttonDown) Direction = 2;
    }
    void GetInput()
    {
        buttonLeft = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
        buttonRight = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
        buttonUp = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow);
        buttonDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow);
        buttonBomb = Input.GetKeyDown(KeyCode.Z);
        buttonDetonate = Input.GetKeyDown(KeyCode.X);
    }
    void Animate()
    {
        animator.SetInteger("Direction", Direction);
    }

    public void NextLevelState(Vector3 portalPos)
    {
        playerActive = false;
        animator.SetTrigger("Portal");
        animator.SetInteger("Direction",0);
        transform.DOMove(portalPos, 0.3f);
    }
}
