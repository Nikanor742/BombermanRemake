
interface IInput
{
    bool ButtonLeft { get;}
    bool ButtonRight { get;}
    bool ButtonUp { get;}
    bool ButtonDown { get;}
    bool ButtonBomb { get; set; }
    bool ButtonDetonate { get; set; }

    void GetButtonLeft();
    void GetButtonRight();
    void GetButtonUp();
    void GetButtonDown();
    void GetButtonBomb();
    void GetButtonDetonate();
    void ShowDetonatorButton();

}
