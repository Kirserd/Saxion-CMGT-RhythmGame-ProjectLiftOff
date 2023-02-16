using GXPEngine;
public class Unit : AnimationSprite, IEventOwner
{
    public Stat HealthPoints { get; } 
    public Stat MoveSpeed { get; } 
    public Unit(Vector2 position, Stat hp, Stat ms, string filename = "Empty", int cols = 1, int rows = 1) : base(filename, cols, rows)
    {
        SetXY(position.x, position.y);
        HealthPoints = hp;
        MoveSpeed = ms;
    }
    protected bool ValidateUpdate()
    {
        if (LevelManager.CurrentLevel == null)
            return false;

        return true;
    }
    protected void Attack(string key, float atX = -1, float atY = -1) => (Game.main as Setup).LoadEvent(key, this, atX, atY);
    public override object Clone() => MemberwiseClone();

    public virtual void OnEventsFinished(){}
}
