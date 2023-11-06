using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInput : MonoBehaviour,IInput
{
    public bool ButtonLeft { get => buttonLeft; }
    public bool ButtonRight { get => buttonRight; }
    public bool ButtonUp { get => buttonUp; }
    public bool ButtonDown { get => buttonDown; }
    public bool ButtonBomb { get => buttonBomb; }
    public bool ButtonDetonate { get => buttonDetonate; }

    private bool buttonLeft;
    private bool buttonRight;
    private bool buttonUp;
    private bool buttonDown;
    private bool buttonBomb;
    private bool buttonDetonate;

    private Joystick _joystick;
    [SerializeField] private Button UIbuttonBomb;
    [SerializeField] private Button UIbuttonDetonate;

    public void GetButtonBomb()
    {

    }

    public void GetButtonDetonate()
    {

    }

    public void GetButtonDown()
    {
        /*float y = _joystick.Direction.y;
        float x = _joystick.Direction.x;
        buttonDown = x > y && y < 0 ? true : false;*/
    }

    public void GetButtonLeft()
    {
        float y = _joystick.Direction.y;
        float x = _joystick.Direction.x;
        buttonLeft = y < x && x < 0 ? true : false;
    }

    public void GetButtonRight()
    {
        /*float y = _joystick.Direction.y;
        float x = _joystick.Direction.x;
        buttonLeft = y > x && x > 0 ? true : false;*/
    }

    public void GetButtonUp()
    {
        /*float y = _joystick.Direction.y;
        float x = _joystick.Direction.x;
        buttonUp = x < y && y > 0 ? true : false;*/
    }
    private void Awake()
    {
        _joystick = GetComponentInChildren<Joystick>();
    }
    private void Update()
    {
        GetButtonUp();
        GetButtonDown();
        GetButtonLeft();
        GetButtonRight();
        GetButtonBomb();
        GetButtonDetonate();
    }
}
