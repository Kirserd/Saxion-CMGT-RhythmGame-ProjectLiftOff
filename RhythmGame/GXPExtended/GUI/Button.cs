using GXPEngine;
using System;

public enum ButtonState
{
    Normal,
    Hovering,
    Clicked
}

/// <summary>
/// This class represents GUI Button logic,
/// listens to mouse hovering and clicking
/// </summary>
public class Button : Sprite
{
    #region Fields and properties
    private Action _action;
    public const int NormalColor = 0xFFFFFF;
    public const int HoveringColor = 0xA5A5A5;
    public const int ClickedColor = 0x757575;
    private int[] _colors = new int[3] { NormalColor, HoveringColor, ClickedColor };
    private ButtonState _state = ButtonState.Normal;

    private float _clickFadeCounter = 0f;
    private const float CLICK_FADEOUT_TIME = 120f;
    #endregion

    #region Constructor
    public Button(string filename, Action action = null) : base(filename) => SetAction(action);
    public void SetAction(Action action) => _action = action;
    #endregion

    #region Controllers
    private void Update()
    {
        if (parent.parent.visible)
        {
            RefreshColor();
            ListenMouseInputAndManageStates();
        }
    }
    private void RefreshColor() => color = (uint) _colors[(int)_state];
    private void ListenMouseInputAndManageStates()
    {
        if (Input.GetMouseButtonDown(0) && _state == ButtonState.Hovering)
        {
            _state = ButtonState.Clicked;
            _clickFadeCounter = CLICK_FADEOUT_TIME;
            if (_action != null)
            {
                SoundManager.PlayOnce("MenuClick");
                _action.Invoke();
            }
        }
        if (_state != ButtonState.Clicked)
        {
            if (HitTestPoint(Input.mouseX, Input.mouseY))
            {
                if (_state == ButtonState.Normal)
                {
                    _state = ButtonState.Hovering;
                    SoundManager.PlayOnce("MenuSelect");
                }
            }
            else if (_state == ButtonState.Hovering)
                _state = ButtonState.Normal;
        }
        else
        {
            _clickFadeCounter -= Time.deltaTime;
            if (_clickFadeCounter <= 0)
                _state = ButtonState.Hovering;
        }
    }
    #endregion
}