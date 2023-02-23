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
            case "Score": Score();
                break;
            case "Score2": Score2();
                break;
            case "LeaderBoard": LeaderBoard();
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
            Button audio = new Button("AudioOn");
            audio.SetAction(AudioOnOff);
            horizontalList.AddButtons(new Button[]
            {
                audio,
                new Button("Leaderboard", () => LoadGUI("LeaderBoard")),
                new Button("1P", () => LevelManager.LateLoadLevel("ExampleLevel")),
                new Button("2P", () => LevelManager.LateLoadLevel("ExampleLevel", true)),
                new Button("Exit", Exit)
            });

            void AudioOnOff()
            {
                if (Settings.Volume > 0)
                {
                    audio.ResetParameters("AudioOff");
                    audio.SetOrigin(audio.width / 2, audio.height / 2);
                    Settings.Volume = 0;
                }
                else
                {
                    audio.ResetParameters("AudioOn");
                    audio.SetOrigin(audio.width / 2, audio.height / 2);
                    Settings.Volume = 0.8f;
                }
            }
        }
        void LeaderBoard()
        {
            gui = new GUI("LeaderBoard");
            Sprite title = new Sprite("Title");
            AutoAnimationSprite titleMiddle = new AutoAnimationSprite("TitleMiddle", 8, 2, 0.2f);
            title.SetOrigin(title.width / 2, title.height / 2);
            title.SetXY(width / 2, height / 5);
            titleMiddle.SetOrigin(titleMiddle.width / 2, titleMiddle.height);
            titleMiddle.SetXY(width / 2 - 140, height / 6);
            ConstValueDisplayer<string> leaderboardTitle = new ConstValueDisplayer<string>("Leaderboard by scores: ", 680, 64);
            leaderboardTitle.SetColor(0.4f, 1, 0.4f);
            leaderboardTitle.SetXY(width / 14f, height / 2.1f);
            ConstValueDisplayer<string> pressAnyButtonText = new ConstValueDisplayer<string>(" >> press any button to return <<", 1280, 64);
            pressAnyButtonText.SetColor(0.2f, 0.6f, 0.2f);
            pressAnyButtonText.SetXY(320, height - 100);
            ScoreList scoreList = new ScoreList();
            scoreList.SetXY(width / 3.8f, height / 2.1f);
            gui.AddChildren(new GameObject[]
            {
                title,
                titleMiddle,
                leaderboardTitle,
                pressAnyButtonText,
                scoreList,
            });
        }
        void Score()
        {
            gui = new GUI("Score");
            ConstValueDisplayer<string> scoreText = new ConstValueDisplayer<string>("Final score: ", 680, 90);
            scoreText.SetColor(0.2f, 0.6f, 0.2f);
            scoreText.SetXY(200, height / 5);
            ConstValueDisplayer<string> score = new ConstValueDisplayer<string>(Level.Score[0].ToString(), 1280, 220);
            score.SetColor(0.2f, 0.6f, 0.2f);
            score.SetXY(200, height / 5);
            score.TextSize(64);
            ConstValueDisplayer<string> pressAnyButtonText = new ConstValueDisplayer<string>(" >> press any button to return <<", 1280, 64);
            pressAnyButtonText.SetColor(0.2f, 0.6f, 0.2f);
            pressAnyButtonText.SetXY(320, height - 100);
            gui.AddChildren(new GameObject[]
            {
                scoreText,
                score,
                pressAnyButtonText
            });
            InputManager.OnAnyButtonPressed += Return;
        }
        void Score2()
        {
            gui = new GUI("Score2");
            ConstValueDisplayer<string> scoreText = new ConstValueDisplayer<string>("Final score (P1): ", 680, 90);
            scoreText.SetColor(0.2f, 0.6f, 0.2f);
            scoreText.SetXY(200, height / 5);
            ConstValueDisplayer<string> score = new ConstValueDisplayer<string>(Level.Score[0].ToString(), 1280, 220);
            score.SetColor(0.2f, 0.6f, 0.2f);
            score.SetXY(200, height / 5);
            score.TextSize(64);
            ConstValueDisplayer<string> pressAnyButtonText = new ConstValueDisplayer<string>(" >> press any button to return <<", 1280, 64);
            pressAnyButtonText.SetColor(0.2f, 0.6f, 0.2f);
            pressAnyButtonText.SetXY(320, height - 100);
            ConstValueDisplayer<string> scoreText2 = new ConstValueDisplayer<string>("Final score (P2): ", 680, 90);
            scoreText2.SetColor(0.2f, 0.6f, 0.2f);
            scoreText2.SetXY(800, height / 5);
            ConstValueDisplayer<string> score2 = new ConstValueDisplayer<string>(Level.Score[1].ToString(), 1280, 220);
            score2.SetColor(0.2f, 0.6f, 0.2f);
            score2.SetXY(800, height / 5);
            score2.TextSize(64);
            gui.AddChildren(new GameObject[]
            {
                scoreText,
                score,
                scoreText2,
                score2,
                pressAnyButtonText
            });
            InputManager.OnAnyButtonPressed += Return;

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
        void Return()
        {
            InputManager.OnAnyButtonPressed -= Return;
            LevelManager.LateLoadLevel("MenuLevel");
        }
        #endregion

        #region Assigning
        GUIManager.SetCurrentGUI(gui);
        AddChild(gui);
        #endregion
    }
}