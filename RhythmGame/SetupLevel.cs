using GXPEngine;

public partial class Setup : Game
{
    public void LoadLevel(string key, bool twoPlayers = false)
    {
        #region Switch by key
        Level level = null;
        SoundManager.StopAll();

        switch (key)
        {
            case "MenuLevel": MenuLevel();
                break;
            case "ExampleLevel": ExampleLevel(); 
                break;
            default:
                break;
        }
        #endregion

        #region Level Setups
        void ExampleLevel()
        {
            Level.ClearScores();
            SoundManager.Play("Level", true);
            level = new Level("Level");
            Sprite background;
            Enemy enemy;
            level.AddChildren(new GameObject[]
            {
                background = new Sprite("LevelBounds"),
                enemy = new Enemy
                (
                    sequencePath: "ExampleSequence",
                    position: new Vector2(width / 2, height / 2),
                    hp: 10,
                    cols: 6,
                    rows: 2,
                    ms: new Stat(2),
                    filename:"Enemy"
                ),
                new Player
                (
                    position: new Vector2(width / 3, height * 2 / 3),
                    hp: 5,
                    ms: new Stat(0.4f)
                ),
            }) ;
            enemy.SetScaleXY(enemy.scaleX / 6, enemy.scaleY / 2);
            enemy.SetCycle(0, 10);
            enemy.SetOrigin(460, 160);
            background.SetOrigin(0, height / 2.8f);
            ObjectPool<Bullet>.GetInstance(typeof(Bullet)).InitPool(264);
            ObjectPool<FastBullet>.GetInstance(typeof(FastBullet)).InitPool(164);
            ObjectPool<LaserBullet>.GetInstance(typeof(LaserBullet)).InitPool(12);

            #region Second player
            if (twoPlayers)
            {
                level.LateAddChild
                (
                    new Player
                    (
                        position: new Vector2(width * 2 / 3, height * 2 / 3),
                        hp: 10,
                        ms: new Stat(0.4f)
                    )
                );
                Level.TwoPlayers = true;
            }
            else
                Level.TwoPlayers = false;
            #endregion

            Assign();
            GUIManager.LoadGUI("Level");

        }
        void MenuLevel()
        {
            Level.TwoPlayers = false;
            SoundManager.PlayOnce("Menu", true);
            level = new Level("Menu");

            Assign();
            GUIManager.LoadGUI("Menu");
        }
        #endregion

        #region Assigning
        void Assign() 
        {
            LevelManager.SetCurrentLevel(level);
            AddChild(level); 
        }
        #endregion
    }
}