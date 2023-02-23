public class LaserBullet : Bullet
{
    public LaserBullet() { }
    protected override void SetCorrespondingParameters()
    {
        ResetParameters
        (
            "LaserBullet",
            cols: 4,
            rows: 1,
            addCollider: true
        );
        SetScaleXY(1f / 4f, 1);
        Speed = 4f;
        Damage = 1;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.1f, 1);
    }
    protected override void CustomOnCollision(Unit unit)
    {
        if (visible)
            DealDamage(unit);
    }
    protected override void Update()
    {
        base.Update();
            Animate(0.05f);
    }
    protected override void ReturnToPool() => ObjectPool<LaserBullet>.GetInstance(typeof(LaserBullet)).ReturnObject(this);
}
