using GXPEngine;
public static class LevelTransitions
{
   public delegate void OnFadeStartedHandler();
    public static OnFadeStartedHandler OnFadeStarted;

    public delegate void OnFadeInFinishedHandler();
    public static OnFadeInFinishedHandler OnFadeInFinished;

    public delegate void OnFadeOutFinishedHandler();
    public static OnFadeOutFinishedHandler OnFadeOutFinished;

    private static float _speed;
    private static sbyte _direction;
    private static bool _isFading = false;
    private static Sprite _fadingScreen = null;
    private static Sprite FadingScreen
    {
        get
        {
            if (_fadingScreen is null)
            {
                Game.main.AddChild(_fadingScreen = new Sprite("FadingScreen"));
                _fadingScreen.SetScaleXY(Game.main.width, Game.main.height);
            }
            return _fadingScreen;
        }
    }
    public static void FadeIn() => Fade(1);
    public static void FadeOut() => Fade(-1);
    private static void Fade(sbyte direction, float speed = 0.002f)
    {
        if (_isFading)
            return;

        _speed = speed;
        _direction = direction;
        _isFading = true;

        FadingScreen.alpha = _direction < 0 ? 0.99f : 0.01f;
        if(OnFadeStarted != null)
            OnFadeStarted.Invoke();
    }
    public static void Timer()
    {
        if(!_isFading)
            return;

        if (_direction > 0) {
            if (FadingScreen.alpha < 1) { 
                FadingScreen.alpha += Time.deltaTime * _speed;
            } else if(OnFadeInFinished != null) {
                _isFading = false;
                OnFadeInFinished.Invoke();
            }
        } 
        else if (_direction < 0) {
            if (FadingScreen.alpha > 0) {
                FadingScreen.alpha -= Time.deltaTime * _speed;
            } else {
                FadingScreen.Destroy();
                _fadingScreen = null;
                _isFading = false;
                if (OnFadeOutFinished != null)
                    OnFadeOutFinished.Invoke();            
            }
        } 
    }
}
