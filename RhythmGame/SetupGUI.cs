using GXPEngine;

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
            AutoAnimationSprite titleMiddle = new AutoAnimationSprite("TitleMiddle", 8, 2, 0.2f);
            title.SetOrigin(title.width / 2, title.height / 2);
            title.SetXY(width / 2, height / 5);
            titleMiddle.SetOrigin(titleMiddle.width / 2, titleMiddle.height);
            titleMiddle.SetXY(width / 2 - 140, height / 6);
            HorizontalList horizontalList = new HorizontalList();
            horizontalList.SetXY(width / 2, height / 2.6f);
            gui.AddChildren( new GameObject[]
            {
                title,
                titleMiddle,
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
            HPBar hpBar1 = new HPBar(0);
            gui.AddChildren(new GameObject[]
            {
                score1,
                scoreText1,
                hpBar1,
            });
            hpBar1.SetXY(0, height);
            hpBar1.SetOrigin(0, hpBar1.height);
            score1.SetXY( -42, height - 154);
            scoreText1.SetXY( -42, height - 34);
            score1.rotation = 270;
            scoreText1.rotation = 270;
            scoreText1.Text("Score: ");
            score1.SetColor(0, 1, 0);
            scoreText1.SetColor(0, 1, 0);
            if (Level.TwoPlayers)
            {
                ValueDisplayer<int> score2 = new ValueDisplayer<int>(() => Level.Score[1], 640, 72);
                EasyDraw scoreText2 = new EasyDraw(640, 72);
                HPBar hpBar2 = new HPBar(1);
                gui.AddChildren(new GameObject[]
                {
                    score2,
                    scoreText2,
                    hpBar2,
                });
                hpBar2.SetXY(940, height);
                hpBar2.SetOrigin(0, hpBar2.height);
                hpBar2.Mirror(true, false);
                score2.SetXY(width - 68, height - 154);
                scoreText2.SetXY(width - 68, height - 34);
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