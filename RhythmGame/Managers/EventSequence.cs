using GXPEngine;
using System.Collections.Generic;
using System.IO;
using System;
public class EventSequence : GameObject
{
    private List<EventData> _events = new List<EventData>();
    private IEventOwner _owner;

    private float _timerFromStart;
    private float _counter = 0;
    private float _countTo = 1;
    public EventSequence(string[] names, float[] delays, IEventOwner owner) : base (false)
    {
        for (int i = 0; i < names.Length; i++)
            _events.Add(new EventData(names[i], delays[i]));

        _owner = owner;
        Game.main.AddChild(this);
    }
    public EventSequence(EventData[] events, IEventOwner owner) : base(false)
    {
        for (int i = 0; i < events.Length; i++)
            _events.Add(events[i]);

        _owner = owner;
        Game.main.AddChild(this);
    }
    public static EventData[] ReadFromFile(string filePath)
    {
        string fullPath = Settings.AssetsPath + "Sequences\\" + filePath + ".txt";
        List<string> names = new List<string>();
        List<float> delays = new List<float>();
        using (StreamReader reader = new StreamReader(fullPath))
        {
            string line;
            while((line = reader.ReadLine()) != null)
            {
                float.TryParse(line, System.Globalization.NumberStyles.AllowDecimalPoint, new System.Globalization.NumberFormatInfo(), out float delay);
                if(delay == 0)
                    names.Add(line);
                else
                {
                    delays.Add(delay);
                    Console.WriteLine(delay);
                }
            }
        }
        EventData[] events = new EventData[names.Count - 1];
        for (int i = 0; i < events.Length; i++)
        {
            events[i] = new EventData(names[i], delays[i]);
            events[i].ShowInConsole();
        }

        return events;
    }
    public void AddEventThere(EventData eventData) => _events.Insert(1, eventData);
    public void AddEventAfter(EventData eventData) => _events.Insert(2, eventData);

    private void Update()
    {
        if (_events.Count > 0)
        {
            _timerFromStart += Time.deltaTime / 1000f;
            _counter += Time.deltaTime / 1000f;
            if (_counter >= _countTo)
            {
                Console.WriteLine($"Event named ''{_events[0].Name}'' was invoked at [{_timerFromStart} sec.]");
                Console.WriteLine($"Waiting {_events[0].Delay} seconds at [{_timerFromStart} sec.]");

                (Game.main as Setup).LoadEvent(_events[0].Name, _owner);
                _counter = 0;
                _countTo = _events[0].Delay;
                _events.RemoveAt(0);
            }
        }
        else
        {
            _owner.OnEventsFinished();
            LateDestroy();
            Console.WriteLine($"Sequence finished in [{_timerFromStart} sec.]");
        }
    }
    public override object Clone() => MemberwiseClone(); 
} 
