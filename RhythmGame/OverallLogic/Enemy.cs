public class Enemy : Unit, IEventOwner
{
    private EventSequence _eventSequence;
    public Enemy(string sequencePath, Vector2 position, int  hp, Stat ms, string filename = "Empty", int cols = 1, int rows = 1) : base(position, hp, ms, filename, cols, rows) =>
    _eventSequence = new EventSequence(EventSequence.ReadFromFile(sequencePath), this);
    public void AddEventThere(EventData eventData) => _eventSequence.AddEventThere(eventData);
    public void AddEventAfter(EventData eventData) => _eventSequence.AddEventAfter(eventData);
    public override void OnEventsFinished()
    {
        Level.FinishLevel();
        LateDestroy();
    }
    protected override void OnDestroy()
    {
        _eventSequence.Destroy();
        base.OnDestroy();
    }
    private void FixedUpdate() =>  Animate(0.23f);
    
}
