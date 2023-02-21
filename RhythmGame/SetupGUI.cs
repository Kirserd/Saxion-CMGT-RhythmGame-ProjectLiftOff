using GXPEngine;
using System;

public partial class Setup : Game
{
    public void LoadGUI(string key)
    {
        #region Switch by key
        GUI gui = null;

        switch (key)
        {
            case "Menu": Menu(); 
                break;
            case "Level": ExampleLevel();
                break;
            default:
                break;
        }
        #endregion

        #region GUI Setups
        void Menu()
        {
            gui = new GUI("Menu");
            Sprite title = new Sprite("Title");
            title.SetOrigin(title.width / 2, title.height / 2);
            title.SetXY(width / 2, height * 1 / 5);
            HorizontalList horizontalList = new HorizontalList();
            horizontalList.SetXY(width / 2, height / 4);
            gui.AddChildren( new GameObject[]
            {
                title,
                horizontalList
            });
            horizontalList.AddButtons(new Button[]
            {
                new Button("Settings"),
                new Button("Leaderboard"),
                new Button("1P", () => LevelManager.LateLoadLevel("ExampleLevel")),
                new Button("2P", () => LevelManager.LateLoadLevel("ExampleLevel", true)),
                new Button("Exit", Exit)
            });
        }
        void ExampleLevel()
        {
            gui = new GUI("Level");
            ValueDisplayer<int> score1 = new ValueDisplayer<int>(() => Level.Score[0], 640, 72);
            EasyDraw scoreText1 = new EasyDraw(640, 72);
            gui.AddChildren(new GameObject[]
            {
                score1,
                scoreText1
            });
            score1.SetXY( -16, height - 140);
            scoreText1.SetXY( -16, height - 20);
            score1.rotation = 270;
            scoreText1.rotation = 270;
            scoreText1.Text("Score: ");
            score1.SetColor(0, 1, 0);
            scoreText1.SetColor(0, 1, 0);
            if (Level.TwoPlayers)
            {
                ValueDisplayer<int> score2 = new ValueDisplayer<int>(() => Level.Score[1], 640, 72);
                EasyDraw scoreText2 = new EasyDraw(640, 72);
                gui.AddChildren(new GameObject[]
                {
                    score2,
                    scoreText2
                });
                score2.SetXY(width - 90, height - 140);
                scoreText2.SetXY(width - 90, height - 20);
                score2.rotation = 270;
                scoreText2.rotation = 270;
                scoreText2.Text("Score: ");
                score2.SetColor(0, 1, 0);
                scoreText2.SetColor(0, 1, 0);
            }
        }
        #endregion

        #region Assigning
        GUIManager.SetCurrentGUI(gui);
        AddChild(gui);
        #endregion
    }
}