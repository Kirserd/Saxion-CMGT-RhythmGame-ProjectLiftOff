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
    
    public static void LoadLevel(string key) 
    {
        (Game.main as Setup).LoadLevel(key);
        if (AfterLevelLoaded != null)
            AfterLevelLoaded.Invoke();
    }
    public static void SetCurrentLevel(Level level)
    {
        if (CurrentLevel != null)
            LateDisposeLevel();

        CurrentLevel = level;
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
        }
        if(AfterLevelDisposed != null)
            AfterLevelDisposed.Invoke();
    }
    public static void LateDisposeLevel() => BeforeLevelDisposed.Invoke();
}
