using GXPEngine;

public static class InputManager
{
    public delegate void OnUpButtonPressedHandler();
    public static OnUpButtonPressedHandler[] OnUpButtonPressed = new OnUpButtonPressedHandler[2];

    public delegate void OnDownButtonPressedHandler();
    public static OnDownButtonPressedHandler[] OnDownButtonPressed = new OnDownButtonPressedHandler[2];

    public delegate void OnRightButtonPressedHandler();
    public static OnRightButtonPressedHandler[] OnRightButtonPressed = new OnRightButtonPressedHandler[2];

    public delegate void OnLeftButtonPressedHandler();
    public static OnLeftButtonPressedHandler[] OnLeftButtonPressed = new OnLeftButtonPressedHandler[2];

    public delegate void OnSpaceButtonPressedHandler();
    public static OnSpaceButtonPressedHandler[] OnSpaceButtonPressed = new OnSpaceButtonPressedHandler[2];

    public static void ListenToInput()
    {
        #region 1st Player
        if (Input.GetKey(Key.S) && OnUpButtonPressed[0] != null) OnUpButtonPressed[0].Invoke();
        if (Input.GetKey(Key.W) && OnDownButtonPressed[0] != null) OnDownButtonPressed[0].Invoke();
        if (Input.GetKey(Key.A) && OnLeftButtonPressed[0] != null) OnLeftButtonPressed[0].Invoke();
        if (Input.GetKey(Key.D) && OnRightButtonPressed[0] != null) OnRightButtonPressed[0].Invoke();
        if (Input.GetKeyDown(Key.SPACE) && OnSpaceButtonPressed[0] != null) OnSpaceButtonPressed[0].Invoke();
        #endregion

        #region 2nd Player
        if (Input.GetKey(Key.DOWN) && OnUpButtonPressed[1] != null) OnUpButtonPressed[1].Invoke();
        if (Input.GetKey(Key.UP) && OnDownButtonPressed[1] != null) OnDownButtonPressed[1].Invoke();
        if (Input.GetKey(Key.LEFT) && OnLeftButtonPressed[1] != null) OnLeftButtonPressed[1].Invoke();
        if (Input.GetKey(Key.RIGHT) && OnRightButtonPressed[1] != null) OnRightButtonPressed[1].Invoke();
        if (Input.GetKeyDown(Key.NUMPAD_INSERT) && OnSpaceButtonPressed[1] != null) OnSpaceButtonPressed[1].Invoke();
        #endregion
    }

}