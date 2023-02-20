public class LaserBullet : Bullet
{
    public LaserBullet() { }
    protected override void SetCorrespondingParameters()
    {
        ResetParameters
        (
            "LaserBullet",
            cols: 10,
            rows: 1,
            addCollider: true
        );
        Speed = 4f;
        Damage = 1;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.1f, 1);
    }
    protected override bool CustomHitTest(Unit unit) => HitTest(unit);
    protected override void CustomOnCollision(Unit unit)
    {
        if (visible)
            DealDamage(unit);
    }
    protected override void Update()
    {
        base.Update();
            AnimateFixed();
    }
    protected override void ReturnToPool() => ObjectPool<LaserBullet>.GetInstance(typeof(LaserBullet)).ReturnObject(this);
}
