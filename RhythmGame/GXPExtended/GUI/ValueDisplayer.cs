using GXPEngine;
using System;

public class ValueDisplayer<T> : EasyDraw
{
    private Func<T> _valueGetter;
    public ValueDisplayer(Func<T> valueGetter, int width, int height) : base(width, height) => _valueGetter = valueGetter;
    private void Update() => Text(_valueGetter().ToString(), clear: true);
}
