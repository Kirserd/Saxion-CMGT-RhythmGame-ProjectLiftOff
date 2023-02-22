using GXPEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// This class represents rhythm battles.
/// </summary>
public class RhythmBattle : GameObject
{
    #region Fields and properties
    
    #region Constants
    private const short NOTE_WIDTH = 145;
    private const short BUTTON_WIDTH = 60;
    private const float DIST_MULT = 1f;
    private readonly float SAFE_NOTE_BREACH = Game.main.height/ 2;
    #endregion

    #region RythmGame
    public int Combo { get; private set; } = 0;

    private bool _started;
    private short _beatCount;

    public readonly List<Note>[] NoteColumns = new List<Note>[4] 
    {
        new List<Note>(), 
        new List<Note>(),
        new List<Note>(),
        new List<Note>()
    };

    private Sprite _noteParent;

    private Sprite _zButton;
    private Sprite _xButton;
    private Sprite _cButton;
    private Sprite _vButton;

    public short NoteDelay { get; private set; } = 1;
    public float Speed { get; private set; } = 4f;

    public Sprite[] NoteHitTests { get; private set; }

    private byte _id;

    private float _timeToEnd;
    private float _timeCounter = 0;
    #endregion
    #endregion

    #region Constructors and Init
    public RhythmBattle(float timeToEnd, byte id) : base(addCollider: false)
    {
        _id = id;
        _timeToEnd = timeToEnd;
        foreach (Player player in Level.Players)
        {
            player.SetDirectionActive(0, false);
            player.SetDirectionActive(1, false);
        }

        SetupObjects();
        _started = true;
    }
    private void SetupObjects()
    {
        #region Visual and GUI
        Sprite background = new Sprite("Battle_base");
        background.SetOrigin(background.width / 2, 0);
        background.SetScaleXY(background.scaleX, background.scaleY);
        background.SetXY((int)Game.main.width / 2, - 25);

        AddChild(background);

        _noteParent = new Sprite("Empty");
        AddChild(_noteParent);
        #endregion

        #region Collision Hit-Tests
        Sprite firstHitTest = new Sprite("Empty", true);
        Sprite secondHitTest = new Sprite("Empty", true);
        Sprite thirdHitTest = new Sprite("Empty", true);
        NoteHitTests = new Sprite[3] { firstHitTest, secondHitTest, thirdHitTest };

        int yPosCounter = 47;
        for (int i = 0; i < 3; i++)
        {
            Sprite hitTest = NoteHitTests[i];
            hitTest.SetScaleXY(Game.main.height, 1.2f);
            hitTest.SetXY(0, Game.main.height - yPosCounter);
            yPosCounter -= 7;
        }

        AddChild(firstHitTest);
        AddChild(secondHitTest);
        AddChild(thirdHitTest);
        #endregion

        #region Buttons
        _zButton = new Sprite("Z_Button");
        _xButton = new Sprite("X_Button");        
        _cButton = new Sprite("C_Button");
        _vButton = new Sprite("V_Button");

        Sprite[] buttons = new Sprite[4] { _zButton, _xButton, _cButton, _vButton };
        for (int i = -2; i < 2; i++)
        {
            Sprite button = buttons[i + 2];
            button.SetOrigin(0, button.height);
            button.SetXY(Game.main.width / 2 + (BUTTON_WIDTH * i * DIST_MULT), Game.main.height);
            button.SetScaleXY(button.scaleX, button.scaleY);
        }

        AddChild(_zButton);
        AddChild(_xButton);
        AddChild(_cButton);
        AddChild(_vButton);
        #endregion
    }
    #endregion

    #region Controllers
    private void Update()
    {
        if (!_started)
            return;

        _timeCounter += Time.deltaTime;

        ListenFinish();
        ContinueBeats();
        GetInput();
    }
    private void ContinueBeats()
    {
        _beatCount++;
        if (_beatCount >= NoteDelay)
        {
            SpawnNote();
            _beatCount = 0;
        }
    }
    private void ListenFinish()
    {
        if (_timeCounter >= _timeToEnd)
            FinishBattle(true);
    }
    private void GetInput()
    {
        if (Input.GetKeyDown(Key.Z))
        {
            _zButton.color = 0x55FF55;
            TryHitNote(0);
        }
        if (Input.GetKeyUp(Key.Z))
            _zButton.color = 0xFFFFFF;

        if (Input.GetKeyDown(Key.X))
        {
            _xButton.color = 0x55FF55;
            TryHitNote(1);
        }
        if (Input.GetKeyUp(Key.X))
            _xButton.color = 0xFFFFFF;

        if (Input.GetKeyDown(Key.C))
        {
            _cButton.color = 0x55FF55;
            TryHitNote(2);
        }
        if (Input.GetKeyUp(Key.C))
            _cButton.color = 0xFFFFFF;

        if (Input.GetKeyDown(Key.V))
        {
            _vButton.color = 0x55FF55;
            TryHitNote(3);
        }
        if (Input.GetKeyUp(Key.V))
            _vButton.color = 0xFFFFFF;
    }
    private void SpawnNote()
    {
        byte noteColumn = (byte)new Random().Next(0,4);
        Note note = new Note(noteColumn, this, Speed);
        note.SetXY(Game.main.width / 2 + (NOTE_WIDTH * (noteColumn - 2) * DIST_MULT) + 3, - 60);
        NoteColumns[noteColumn].Add(note);
        _noteParent.AddChild(note);
    }
    private void TryHitNote(byte column)
    {
        if (NoteColumns[column].Count == 0)
            return;

        Note nearestNote = NoteColumns[column][0];
        if (nearestNote.y < SAFE_NOTE_BREACH)
            return;

        byte result = nearestNote.ValidateHit(this);

        if (result == 0)
        {
            SoundManager.PlayOnce("TakeDamage", true);
            Combo = 0;
            DealDamage();
            nearestNote.color = 0x770000;
        }
        else if (result == 1)
        {
            SoundManager.PlayOnce("Miss", true);
            Combo = 0;
            nearestNote.color = 0xFF5500;
        }
        else if (result == 2)
        {
            SoundManager.PlayOnce("Attack", true);
            Combo++;
            nearestNote.color = 0x22FF22;
        }
        else if (result == 3)
        {
            SoundManager.PlayOnce("FullAttack", true);
            Combo += 2;
            nearestNote.color = 0x22FFFF;
        }
        NoteColumns[column].RemoveAt(0);

        Level.AddScore(result * 4, _id);
    }
    public void DealDamage() => Level.Players[_id].ChangeHP(-1);

    private void FinishBattle(bool win)
    {
        foreach (Player player in Level.Players)
        {
            player.SetDirectionActive(0, true);
            player.SetDirectionActive(1, true);
        }
        _started = false;
    }
    #endregion
    public override object Clone() => MemberwiseClone();
}
