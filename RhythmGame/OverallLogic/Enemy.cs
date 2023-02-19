public class Enemy : Unit, IEventOwner
{
    private EventSequence _eventSequence;
    public Enemy(string sequencePath, Vector2 position, Stat hp, Stat ms, string filename = "Empty", int cols = 1, int rows = 1) : base(position, hp, ms, filename, cols, rows)
    {
        _eventSequence = new EventSequence(EventSequence.ReadFromFile(sequencePath), this);
    }
    public void AddEventThere(EventData eventData) => _eventSequence.AddEventThere(eventData);
    public override void OnEventsFinished()
    {
        //TODO Finish level instead of Destroy().
        Destroy();
    }
}
