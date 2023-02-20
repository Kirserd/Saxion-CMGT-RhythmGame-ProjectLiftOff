using GXPEngine;

public partial class Setup : Game
{
    public delegate void OnGameUpdateHandler();
    public static event OnGameUpdateHandler OnGameUpdate;
    private void Update() => OnGameUpdate.Invoke();
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
            OnGameUpdate += InputManager.ListenToInput;
            OnGameUpdate += FirstLoad;
            LevelManager.BeforeLevelDisposed += LevelTransitions.FadeIn;
            LevelManager.AfterLevelDisposed += LevelManager.LoadLevel;
            LevelManager.AfterLevelLoaded += LevelTransitions.FadeOut;
            LevelTransitions.OnFadeInFinished += LevelManager.DisposeLevel;
            Level.OnPlayerAdded += () => Camera.AddFocus(Level.Players[Level.Players.Count - 1]);
        }

        settings();
        subscriptions();
        Start();
    }
    private void FirstLoad()
    {
        LevelManager.LateLoadLevel("MenuLevel");
        LevelManager.LoadLevel();
        OnGameUpdate -= FirstLoad;
    }
}