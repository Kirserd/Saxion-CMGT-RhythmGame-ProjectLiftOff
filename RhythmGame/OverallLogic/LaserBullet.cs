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

public class HomingBullet : Bullet
{
    private Unit _target;
    public HomingBullet() { }
    protected override void SetCorrespondingParameters()
    {
        ResetParameters
        (
            "HomingBullet",
            cols: 10,
            rows: 1,
            addCollider: false
        );
        CollisionRadius = 30f;
        Speed = 2f;
        Damage = 1;
        SetOrigin(width / 2, height / 2);
    }
    protected override void Move()
    {
        _target = TakeClosestUnit();
        base.Move();
    }
    private Unit TakeClosestUnit()
    {
        Unit[] units = Level.Units.ToArray();
        float minDist = -1;
        Unit closestUnit = null;
        foreach(Unit unit in units)
        {
            if (unit.GetType() != Owner.GetType())
                if (Vector2.Distance(new Vector2(x, y), new Vector2(unit.x, unit.y)) < minDist || minDist == -1)
                    closestUnit = unit;
        }
        return closestUnit;
    }
    protected override void ReturnToPool() => ObjectPool<HomingBullet>.GetInstance(typeof(HomingBullet)).ReturnObject(this);
}