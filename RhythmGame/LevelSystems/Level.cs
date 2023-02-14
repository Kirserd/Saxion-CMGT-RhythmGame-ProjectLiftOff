using GXPEngine;
using System.Collections.Generic;
public class Level : Sprite
{
    public delegate void OnPlayerAddedHandler();
    public OnPlayerAddedHandler OnPlayerAdded;

    public string[] PackedArgs { get; }
    public List<Player> Players{ get; private set; } = new List<Player>();

    public Level(string name, Vector2 spawn) : base("Empty", false, false)
    {
        PackedArgs = new string[]
        {
            name,
            spawn.x.ToString(),
            spawn.y.ToString()
        };
    }

    public override void AddChild(GameObject child)
    {
        if (child is Player player)
        {
            Players.Add(player);

            if (OnPlayerAdded != null)
                OnPlayerAdded.Invoke();
        }
        base.AddChild(child);
    }
    public override object Clone() => MemberwiseClone();
}