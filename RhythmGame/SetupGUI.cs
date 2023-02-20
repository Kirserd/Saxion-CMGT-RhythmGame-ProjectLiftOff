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
            case "Level": Level();
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
        void Level()
        {
            gui = new GUI("Level");
        }
        #endregion

        #region Assigning
        GUIManager.SetCurrentGUI(gui);
        AddChild(gui);
        #endregion
    }
}