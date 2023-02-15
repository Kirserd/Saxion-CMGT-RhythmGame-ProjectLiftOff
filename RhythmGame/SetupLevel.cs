using GXPEngine;

public partial class Setup : Game
{  
    public void LoadLevel(string key)
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
            level = new Level("ExampleLevel", new Vector2(0, 0));
            level.LateAddChildren(new GameObject[] 
            {
                new Unit
                (
                    position: new Vector2(width/2, height/2),
                    hp: new Stat(10),
                    ms: new Stat(2),
                    filename:"Checker"
                )
            });
            ObjectPool<Bullet>.GetInstance(typeof(Bullet)).InitPool(64);
        }
        #endregion

        #region Assigning
        LevelManager.SetCurrentLevel(level);
        AddChild(level);
        #endregion
    }
}