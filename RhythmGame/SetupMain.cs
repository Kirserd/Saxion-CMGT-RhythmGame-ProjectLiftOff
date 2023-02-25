using GXPEngine;
using System.Drawing;

public partial class Setup : Game
{
    public delegate void OnGameUpdateHandler();
    public static event OnGameUpdateHandler OnGameUpdate;
    public static Sprite postProcessing;
    private void Update() => OnGameUpdate.Invoke();
    private static void Main() => new Setup();
    public Setup() : base(1280, 720, false, pPixelArt: true, pRealWidth: Settings.Screen.Width, pRealHeight: Settings.Screen.Height)
    {
        void settings()
        {
            Settings.RefreshAssetsPath();
            Settings.Validate();
            Settings.Volume = 0.4f;
            Settings.Fullscreen = false;
            SoundManager.Init();
            EasyDraw.DefaultFont = new Font("OCR A Extended", 24); 
        }
        void subscriptions()
        {
            OnGameUpdate += Camera.Interpolate;
            OnGameUpdate += LevelTransitions.Timer;
            OnGameUpdate += InputManager.ListenToInput;
            OnGameUpdate += FirstLoad;
            OnGameUpdate += () => SetChildIndex(postProcessing, 1000);
            InputManager.Start();
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

        #region Post-processing
        AddChild(postProcessing = new Sprite("Empty"));
        Sprite screenEffect = new Sprite("ScreenEffect");
        screenEffect.blendMode = BlendMode.ADDITIVE;
        postProcessing.AddChildren(new GameObject[]
        {
            screenEffect,
        });
        #endregion

        OnGameUpdate -= FirstLoad;
    }
}