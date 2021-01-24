using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Bomberman : MonoBehaviour
{
    private bool buttonLeft;
    private bool buttonRight;
    private bool buttonUp;
    private bool buttonDown;
    private bool buttonBomb;
    private bool buttonDetonate;

    private int BombsAllowed;
    private int FireLenght;
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


    void Start()
    {
        BombsAllowed = 2;
        FireLenght = 1;
        SpeedBoots = 0;

        Life = PlayerPrefs.GetInt("Life");
        HasDetonator = false;
        TextBomb.text = BombsAllowed.ToString();
        TextFire.text = FireLenght.ToString();
        TextLife.text = Life.ToString();
    }


    void Update()
    {
        GetInput();
        GetDirection();
        HandleSensor();
        HandleBombs();
        Move();
        Animate();
    }

    IEnumerator ReloadScene()
    {
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
        else if(source==1 && !NoclipFire)
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
        PlayerPrefs.SetInt("Life", Life);
        PlayerPrefs.Save();
        Destroy(gameObject);
        if (Life <= 0)
        {
            Instantiate(LoseAudio, transform.position, transform.rotation);
            PlayerPrefs.SetInt("Life", 3);
            PlayerPrefs.Save();
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
            switch (other.GetComponent<PowerUp>().Type)
            //0 -Bomb
            //1 -Fire
            //2 -speed
            //3 -noclip wall
            //4 -noclip fire
            //5 -noclip bomb
            //6 -detonator
            {
                case 0:
                    {
                        GetBombPowerUp();
                        TextBomb.text = BombsAllowed.ToString();
                        break;
                    }
                case 1:
                    {
                        GetFirePowerUp();
                        TextFire.text = FireLenght.ToString();
                        break;
                    }
                case 2:
                    {
                        GetSpeedPowerUp();
                        guiBoots.SetActive(true);
                        break;
                    }
                case 3:
                    {
                        GetNoclipWallsPowerUp();
                        guiNoClipWals.SetActive(true);
                        break;
                    }
                case 4:
                    {
                        GetNoclipFirePowerUp();
                        guiNoClipFire.SetActive(true);
                        break;
                    }
                case 5:
                    {
                        GetNoclipBombsPowerUp();
                        guiNoClipBomb.SetActive(true);
                        break;
                    }
                case 6:
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
        BombsAllowed++;
    }

    void GetFirePowerUp()
    {
        FireLenght++;
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

    public void AddBomb()
    {
        BombsAllowed++;
    }
    public void AddFire()
    {
        FireLenght++;
    }

    public int GetFireLength()
    {
        return FireLenght; 
    }
    void Move()
    {
        if (Direction == 4)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Direction == 6)
        {
            GetComponent<SpriteRenderer>().flipX = false;
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
        if (buttonBomb && GameObject.FindGameObjectsWithTag("Bomb").Length < BombsAllowed && !InsideBomb &&!InsideFire&&!InsideBrick)
        {
            Instantiate(Bomb, new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)), transform.rotation);
        }
        if (buttonDetonate &&HasDetonator)
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
        switch (Direction)
        {

            case 2:
                Sensor.transform.localPosition = new Vector2(0, -sensorRange);
                break;
            case 4:
                Sensor.transform.localPosition = new Vector2(-sensorRange, 0);
                break;
            case 6:
                Sensor.transform.localPosition = new Vector2(sensorRange, 0);
                break;
            case 8:
                Sensor.transform.localPosition = new Vector2(0, sensorRange);
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
        if (buttonLeft) Direction = 4;
        if (buttonRight) Direction = 6;
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
        var animator = GetComponent<Animator>();
        animator.SetInteger("Direction", Direction);
    }
}
