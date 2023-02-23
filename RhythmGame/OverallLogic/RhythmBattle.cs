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
    private const short NOTE_WIDTH = 114;
    private const short BUTTON_WIDTH = 114;
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
        if (Level.Players.Count <= id || Level.Players[id] == null || Level.Players[id].IsDead) 
        { 
            Destroy();
            return;
        }
        Speed = speed;
        NoteDelay = noteDelay;

        _id = id;
        _timeToEnd = timeToEnd;
        Level.Players[id].SetActive(false);

        SetupObjects();
        Game.main.AddChild(this);
        Level.Players[_id].rhythmBattle = this;
        _started = true;
    }
    private void SetupObjects()
    {
        if (Level.TwoPlayers)
        {
            #region Visual and GUI
                Sprite background = new Sprite("Battle_base");
                background.SetOrigin(background.width / 2 + 18, 0);
                background.SetScaleXY(background.scaleX, background.scaleY);
                background.SetXY(Game.main.width / 2 + (_id * 2 - 1) * background.width * 2 / 3, -70);

                _noteParent = new Sprite("Empty");
                _noteParent.SetXY(x + (_id * 2 - 1) * background.width * 2 / 3, y);
                #endregion

            #region Collision Hit-Tests
                Sprite firstHitTest = new Sprite("Empty", true);
                Sprite secondHitTest = new Sprite("Empty", true);
                Sprite thirdHitTest = new Sprite("Empty", true);
                NoteHitTests = new Sprite[3] { firstHitTest, secondHitTest, thirdHitTest };

                int yPosCounter = 250;
                for (int i = 0; i < 3; i++)
                {
                    Sprite hitTest = NoteHitTests[i];
                    hitTest.SetScaleXY(Game.main.height, 4f);
                    hitTest.SetXY(0, Game.main.height - yPosCounter);
                    yPosCounter -= 32;
                }
                #endregion

            #region Buttons
                _1Button = new AnimationSprite("1Rhythm_Button", 2, 1);
                _2Button = new AnimationSprite("3Rhythm_Button", 2, 1);
                _3Button = new AnimationSprite("2Rhythm_Button", 2, 1);
                _4Button = new AnimationSprite("4Rhythm_Button", 2, 1);

                Sprite[] buttons = new Sprite[4] { _1Button, _2Button, _3Button, _4Button };
                for (int i = -2; i < 2; i++)
                {
                    Sprite button = buttons[i + 2];
                    button.SetOrigin(button.width / 4, button.height / 2);
                    button.SetXY(Game.main.width / 2 + (_id * 2 - 1) * background.width * 2 / 3 + (BUTTON_WIDTH * i * DIST_MULT), Game.main.height - 125);
                    button.SetScaleXY(button.scaleX / 2, button.scaleY);
                }


                ValueDisplayer<int> combo = new ValueDisplayer<int>(() => Combo, 220, 720);
                EasyDraw comboText = new EasyDraw(220, 720);
                combo.SetXY(-325 + (_id * 2 - 1) * background.width * 2 / 3, 340);
                comboText.SetXY(-325 + (_id * 2 - 1) * background.width * 2 / 3, 520);
                combo.rotation = -90;
                comboText.rotation = -90;
                combo.TextSize(36);
                comboText.TextSize(36);
                comboText.Text("Combo: ");
                combo.SetColor(0, 1, 0);
                comboText.SetColor(0, 1, 0);

                AddChildren(new GameObject[]
                {
                background,
                _noteParent,
                firstHitTest,
                secondHitTest,
                thirdHitTest,
                _1Button,
                _2Button,
                _3Button,
                _4Button,
                comboText,
                combo
                });
                #endregion
        }
        else
        {
            #region Visual and GUI
            Sprite background = new Sprite("Battle_base");
            background.SetOrigin(background.width / 2 + 18, 0);
            background.SetScaleXY(background.scaleX, background.scaleY);
            background.SetXY(Game.main.width / 2, -25);

            _noteParent = new Sprite("Empty");
            #endregion

            #region Collision Hit-Tests
            Sprite firstHitTest = new Sprite("Empty", true);
            Sprite secondHitTest = new Sprite("Empty", true);
            Sprite thirdHitTest = new Sprite("Empty", true);
            NoteHitTests = new Sprite[3] { firstHitTest, secondHitTest, thirdHitTest };

            int yPosCounter = 192;
            for (int i = 0; i < 3; i++)
            {
                Sprite hitTest = NoteHitTests[i];
                hitTest.SetScaleXY(Game.main.height, 4f);
                hitTest.SetXY(0, Game.main.height - yPosCounter);
                yPosCounter -= 32;
            }
            #endregion

            #region Buttons
            _1Button = new AnimationSprite("1Rhythm_Button", 2, 1);
            _2Button = new AnimationSprite("3Rhythm_Button", 2, 1);
            _3Button = new AnimationSprite("2Rhythm_Button", 2, 1);
            _4Button = new AnimationSprite("4Rhythm_Button", 2, 1);

            Sprite[] buttons = new Sprite[4] { _1Button, _2Button, _3Button, _4Button };
            for (int i = -2; i < 2; i++)
            {
                Sprite button = buttons[i + 2];
                button.SetOrigin(button.width / 4, button.height / 2);
                button.SetXY(Game.main.width / 2 + (BUTTON_WIDTH * i * DIST_MULT), Game.main.height - 60);
                button.SetScaleXY(button.scaleX / 2, button.scaleY);
            }


            ValueDisplayer<int> combo = new ValueDisplayer<int>(() => Combo, 220, 720);
            EasyDraw comboText = new EasyDraw(220, 720);
            combo.SetXY(-325, 340);
            comboText.SetXY(-325, 520);
            combo.rotation = -90;
            comboText.rotation = -90;
            combo.TextSize(36);
            comboText.TextSize(36);
            comboText.Text("Combo: ");
            combo.SetColor(0, 1, 0);
            comboText.SetColor(0, 1, 0);

            AddChildren(new GameObject[]
            {
                background,
                _noteParent,
                firstHitTest,
                secondHitTest,
                thirdHitTest,
                _1Button,
                _2Button,
                _3Button,
                _4Button,
                comboText,
                combo
            }) ;
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
                _3Button.currentFrame = 0;

            if (Input.GetKeyDown(Key.D))
            {
                _4Button.currentFrame = 1;
                TryHitNote(3);
            }
            if (Input.GetKeyUp(Key.D))
                _4Button.currentFrame = 0;
        }
        else
        {
            if (Input.GetKeyDown(Key.LEFT))
            {
                _1Button.currentFrame = 1;
                TryHitNote(0);
            }
            if (Input.GetKeyUp(Key.LEFT))
                _1Button.currentFrame = 0;

            if (Input.GetKeyDown(Key.UP))
            {
                _2Button.currentFrame = 1;
                TryHitNote(1);
            }
            if (Input.GetKeyUp(Key.UP))
                _2Button.currentFrame = 0;

            if (Input.GetKeyDown(Key.DOWN))
            {
                _3Button.currentFrame = 1;
                TryHitNote(2);
            }
            if (Input.GetKeyUp(Key.DOWN))
                _3Button.currentFrame = 0;

            if (Input.GetKeyDown(Key.RIGHT))
            {
                _4Button.currentFrame = 1;
                TryHitNote(3);
            }
            if (Input.GetKeyUp(Key.RIGHT))
                _4Button.currentFrame = 0;
        }
    }
    private void SpawnNote()
    {
        byte noteColumn = (byte)new Random().Next(0,4);
        Note note = new Note(noteColumn, this, Speed);
        note.SetOrigin(note.width / 2, note.height / 2);
        note.SetXY(Game.main.width / 2 + (NOTE_WIDTH * (noteColumn - 2) * DIST_MULT) + 38, - 60);
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

        Level.AddScore(result * 4 + Combo, _id);
    }
    public void DealDamage()
    {
        if (Level.Players.Count > _id)
            Level.Players[_id].ChangeHP(Level.Players[_id].HP > 0 ? -1 : 0);
        else
        {
            _started = false;
            visible = false;
        }
    }

    public void FinishBattle()
    {
        if (Level.Players.Count > _id && Level.Players[_id] != null)
        {
            Level.Players[_id].rhythmBattle = null;
            Level.Players[_id].SetActive(true);
            System.Console.WriteLine(_id + " is true");
        }
        _started = false;
        DestroyChildren();
        LateDestroy();
    }
    #endregion
    public override object Clone() => MemberwiseClone();
}
