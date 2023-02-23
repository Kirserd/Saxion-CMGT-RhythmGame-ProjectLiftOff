using GXPEngine;

public class ConstValueDisplayer<T> : EasyDraw
{
    private T _value;
    public ConstValueDisplayer(T valueGetter, int width, int height) : base(width, height) => _value = valueGetter;
    private void Update() => Text(_value.ToString(), clear: true);
}
