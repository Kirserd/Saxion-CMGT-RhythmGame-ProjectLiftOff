using GXPEngine;
using System.Collections.Generic;

public class HorizontalList : Sprite
{

    private int _chosenElement = 0;
    public int ChosenElement
    {
        get => _chosenElement;
        protected set
        {
            if (IsActive && value >= 0 && value < _buttons.Count  ) _chosenElement = value;
            RefreshPositions();
            RefreshColors();
        }
    }
    public float DistanceBetweenElements { get; protected set; } = 256;
    public float ScrollSpeed { get; protected set; } = 0.2f;
    public bool IsActive { get; protected set; } = true;

    private List<Button> _buttons = new List<Button>();
    private float[] _destinations;
    private bool _isMoving = false;

    public HorizontalList() : base("Empty", false, false)
    {
        InputManager.OnLeftButtonPressed[0] += () => MoveInList(-1);
        InputManager.OnRightButtonPressed[0] += () => MoveInList(1);
        InputManager.OnSpaceButtonPressed[0] += ActivateChosen;
    }
    public void AddButton(Button button)
    {
        AddChild(button);
        _buttons.Add(button);
        button.SetXY(button.x, y);
        ChosenElement = _buttons.Count / 2;
    }
    public void AddButtons(Button[] buttons)
    {
        foreach (Button button in buttons)
            AddButton(button);
    }
    public void SetState(bool state)
    {
        foreach (Button button in _buttons)
            button.SetState(state);

        IsActive = state;
    }
    private void MoveInList(int step) => ChosenElement += !_isMoving ? step : 0;
    private void ActivateChosen() => _buttons[ChosenElement].Invoke();
    private void RefreshPositions()
    {
        _destinations = new float[_buttons.Count];
        for (int i = 0; i < _buttons.Count; i++)
        {
            float newDestination = (i - _chosenElement) * DistanceBetweenElements;
            if (_destinations[i] != newDestination)
            {
                _isMoving = true;
                _destinations[i] = newDestination;
            }
        }
    }
    private void RefreshColors()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (i == ChosenElement) _buttons[i].SetHovering();
            else _buttons[i].SetNormal();
        }
    }
    private void Update() => InterpolateToDestinations();
    private void InterpolateToDestinations()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (Mathf.Abs(_buttons[i].x - _destinations[i]) > 1)
                _buttons[i].x = Mathf.Lerp(_buttons[i].x, _destinations[i], ScrollSpeed);
            else
                _isMoving = false;
        }
        if (Input.GetKeyDown(Key.SPACE))
        {
            SoundManager.PlayOnce("Click");
        }
    }
    protected override void OnDestroy()
    {
        InputManager.OnLeftButtonPressed[0] -= () => MoveInList(-1);
        InputManager.OnRightButtonPressed[0] -= () => MoveInList(1);
        InputManager.OnSpaceButtonPressed[0] -= ActivateChosen;
        foreach (Button button in _buttons)
            button.SetState(false);
    }
}