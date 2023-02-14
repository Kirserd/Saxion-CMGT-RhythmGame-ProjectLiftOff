using GXPEngine;
using System.Collections.Generic;
public static class Camera
{
    private static bool _state = false;
    private static Transformable _level;
    private static List<Transformable> _focuses = new List<Transformable>();

    private static Vector2 _cameraPosition = Vector2.zero;
    private static Vector2 _destination = Vector2.zero;

    public static void SetLevel(Transformable level) => _level = level;
    public static void AddFocus(Transformable focus) => _focuses.Add(focus);
    public static void ClearFocuses() => _focuses.Clear();
    public static void SetState(bool state) => _state = state;

    public static void Interpolate()
    {
        if (!_state || _level is null || _focuses.Count == 0)
            return;

        _cameraPosition = new Vector2
        (
            _level.x,
            _level.y
        );

        foreach (Transformable focus in _focuses)
        {
            _destination = new Vector2
            (
                _destination.x + focus.x,
                _destination.y + focus.y
            );
        }
        _destination /= _focuses.Count;

        Vector2 interpolatedPosition = Vector2.Lerp
        (
            _cameraPosition,
            _destination, 0.04f
        );
        
        _level.SetXY
        (
            interpolatedPosition.x,
            interpolatedPosition.y
        );
    }
}

