using GXPEngine;
using System.Collections.Generic;
using System.IO;

public class ScoreList : Sprite
{
    public float DistanceXBetweenElements { get; protected set; } = 232;
    public float DistanceYBetweenElements { get; protected set; } = 48;

    private List<ConstValueDisplayer<string>> _scores = new List<ConstValueDisplayer<string>>();
    private float[] _destinationsY;
    private float[] _destinationsX;

    public ScoreList() : base("Empty", false, false)
    {
        ReadFromFile();
        RefreshPositions();
        InputManager.OnAnyButtonPressed += Return;
    }
    private void Return()
    {
        InputManager.OnAnyButtonPressed -= Return;
        (Game.main as Setup).LoadGUI("Menu");
    }
    private void ReadFromFile()
    {
        string fullPath = Settings.AssetsPath + "Scores.txt";
        using (StreamReader reader = new StreamReader(fullPath))
        {
            string line;
            int counter = 0;

            while ((line = reader.ReadLine()) != null)
            {
                counter++;
                AddValue(new ConstValueDisplayer<string>($"{counter}) " + line, 480, 64));
            }
            void AddValue(ConstValueDisplayer<string> value)
            {
                value.SetColor(0.5f, 1, 0.5f);
                value.SetOrigin(value.width / 2, value.height / 2);
                _scores.Add(value);
                AddChild(value);
            }
        }
    }
    private void RefreshPositions()
    {
        _destinationsY = new float[_scores.Count];
        _destinationsX = new float[_scores.Count];
        int xCounter = -1;
        int yCounter = -1;
        for (int i = 0; i < _scores.Count; i++)
        {
            if (i % 4 == 0)
            {
                xCounter++;
                yCounter = 1;
            }

            yCounter++;

            float newDestinationY = yCounter * DistanceYBetweenElements;
            float newDestinationX = xCounter * DistanceXBetweenElements;
            if (_destinationsY[i] != newDestinationY)
            {
                _destinationsY[i] = newDestinationY;
            }
            if (_destinationsX[i] != newDestinationX)
            {
                _destinationsX[i] = newDestinationX;
            }
        }
    }
    private void Update() => InterpolateToDestinations();
    private void InterpolateToDestinations()
    {
        for (int i = 0; i < _scores.Count; i++)
        {
            if (Mathf.Abs(_scores[i].y - _destinationsY[i]) > 1)
                _scores[i].y = Mathf.Lerp(_scores[i].y, _destinationsY[i], 0.25f);
            if (Mathf.Abs(_scores[i].x - _destinationsX[i]) > 1)
                _scores[i].x = Mathf.Lerp(_scores[i].x, _destinationsX[i], 0.25f);
        }
    }
}