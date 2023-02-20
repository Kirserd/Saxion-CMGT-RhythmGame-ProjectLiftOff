using GXPEngine;

public static class GUIManager
{
    public static GUI CurrentGUI { get; private set; }
    public static void LoadGUI(string key) => (Game.main as Setup).LoadGUI(key);
    public static void SetCurrentGUI(GUI gui)
    {
        if(CurrentGUI != null)System.Console.WriteLine($"BEFORE: {CurrentGUI.Name}");
        if (CurrentGUI != null)
            DisposeGUI();

        CurrentGUI = gui;
        System.Console.WriteLine($"After: {CurrentGUI.Name}");
    }
    public static void DisposeGUI()
    {
        if (CurrentGUI != null)
        {
            CurrentGUI.DestroyChildren();
            CurrentGUI.LateDestroy();
            CurrentGUI = null;
        }
    }
}
