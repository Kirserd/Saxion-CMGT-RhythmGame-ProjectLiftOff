public interface IEventOwner
{
    float x { get; }
    float y { get; }
    void AddEventThere(EventData eventData);
    void OnEventsFinished();
}
