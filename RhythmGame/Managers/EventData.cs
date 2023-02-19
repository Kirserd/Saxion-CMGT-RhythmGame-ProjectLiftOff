public struct EventData
{
    public string Name { get; }
    public float Delay { get; }
    public EventData(string name, float delay)
    {
        Name = name;
        Delay = delay;
    }
    public void ShowInConsole() => System.Console.WriteLine($"[name: {Name}][delay: {Delay}]");
}
