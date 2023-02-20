using GXPEngine;
using System.Collections.Generic;
public class Level : Sprite
{
    public delegate void OnPlayerAddedHandler();
    public static OnPlayerAddedHandler OnPlayerAdded;

    public string Name { get; }
    public const float MAX_RADIUS = 600;
    public static List<Player> Players{ get; private set; } = new List<Player>();
    public static List<Unit> Units { get; private set; } = new List<Unit>();

    public Level(string name) : base("Empty", false, false) => Name = name;

    public override void AddChild(GameObject child)
    {
        if(child is Unit unit)
            Units.Add(unit);
        
        if (child is Player player)
        {
            Players.Add(player);
            base.AddChild(child);

            if (OnPlayerAdded != null)
                OnPlayerAdded.Invoke();

            return;
        }
        base.AddChild(child);
    }
    public override object Clone() => MemberwiseClone();
}
