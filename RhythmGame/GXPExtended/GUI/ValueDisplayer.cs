using GXPEngine;
using System;

/// <summary>
/// This class gives ability to show refreshing text on screen.
/// It stores reference to value to be displayed.
/// </summary>
public class ValueDisplayer<T> : EasyDraw
{
    private Func<T> _valueGetter;
    public ValueDisplayer(Func<T> valueGetter, int width, int height) : base(width, height) => _valueGetter = valueGetter;
    private void Update() => Text(_valueGetter().ToString(), clear: true);
}
