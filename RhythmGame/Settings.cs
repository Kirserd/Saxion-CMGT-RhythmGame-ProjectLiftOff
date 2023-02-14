using GXPEngine.Core;
public static class Settings
{
    public static bool EditMode = true;

    private static Setup _myGame;
    public static Setup MyGame
    {
        get => _myGame;
        set
        {
            if(MyGame == null)
                _myGame = value;
        }
    }

    private static GLContext _glContext;
    public static GLContext GLContext
    {
        get => _glContext;
        set
        {
            if (EditMode)
                _glContext = value;
        }
    }

    private static System.Drawing.Rectangle _screen;
    public static System.Drawing.Rectangle Screen
    {
        get 
        {
            if (Fullscreen)
                return _screen;
            else
            {
                System.Drawing.Rectangle smallerScreen = _screen;
                smallerScreen.Height = (int)(_screen.Height * 0.8f);
                smallerScreen.Width = (int)(_screen.Width * 0.8f);
                return smallerScreen;
            }       
        }
        set
        {
            if(EditMode)
                _screen = value;
        }
    }

    private static string _assetsPath;
    public static string AssetsPath
    {
        get => _assetsPath;
        set
        {
            if (EditMode)
                _assetsPath = value;
        }
    }

    private static bool _fullscreen;
    public static bool Fullscreen
    {
        get => _fullscreen;
        set
        {
            if (EditMode)
                _fullscreen = value;
        }
    }

    private static float _volume;
    public static float Volume
    {
        get => _volume;
        set
        {
            if (EditMode)
            {
                if(value <= 1 && value >= 0)
                    _volume = value;
            }
        }
    }
    public static void Validate()
    {
        if (Screen == System.Drawing.Rectangle.Empty)
           Screen = GetScreen();
    }

    public static void RefreshAssetsPath()
    {
        string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
        for (int i = 0; i < 3; i++)
        {
            path = path.Substring(0, path.LastIndexOf('\\'));
        }
        AssetsPath = path + "\\Assets\\";
    } 
    private static System.Drawing.Rectangle GetScreen() => System.Windows.Forms.Screen.AllScreens[0].Bounds;
}