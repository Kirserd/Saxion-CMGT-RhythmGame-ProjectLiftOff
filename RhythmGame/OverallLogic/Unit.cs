using GXPEngine;
public class Unit : AnimationSprite
{
    protected int HP;
    protected int MaxHp;
    public void ChangeHP(int amount)
    {
        HP += amount;
        if (HP > MaxHp) HP = MaxHp;
        if (HP < 0) HP = 0;
    }
    public Stat MoveSpeed { get; } 
    public Unit(Vector2 position, int hp, Stat ms, string filename = "Empty", int cols = 1, int rows = 1) : base(filename, cols, rows)
    {
        SetXY(position.x, position.y);
        HP = hp;
        MaxHp = hp;
        MoveSpeed = ms;
    }
    protected bool ValidateUpdate()
    {
        if (LevelManager.CurrentLevel == null)
            return false;

        return true;
    }
    protected void Attack(string key) => (Game.main as Setup).LoadEvent(key, this as IEventOwner);
    public override object Clone() => MemberwiseClone();

    public virtual void OnEventsFinished(){}
}
