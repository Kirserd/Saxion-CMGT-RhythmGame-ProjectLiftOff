using System.Windows.Forms;
using GXPEngine;

public partial class Setup : Game
{  
    public void LoadLevel(string key, bool twoPlayers = false)
    {
        #region Switch by key
        Level level = null;

        switch (key)
        {
            case "ExampleLevel": ExampleLevel(); 
                break;
            default:
                break;
        }
        #endregion

        #region Level Setups
        void ExampleLevel()
        {
            level = new Level("ExampleLevel");
            level.AddChildren(new GameObject[]
            {
                new Enemy
                (
                    sequencePath: "ExampleSequence",
                    position: new Vector2(width / 2, height / 2),
                    hp: new Stat(10),
                    ms: new Stat(2),
                    filename:"Checker"
                ),
                new Player
                (
                    position: new Vector2(width / 3, height * 2 / 3),
                    hp: new Stat(10),
                    ms: new Stat(0.4f)
                ),
            });
            ObjectPool<Bullet>.GetInstance(typeof(Bullet)).InitPool(64);
        }
        #endregion

        #region Second player
        if (twoPlayers)
        {
            level.LateAddChild
            (
                new Player
                (
                    position: new Vector2(width * 2 / 3, height * 2 / 3),
                    hp: new Stat(10),
                    ms: new Stat(0.4f)
                )
            );
        }
        #endregion

        #region Assigning
        LevelManager.SetCurrentLevel(level);
        AddChild(level);
        #endregion
    }
}