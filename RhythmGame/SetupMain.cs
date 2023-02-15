using GXPEngine;

public partial class Setup : Game
{
    public delegate void OnGameUpdateHandler();
    public static event OnGameUpdateHandler OnGameUpdate;
    private bool started = false;
    private void Update()
    {
        // TODO PASS TO GUI HANDLER
        if (!started)
        {
            LevelManager.LoadLevel("ExampleLevel");
            started = true;
        }
        // TODO PASS TO GUI HANDLER
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
            LevelManager.AfterLevelLoaded += LevelTransitions.FadeOut;
            LevelManager.BeforeLevelDisposed += LevelTransitions.FadeIn;
            LevelManager.AfterLevelDisposed += LevelManager.ShowLevel;
            LevelTransitions.OnFadeInFinished += LevelManager.DisposeLevel;
        }

        settings();
        subscriptions();
        Start();
    }
}