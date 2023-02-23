using GXPEngine;

public class Particle : AnimationSprite
{
    private float _timeToEnd;
    private float _counter = 0;

    public Particle(string filename, float timeToEnd, int cols = 1, int rows = 1) : base(filename, cols, rows) => _timeToEnd = timeToEnd;
    protected virtual void Update()
    {
        _counter += Time.deltaTime / 1000f;
        if (_counter >= _timeToEnd)
            LateDestroy();
    }
}
