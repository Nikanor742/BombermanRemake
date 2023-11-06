
interface IInput
{
    bool ButtonLeft { get;}
    bool ButtonRight { get;}
    bool ButtonUp { get;}
    bool ButtonDown { get;}
    bool ButtonBomb { get;}
    bool ButtonDetonate { get;}

    void GetButtonLeft();
    void GetButtonRight();
    void GetButtonUp();
    void GetButtonDown();
    void GetButtonBomb();
    void GetButtonDetonate();

}
