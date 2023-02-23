using GXPEngine;

public class AutoAnimationSprite : AnimationSprite
{
    private float _deltaFrameTime;

    public AutoAnimationSprite(string filename, int cols, int rows, float deltaFrameTime = 1) : base(filename, cols, rows)
    {
        _deltaFrameTime = deltaFrameTime;
        SetScaleXY(scaleX / cols, scaleY / rows);
    }
    private void FixedUpdate() => Animate(_deltaFrameTime);
}
