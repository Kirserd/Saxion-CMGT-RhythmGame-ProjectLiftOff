using GXPEngine;
using System;

public class HPBar: AnimationSprite
{
    private Func<int> _hpGetter;
    public HPBar(int id) : base("HPBar", 9, 1)
    {
        _hpGetter = () => Level.Players[id].HP;
        Level.Players[id].OnDamageTaken += OnHpUpdated;
        SetScaleXY(scaleX / 9, scaleY);
    }
    private void OnHpUpdated() => SetFrame(8 - _hpGetter.Invoke());
}
