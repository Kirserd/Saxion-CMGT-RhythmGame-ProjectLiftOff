using GXPEngine;
public class Unit : AnimationSprite
{
    public Stat HP { get; } // Health points
    public Stat MS { get; } // Movement speed
    public Unit(Vector2 position, Stat hp, Stat ms, string filename = "Empty", int cols = 1, int rows = 1) : base(filename, cols, rows)
    {
        SetXY(position.x, position.y);
        HP = hp;
        MS = ms;
    }
    private void Update() => Attack("ExampleCircleAttack");

    protected void Attack(string key, float atX = -1, float atY = -1) => (Game.main as Setup).LoadAttack(key, this, atX, atY);
    public override object Clone() => MemberwiseClone();
}
