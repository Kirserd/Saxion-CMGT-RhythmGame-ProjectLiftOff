public interface IEventOwner
{
    float x { get; }
    float y { get; }
    void AddEventThere(EventData eventData);
    void AddEventAfter(EventData eventData);
    void OnEventsFinished();
}
