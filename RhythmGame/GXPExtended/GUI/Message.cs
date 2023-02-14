using GXPEngine;
using System.Drawing;
using System;

public class Message : Sprite
{
    private const float CHAR_DELAY = 0.2f;
    private float _timeCounter = 0;

    private readonly string[] _lines;
    private byte _lineCounter = 0;
    private string _currentText;
    private byte _charCounter = 0;
    private readonly EasyDraw _text;

    private bool _waitForContinue = false;
    private Action _onFinishedAction;

    public Message(string[] lines, Action onFinishedAction = null) : base("MessageBox")
    {
        Game.main.AddChild(this);

        _lines = lines;
        _onFinishedAction = onFinishedAction;
        _text = new EasyDraw(width, height);
        _text.TextAlign(CenterMode.Min, CenterMode.Min);
        _text.TextSize(7f);
        _text.Fill(Color.FromArgb(255, 200, 160, 140));
        _text.SetOrigin(width / 2, 0);
        AddChild(_text);

        SetOrigin(width / 2, 0);
        SetXY(Game.main.width / 2, Game.main.height * 3 / 4);
    }

    private void Update()
    {
        CheckInput();
        TryWritingText();
    }
    private void TryWritingText()
    {
        _timeCounter += Time.deltaTime;
        if (_timeCounter >= CHAR_DELAY)
        {
            _timeCounter = CHAR_DELAY;
            if (_charCounter < _lines[_lineCounter].Length)
            {
                _charCounter++;
                _currentText = _lines[_lineCounter].Substring(0, _charCounter);
                _text.Text(_currentText, 54, 7, clear: true);
                SoundManager.PlayOnce("TypingSound");
            }
            else
                _waitForContinue = true;
        }
    }
    private void CheckInput()
    {
        if (_waitForContinue)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown('Z'))
            {
                if (_lineCounter + 1 < _lines.Length)
                {
                    _lineCounter++;
                    _charCounter = 0;
                }
                else
                    CloseMessage();
                _waitForContinue = false;
            }
        }
    }
    private void CloseMessage()
    {
        if (_onFinishedAction != null)
            _onFinishedAction.Invoke();

        LateDestroy();
    }
}
