using GXPEngine;
using System.Collections.Generic;

public class GUI : Sprite
{
    public delegate void OnPlayerAddedHandler();
    public static OnPlayerAddedHandler OnPlayerAdded;

    public string Name { get; }
    public static List<Button> Buttons { get; private set; } = new List<Button>();

    public GUI(string name) : base("Empty", false, false) => Name = name;

    public override void AddChild(GameObject child)
    {
        if (child is Button button)
            Buttons.Add(button);

        base.AddChild(child);
    }
    public override object Clone() => MemberwiseClone();
}
