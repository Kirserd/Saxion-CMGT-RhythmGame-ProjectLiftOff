using GXPEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;
public class Level : Sprite
{
    public delegate void OnPlayerAddedHandler();
    public static OnPlayerAddedHandler OnPlayerAdded;

    public string Name { get; }
    public const float MAX_RADIUS = 600;
    public static bool TwoPlayers = false;
    public static int[] Score { get; } = new int[2];
    public static List<Player> Players{ get; private set; } = new List<Player>();
    public static List<Unit> Units { get; private set; } = new List<Unit>();

    public Level(string name) : base("Empty", false, false) => Name = name;

    public static void CheckPlayers()
    {
        foreach (Player player in Players)
        {
            if(!player.IsDead)
                return;
        }
        FinishLevel();
    }
    public static void FinishLevel()
    {
        SaveScore(Score[0]);
        if (TwoPlayers)
            SaveScore(Score[1]);
        LevelManager.LateLoadLevel("ScoreLevel", TwoPlayers);

        void SaveScore(int newScore)
        {
            List<int> scores = new List<int>
            {
                newScore
            };
            using (StreamReader reader = new StreamReader(Settings.AssetsPath + "Scores.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int.TryParse(line, out int score);
                    scores.Add(score);
                }
            }
            scores.Sort();
            using (StreamWriter writer = new StreamWriter(Settings.AssetsPath + "Scores.txt"))
            {
                for (int i = 0; i < scores.Count; i++)
                {
                    writer.WriteLine(scores[scores.Count - 1 - i]);
                }
            }
        }
    }
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
    public static void AddScore(int amount, int id) => Score[id] += amount;
    public static void ResetArraysAndScore() 
    {
        for (int i = 0; i < Score.Length; i++)
            Score[i] = 0;

        Players.Clear();
        Units.Clear();
    }
    public override object Clone() => MemberwiseClone();
}
