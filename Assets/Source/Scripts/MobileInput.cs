using System.Collections;
using UnityEngine;

public class MobileInput : MonoBehaviour,IInput
{
    public bool ButtonLeft { get => buttonLeft; }
    public bool ButtonRight { get => buttonRight; }
    public bool ButtonUp { get => buttonUp; }
    public bool ButtonDown { get => buttonDown; }
    public bool ButtonBomb
    {
        get => buttonBomb;
        set
        {
            buttonBomb = value;
        }
    }
    public bool ButtonDetonate 
    {
        get => buttonDetonate;
        set
        {
            buttonDetonate = value;
        }
    }

    private bool buttonLeft;
    private bool buttonRight;
    private bool buttonUp;
    private bool buttonDown;
    private bool buttonBomb;
    private bool buttonDetonate;

    private Joystick _joystick;
    [SerializeField] private MobileButton UIbuttonBomb;
    [SerializeField] private MobileButton UIbuttonDetonate;

    public void GetButtonBomb()
    {
        buttonBomb = true;
    }

    public void GetButtonDetonate()
    {
        buttonDetonate = true;
    }

    public void GetButtonDown()
    {
        float y = _joystick.Direction.y;
        buttonDown = y <= -0.5f ? true : false;
    }

    public void GetButtonLeft()
    {
        float x = _joystick.Direction.x;
        buttonLeft = x <= -0.5f ? true : false;
    }

    public void GetButtonRight()
    {
        float x = _joystick.Direction.x;
        buttonRight = x >= 0.5f ? true : false;
    }

    public void GetButtonUp()
    {
        float y = _joystick.Direction.y;
        buttonUp = y >= 0.5f ? true : false;
    }
    public void ShowDetonatorButton()
    {
        UIbuttonDetonate.gameObject.SetActive(true);
    }

    private void Awake()
    {
        _joystick = GetComponentInChildren<Joystick>();
        UIbuttonBomb.button.onClick.AddListener(GetButtonBomb);
        UIbuttonDetonate.button.onClick.AddListener(GetButtonDetonate);
    }
    private void Update()
    {
        GetButtonUp();
        GetButtonDown();
        GetButtonLeft();
        GetButtonRight();
    }

    private void OnDestroy()
    {
        UIbuttonBomb.button.onClick.RemoveAllListeners();
        UIbuttonDetonate.button.onClick.RemoveAllListeners();
    }
}
