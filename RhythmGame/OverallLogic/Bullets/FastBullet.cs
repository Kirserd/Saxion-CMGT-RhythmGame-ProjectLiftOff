public class FastBullet : Bullet
{
    public FastBullet(){}
    protected override void SetCorrespondingParameters()
    {
        ResetParameters
        (
            "FastBullet",
            cols: 3,
            rows: 1,
            addCollider: true
        ) ;
        SetScaleXY(1f / 3f, 1);
        Speed = 10f;
        Damage = 1;
        SetCycle(0, 2);
    }
    protected override void Update()
    {
        base.Update();
        Animate(0.70f);
    }
    protected override void ReturnToPool() => ObjectPool<FastBullet>.GetInstance(typeof(FastBullet)).ReturnObject(this);
}
