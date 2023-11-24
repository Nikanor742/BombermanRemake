using Cinemachine;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Bomberman : MonoBehaviour
{
    private int SpeedBoots;
    private int Life;
    private bool NoclipWalls;
    private bool NoclipFire;
    private bool NoclipBombs;
    private bool HasDetonator;

    private bool handleBomb = true;
    private bool handleDetonator = true;

    private bool dead;
    private bool canMove;
    private bool InsideBomb;
    private bool InsideFire;
    private bool InsideBrick;
    [SerializeField] private MobileInput mobileInputPrefab;
    [SerializeField] private TextMeshProUGUI TextBomb;
    [SerializeField] private TextMeshProUGUI TextFire;
    [SerializeField] private TextMeshProUGUI TextLife;


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
    public GameUI gui;

    private SpriteRenderer sprite;
    private Animator animator;

    private bool playerActive = true;

    private IInput _input;

    private void Set()
    {
        if (_input == null)
        {
            if (YandexGame.EnvironmentData.isDesktop)
            {
                _input = gameObject.AddComponent<DesktopInput>();
            }
            else if (YandexGame.EnvironmentData.isMobile)
            {
                _input = Instantiate(mobileInputPrefab);
                var camera = FindObjectOfType<CinemachineVirtualCamera>();
                camera.m_Lens.OrthographicSize = 4.5f;
                camera.Follow = transform;
                camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 0, -10);
            }
        }
        GetBonusSaves();
    }
    private void OnYandexSDKInitialized()
    {
        Set();
        YandexGame.RewardVideoEvent += AddRewardedLife;
    }
    void Start()
    {
        SaveExtension.game.OnYandexSDKInitialized += OnYandexSDKInitialized;
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
        if (playerActive && _input != null)
        {
            GetDirection();
            HandleSensor();
            HandleBombs();
            Move();
            Animate();
        }
    }

    private void AddRewardedLife(int ID)
    {
        if (ID == 0)
        {
            SaveExtension.player.lifeCount = 1;
            SaveExtension.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        if (source == 2 && !dead)
        {
            dead = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Bomberman>().enabled = false;
            Instantiate(DeathEffect, transform.position, transform.rotation);
            StartCoroutine(ReloadScene());
            
        }
        else if(source == 1 && !NoclipFire && !dead)
        {
            dead = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Bomberman>().enabled = false;
            Instantiate(DeathEffect, transform.position, transform.rotation);
            StartCoroutine(ReloadScene());
        }
    }
   
    void Die()
    {
        HapticSystem.Haptic(true, EHapticType.death);
        Life--;
        TextLife.text = Life.ToString();
        SaveExtension.player.lifeCount = Life;
        SaveExtension.player.hasDetonator = false;
        SaveExtension.player.hasNoClipBomb = false;
        SaveExtension.player.hasNoClipWall = false;
        SaveExtension.player.hasNoClipFire = false;
        //Destroy(gameObject);
        if (Life <= 0)
        {
            gui.death.SetActive(true);
            YandexGame.FullscreenShow();
            AudioPlayer.Instance.StopAllSounds();
            AudioPlayer.Instance.PlaySound(ESoundType.dead);
            SaveExtension.player.lifeCount = 3;
            if (SaveExtension.player.firstDeathAd)
            {
                SaveExtension.player.firstDeathAd = false;
                gui.adButton.gameObject.SetActive(true);
            }
            else
            {
                SaveExtension.player.firstDeathAd = true;
                gui.adButton.gameObject.SetActive(false);
                SaveExtension.player.level = 0;
                SaveExtension.player.fireLevel = 1;
                SaveExtension.player.bombLevel = 1;
                SaveExtension.player.hasDetonator = false;
                SaveExtension.player.hasNoClipBomb = false;
                SaveExtension.player.hasNoClipWall = false;
                SaveExtension.player.hasNoClipFire = false;
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        SaveExtension.Save();
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
                case (EBonusType)7:
                    {
                        SaveExtension.player.lifeCount++;
                        Life++;
                        TextLife.text = Life.ToString();
                        SaveExtension.Save();
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
        SaveExtension.player.hasNoClipWall = true;
        SaveExtension.Save();
        NoclipWalls = true;
    }

    void GetNoclipFirePowerUp()
    {
        SaveExtension.player.hasNoClipFire = true;
        SaveExtension.Save();
        NoclipFire = true;
    }

    void GetNoclipBombsPowerUp()
    {
        NoclipBombs = true;
    }

    void GetDetonatorPowerUp()
    {
        SaveExtension.player.hasDetonator = true;
        SaveExtension.Save();
        _input.ShowDetonatorButton();        
        HasDetonator = true;
    }

    void GetBonusSaves()
    {
        if (SaveExtension.player.hasDetonator)
        {
            HasDetonator = true;
            guiDetonator.SetActive(true);
            _input.ShowDetonatorButton();
        }
        if (SaveExtension.player.hasNoClipFire)
        {
            NoclipFire = true;
            guiNoClipFire.SetActive(true);
        }
        if (SaveExtension.player.hasNoClipWall)
        {
            NoclipWalls = true;
            guiNoClipWals.SetActive(true);
        }
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
    private IEnumerator TimeToHandleBomb()
    {
        yield return new WaitForSeconds(0.1f);
        handleBomb = true;
    }
    private IEnumerator TimeToHandleDetonator()
    {
        yield return new WaitForSeconds(0.1f);
        handleDetonator = true;
    }
    void HandleBombs()
    {
        if (_input.ButtonBomb && GameObject.FindGameObjectsWithTag("Bomb").Length < SaveExtension.player.bombLevel
            && !InsideBomb
            && !InsideFire
            && !InsideBrick
            && handleBomb)
        {
            handleBomb = false;
            _input.ButtonBomb = false;
            StartCoroutine(TimeToHandleBomb());
            HapticSystem.Haptic(true, EHapticType.bombPlant);
            Instantiate(Bomb, new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)), transform.rotation);
        }
        else
        {
            _input.ButtonBomb = false;
        }
        if (_input.ButtonDetonate && HasDetonator && handleDetonator)
        {
            handleDetonator = false;
            _input.ButtonDetonate = false;
            StartCoroutine(TimeToHandleDetonator());
            var bombs = FindObjectsOfType<Bomb>();
            foreach(var bomb in bombs)
            {
                bomb.Blow();
            }
        }
        else
        {
            _input.ButtonDetonate = false;
        }
    }
    void HandleSensor()
    {
        Sensor.transform.localPosition = new Vector2(0, 0);
        InsideBomb = Physics2D.OverlapBox(Sensor.position, new Vector2(sensorSize, sensorSize), 0, BombLayer);
        InsideFire = Physics2D.OverlapBox(Sensor.position, new Vector2(sensorSize, sensorSize), 0, FireLayer);
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
                Sensor.transform.localPosition = new Vector2(-sensorRange, Sensor.transform.localPosition.y);
                break;
            case 6:
                Sensor.transform.position = new Vector2(sensorRange, sensorPos.y);
                Sensor.transform.localPosition = new Vector2(sensorRange, Sensor.transform.localPosition.y);
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
        if (_input.ButtonLeft)
        {
            lastDirectionX = -1;
            Direction = 4;
        }
        if (_input.ButtonRight)
        {
            lastDirectionX = 1;
            Direction = 6;
        }
        if (_input.ButtonUp) Direction = 8;
        if (_input.ButtonDown) Direction = 2;
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
    private void OnDestroy()
    {
        YandexGame.RewardVideoEvent -= AddRewardedLife;
        SaveExtension.game.OnYandexSDKInitialized -= OnYandexSDKInitialized;
    }
}
