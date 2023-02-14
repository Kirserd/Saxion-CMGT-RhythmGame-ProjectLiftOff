using GXPEngine;
using System;

public class Setup : Game
{
    public delegate void OnGameUpdateHandler();
    public static event OnGameUpdateHandler OnGameUpdate;
    private bool started = false;
    private void Update()
    {
        if (!started)
        {
            LevelManager.LoadLevel("ExampleLevel");
            started = true;
        }
        OnGameUpdate.Invoke();
    }

    private static void Main() => new Setup();
    public Setup() : base(1280, 720, false, pPixelArt: true, pRealWidth: Settings.Screen.Width, pRealHeight: Settings.Screen.Height)
    {
        void settings()
        {
            Settings.RefreshAssetsPath();
            Settings.Validate();
            Settings.Volume = 0.5f;
            Settings.Fullscreen = false;
            SoundManager.Init();
        }
        void subscriptions()
        {
            OnGameUpdate += Camera.Interpolate;
            OnGameUpdate += LevelTransitions.Timer;
            LevelManager.AfterLevelLoaded += LevelTransitions.FadeIn;
            LevelManager.BeforeLevelDisposed += LevelTransitions.FadeOut;
            LevelManager.AfterLevelDisposed += LevelManager.ShowLevel;
            LevelTransitions.OnFadeOutFinished += LevelManager.DisposeLevel;
        }

        settings();
        subscriptions();
        Start();
    }

    public void LoadLevel(string key)
    {
        Level level = null;

        switch (key)
        {
            case "ExampleLevel": ExampleLevel(); 
                break;
            case "ExampleLevel2": ExampleLevel2();
                break;
            default:
                break;
        }

        void ExampleLevel()
        {
            level = new Level("ExampleLevel", new Vector2(0, 0));
            level.AddChildren(new GameObject[] 
            {
                new Unit
                (
                    position: new Vector2(width/2, height/2),
                    hp: new Stat(10),
                    ms: new Stat(2),
                    filename:"Checker"
                ) 
            });
            ObjectPool<Bullet>.GetInstance(typeof(Bullet)).InitPool(200);
        }
        void ExampleLevel2()
        {
            level = new Level("ExampleLevel", new Vector2(0, 0));
        }

        LevelManager.SetCurrentLevel(level);
        AddChild(level);
    }

    public void LoadAttack(string key, Unit owner, float atX = -1, float atY = -1)
    {
        if (atX == -1) atX = owner.x;
        if (atY == -1) atY = owner.y;

        switch (key)
        {
            case "ExampleCircleAttack":
                ExampleCircleAttack();
                break;
            default:
                break;
        }

        void ExampleCircleAttack()
        {
            float angle = 0;
            for (int i = 0; i < 8; i++)
            {
                Bullet bullet = ObjectPool<Bullet>.GetInstance(typeof(Bullet)).GetObject(owner);
                if (bullet is null)
                    return;

                bullet.SetXY(atX, atY);
                bullet.rotation = angle;
                angle += 45;
            }
        }
    }
}