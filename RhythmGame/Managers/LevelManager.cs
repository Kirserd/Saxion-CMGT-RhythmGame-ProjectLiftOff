using GXPEngine;
using System;
public static class LevelManager
{
    public delegate void AfterLevelLoadedHandler();
    public static AfterLevelLoadedHandler AfterLevelLoaded;

    public delegate void BeforeLevelDisposedHandler();
    public static BeforeLevelDisposedHandler BeforeLevelDisposed;

    public delegate void AfterLevelDisposedHandler();
    public static AfterLevelDisposedHandler AfterLevelDisposed;

    public static Level CurrentLevel { get; private set; } = null;
    private static Action _levelLoadAction;

    public static void LateLoadLevel(string key, bool twoPlayers = false)
    {
        _levelLoadAction = () => (Game.main as Setup).LoadLevel(key, twoPlayers);
        if(CurrentLevel != null )
            BeforeLevelDisposed.Invoke();
    }
    public static void SetCurrentLevel(Level level)
    {
        if (CurrentLevel != null)
            LateDisposeLevel();

        CurrentLevel = level;
        Camera.SetLevel(level);
    }
    public static void LoadLevel()
    {
        if (_levelLoadAction != null)
        {
            _levelLoadAction.Invoke();
            if (AfterLevelLoaded != null)
                AfterLevelLoaded.Invoke();
        }
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
