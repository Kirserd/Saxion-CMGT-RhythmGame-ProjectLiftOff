using GXPEngine;

public class ImpulseParticle : Particle
{
    private bool _alphaDirection = false;
    public ImpulseParticle(string filename, float timeToEnd, int cols = 1, int rows = 1) : base(filename, timeToEnd, cols, rows) { }
    protected override void Update()
    {
        base.Update();
        Impulse();
    }
    private void Impulse()
    {
        if (alpha >= 0 && !_alphaDirection)
        {
            alpha -= Time.deltaTime / 100f;
            if (alpha <= 0)
            {
                _alphaDirection = true;
            }
        }
        else if (alpha <= 1 && _alphaDirection)
        {
            alpha += Time.deltaTime / 100f;
            if (alpha >= 1)
            {
                _alphaDirection = false;
            }
        }
    }
}