using GXPEngine;
public static class LevelManager
{
    public delegate void AfterLevelLoadedHandler();
    public static AfterLevelLoadedHandler AfterLevelLoaded;

    public delegate void BeforeLevelDisposedHandler();
    public static BeforeLevelDisposedHandler BeforeLevelDisposed;

    public delegate void AfterLevelDisposedHandler();
    public static AfterLevelDisposedHandler AfterLevelDisposed;

    public static Level CurrentLevel { get; private set; }
    
    public static void LoadLevel(string key, bool twoPlayers = false) 
    {
        (Game.main as Setup).LoadLevel(key, twoPlayers);
        if (AfterLevelLoaded != null)
            AfterLevelLoaded.Invoke();
    }
    public static void SetCurrentLevel(Level level)
    {
        if (CurrentLevel != null)
            LateDisposeLevel();

        CurrentLevel = level;
        Camera.SetLevel(level);
    }
    public static void ShowLevel()
    {
        if(CurrentLevel != null)
            CurrentLevel.visible = true;
    }
    public static void DisposeLevel()
    {
        if (CurrentLevel != null)
        {
            CurrentLevel.DestroyChildren();
            CurrentLevel.LateDestroy();
            CurrentLevel = null;
            Camera.ClearFocuses();
        }
        if(AfterLevelDisposed != null)
            AfterLevelDisposed.Invoke();
    }
    public static void LateDisposeLevel() => BeforeLevelDisposed.Invoke();
}
