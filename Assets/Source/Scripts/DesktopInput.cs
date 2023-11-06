using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInput : MonoBehaviour,IInput
{
    public bool ButtonLeft { get => buttonLeft;}
    public bool ButtonRight { get => buttonRight;}
    public bool ButtonUp { get => buttonUp;}
    public bool ButtonDown { get => buttonDown;}
    public bool ButtonBomb { get => buttonBomb;}
    public bool ButtonDetonate { get => buttonDetonate;}

    private bool buttonLeft;
    private bool buttonRight;
    private bool buttonUp;
    private bool buttonDown;
    private bool buttonBomb;
    private bool buttonDetonate;

    public void GetButtonBomb()
    {
        buttonBomb = Input.GetMouseButton(0);
    }

    public void GetButtonDetonate()
    {
        buttonDetonate = Input.GetMouseButton(1);
    }

    public void GetButtonDown()
    {
        buttonDown = Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W);
    }

    public void GetButtonLeft()
    {
        buttonLeft = Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S);
    }

    public void GetButtonRight()
    {
        buttonRight = Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S);
    }

    public void GetButtonUp()
    {
        buttonUp = Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S);
    }
    public void ShowDetonatorButton()
    {
        ControllHelp.Instance.ShowDetonateButton();
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
