public class Enemy : Unit, IEventOwner
{
    private EventSequence _eventSequence;
    public Enemy(string sequencePath, Vector2 position, int hp, Stat ms, string filename = "Empty", int cols = 1, int rows = 1) : base(position, hp, ms, filename, cols, rows)
    {
        _eventSequence = new EventSequence(EventSequence.ReadFromFile(sequencePath), this);
        SetScaleXY(scaleX / 6, scaleY / 2 );
    }
    public void AddEventThere(EventData eventData) => _eventSequence.AddEventThere(eventData);
    public override void OnEventsFinished()
    {
        //TODO Finish level instead of Destroy().
        Destroy();
    }
    void FixedUpdate()
    {
        SetCycle(0, 10);
        Animate(0.1f);
    }
}
