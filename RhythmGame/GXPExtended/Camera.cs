using GXPEngine;
using System.Collections.Generic;
using System;
public static class Camera
{
    private const float INTERPOLATION_STEP = 0.003f;
    private const float SHAKE_POWER = 5f;
    private const float SHAKE_TIME = 18f;

    private static bool _state = true;
    private static bool _shake = false;
    private static Transformable _level;
    private static List<Transformable> _focuses = new List<Transformable>();

    private static Vector2 _cameraPosition = Vector2.zero;
    private static Vector2 _destination = Vector2.zero;

    private static float _shakeCounter;

    public static void Shake() => _shake = true;
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

        _destination = Vector2.zero;
        foreach (Transformable focus in _focuses)
        {
            _destination = new Vector2
            (
                _destination.x - focus.x,
                _destination.y - focus.y
            );
        }
        _destination /= _focuses.Count;

        _destination = new Vector2
        (
            _destination.x + Game.main.width / 2,
            _destination.y + Game.main.height / 2
        );

        Vector2 interpolatedPosition = Vector2.Lerp
        (
            _cameraPosition,
            _destination,
            INTERPOLATION_STEP * Time.deltaTime
        );


        if (_shake)
        {
            _level.SetXY
            (
                interpolatedPosition.x + ((float)(new Random()).NextDouble() - 0.5f) * SHAKE_POWER,
                interpolatedPosition.y + ((float)(new Random()).NextDouble() - 0.5f) * -SHAKE_POWER
            );

            _shakeCounter += Time.deltaTime / 10;
            if (_shakeCounter >= SHAKE_TIME)
            {
                _shakeCounter = 0;
                _shake = false;
            }
        }
        else
        {
            _level.SetXY
            (
                interpolatedPosition.x,
                interpolatedPosition.y
            );
        }
    }
}

