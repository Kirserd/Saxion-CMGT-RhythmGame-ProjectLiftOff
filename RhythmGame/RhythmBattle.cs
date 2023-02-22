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
    private const short BUTTON_WIDTH = 145;
    private const float DIST_MULT = 1f;
    private readonly float SAFE_NOTE_BREACH = Game.main.height/ 2;
    #endregion

    #region RythmGame
    public int Combo { get; private set; } = 0;

    private bool _started;
    private float _beatCount;

    public readonly List<Note>[] NoteColumns = new List<Note>[4] 
    {
        new List<Note>(), 
        new List<Note>(),
        new List<Note>(),
        new List<Note>()
    };

    private Sprite _noteParent;

    private AnimationSprite _1Button;
    private AnimationSprite _2Button;
    private AnimationSprite _3Button;
    private AnimationSprite _4Button;

    public float NoteDelay { get; private set; } = 1.2f;
    public float Speed { get; private set; } = 40f;

    public Sprite[] NoteHitTests { get; private set; }

    private byte _id;

    private float _timeToEnd;
    private float _timeCounter = 0;
    #endregion
    #endregion

    #region Constructors and Init
    public RhythmBattle(float speed, float noteDelay, float timeToEnd, byte id) : base(addCollider: false)
    {
        Speed = speed;
        NoteDelay = noteDelay;

        _id = id;
        _timeToEnd = timeToEnd;
        foreach (Player player in Level.Players)
            player.SetActive(false);

        SetupObjects();
        Game.main.AddChild(this);
        Level.Players[_id].rhythmBattle = this;
        _started = true;
    }
    private void SetupObjects()
    {
        if (Level.TwoPlayers)
        {
            if (_id == 0)
            {
                #region Visual and GUI
                Sprite background = new Sprite("Battle_base");
                background.SetOrigin(background.width / 2, 0);
                background.SetScaleXY(background.scaleX, background.scaleY);
                background.SetXY((int)Game.main.width / 2, -25);

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
                _1Button = new AnimationSprite("Rhythm_Button", 2, 1);
                _2Button = new AnimationSprite("Rhythm_Button", 2, 1);
                _3Button = new AnimationSprite("Rhythm_Button", 2, 1);
                _4Button = new AnimationSprite("Rhythm_Button", 2, 1);

                Sprite[] buttons = new Sprite[4] { _1Button, _2Button, _3Button, _4Button };
                for (int i = -2; i < 2; i++)
                {
                    Sprite button = buttons[i + 2];
                    button.SetOrigin(button.width / 4, button.height / 2);
                    button.SetXY(Game.main.width / 2 + (BUTTON_WIDTH * i * DIST_MULT), Game.main.height);
                    button.SetScaleXY(button.scaleX / 2, button.scaleY);
                }

                AddChild(_1Button);
                AddChild(_2Button);
                AddChild(_3Button);
                AddChild(_4Button);
                #endregion
            }
            if (_id == 1)
            {
                #region Visual and GUI
                Sprite background = new Sprite("Battle_base");
                background.SetOrigin(background.width / 2, 0);
                background.SetScaleXY(background.scaleX, background.scaleY);
                background.SetXY((int)Game.main.width / 2, -25);

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
                _1Button = new AnimationSprite("Rhythm_Button", 2, 1);
                _2Button = new AnimationSprite("Rhythm_Button", 2, 1);
                _3Button = new AnimationSprite("Rhythm_Button", 2, 1);
                _4Button = new AnimationSprite("Rhythm_Button", 2, 1);

                Sprite[] buttons = new Sprite[4] { _1Button, _2Button, _3Button, _4Button };
                for (int i = -2; i < 2; i++)
                {
                    Sprite button = buttons[i + 2];
                    button.SetOrigin(button.width / 4, button.height / 2);
                    button.SetXY(Game.main.width / 2 + (BUTTON_WIDTH * i * DIST_MULT), Game.main.height);
                    button.SetScaleXY(button.scaleX / 2, button.scaleY);
                }

                AddChild(_1Button);
                AddChild(_2Button);
                AddChild(_3Button);
                AddChild(_4Button);
                #endregion
            }
        }
        else
        {
            #region Visual and GUI
            Sprite background = new Sprite("Battle_base");
            background.SetOrigin(background.width / 2, 0);
            background.SetScaleXY(background.scaleX, background.scaleY);
            background.SetXY((int)Game.main.width / 2, -25);

            AddChild(background);

            _noteParent = new Sprite("Empty");
            AddChild(_noteParent);
            #endregion

            #region Collision Hit-Tests
            Sprite firstHitTest = new Sprite("Empty", true);
            Sprite secondHitTest = new Sprite("Empty", true);
            Sprite thirdHitTest = new Sprite("Empty", true);
            NoteHitTests = new Sprite[3] { firstHitTest, secondHitTest, thirdHitTest };

            int yPosCounter = 148;
            for (int i = 0; i < 3; i++)
            {
                Sprite hitTest = NoteHitTests[i];
                hitTest.SetScaleXY(Game.main.height, 4f);
                hitTest.SetXY(0, Game.main.height - yPosCounter);
                yPosCounter -= 32;
            }

            AddChild(firstHitTest);
            AddChild(secondHitTest);
            AddChild(thirdHitTest);
            #endregion

            #region Buttons
            _1Button = new AnimationSprite("1Rhythm_Button", 2, 1);
            _2Button = new AnimationSprite("2Rhythm_Button", 2, 1);
            _3Button = new AnimationSprite("3Rhythm_Button", 2, 1);
            _4Button = new AnimationSprite("4Rhythm_Button", 2, 1);

            Sprite[] buttons = new Sprite[4] { _1Button, _2Button, _3Button, _4Button };
            for (int i = -2; i < 2; i++)
            {
                Sprite button = buttons[i + 2];
                button.SetOrigin(button.width / 4, button.height / 2);
                button.SetXY(Game.main.width / 2 + (BUTTON_WIDTH * i * DIST_MULT), Game.main.height);
                button.SetScaleXY(button.scaleX / 2, button.scaleY);
            }

            AddChild(_1Button);
            AddChild(_2Button);
            AddChild(_3Button);
            AddChild(_4Button);
            #endregion
        }
    }
    #endregion

    #region Controllers
    private void Update()
    {
        if (!_started)
            return;

        ListenFinish();
        ContinueBeats();
        GetInput();
    }
    private void ContinueBeats()
    {
        _beatCount += Time.deltaTime / 1000f;
        if (_beatCount >= NoteDelay)
        {
            SpawnNote();
            _beatCount = 0;
        }
    }
    private void ListenFinish()
    {
        _timeCounter += Time.deltaTime / 1000f;
        if (_timeCounter >= _timeToEnd)
            FinishBattle();
    }
    private void GetInput()
    {
        if (_id == 0)
        {
            if (Input.GetKeyDown(Key.A))
            {
                _1Button.currentFrame = 1;
                TryHitNote(0);
            }
            if (Input.GetKeyUp(Key.A))
                _1Button.currentFrame = 0;

            if (Input.GetKeyDown(Key.W))
            {
                _2Button.currentFrame = 1;
                TryHitNote(1);
            }
            if (Input.GetKeyUp(Key.W))
                _2Button.currentFrame = 0;

            if (Input.GetKeyDown(Key.S))
            {
                _3Button.currentFrame = 1;
                TryHitNote(2);
            }
            if (Input.GetKeyUp(Key.S))
                _1Button.currentFrame = 0;

            if (Input.GetKeyDown(Key.D))
            {
                _4Button.currentFrame = 1;
                TryHitNote(3);
            }
            if (Input.GetKeyUp(Key.D))
                _4Button.currentFrame = 0;
        }
    }
    private void SpawnNote()
    {
        byte noteColumn = (byte)new Random().Next(0,4);
        Note note = new Note(noteColumn, this, Speed);
        note.SetOrigin(note.width / 2, note.height / 2);
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
    public void DealDamage() => Level.Players[_id].ChangeHP(Level.Players[_id].HP > 0? -1 : 0);

    public void FinishBattle()
    {
        Level.Players[_id].rhythmBattle = null;
        Level.Players[_id].SetActive(true);
        _started = false;
        DestroyChildren();
        LateDestroy();
    }
    #endregion
    public override object Clone() => MemberwiseClone();
}
