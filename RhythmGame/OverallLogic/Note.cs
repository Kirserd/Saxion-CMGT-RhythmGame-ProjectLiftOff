using GXPEngine;
/// <summary>
/// This class represents Note logic in Battles.
/// </summary>
public class Note : Sprite
{
    #region Fields
    public readonly byte Position;
    public readonly float Speed;
    public readonly RhythmBattle RhythmBattle;

    private bool _isDying = false;
    private byte _deathCounter = 0;

    private const float TIME_TO_DEATH = 5f;
    #endregion

    #region Constructor
    public Note(byte position, RhythmBattle rhythmBattle, float speed = 50f) : base("Note", true)
    {
        RhythmBattle = rhythmBattle;
        Position = position;
        Speed = speed;
    }
    #endregion

    #region Controllers
    private void Update() 
    {
        if (_isDying)
            return;

        Fall();
        CheckIfOutOfBoundaries();
    }
    private void Fall() => SetXY(x, y + Speed);
    private void CheckIfOutOfBoundaries()
    {
        if (y > Game.main.height - 27)
        {
            RhythmBattle.DealDamage();
            if (RhythmBattle.NoteColumns[Position].Count > 0)
                RhythmBattle.NoteColumns[Position].RemoveAt(0);
            color = 0x770000;
            _isDying = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_isDying)
            return;
        CountToDeath();
    }
    private void CountToDeath()
    {
        _deathCounter++;
        if (_deathCounter >= TIME_TO_DEATH)
        {
            Destroy();
            SoundManager.PlayOnce("TakeDamage");
        }
    }

    public byte ValidateHit(RhythmBattle battle) 
    {
        byte HitQuality = 0;
        foreach (Sprite hitTest in battle.NoteHitTests)
            if (HitTest(hitTest)) 
                HitQuality++;

        _isDying = true;
        return HitQuality;
    }
    #endregion
}

