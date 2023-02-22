using GXPEngine;
public class ChaoticBullet : Bullet
{
    private Vector2 _target;
    private const float ROTATION_SPEED = 0.04f;

    private float _counter;
    private const float STEP = 40;
    public ChaoticBullet() { }
    protected override void SetCorrespondingParameters()
    {
        ResetParameters
        (
            "HomingBullet",
            cols: 4,
            rows: 1,
            addCollider: false
        );
        SetScaleXY(1f / 4f, 1);
        CollisionRadius = 30f;
        Speed = 4f;
        Damage = 1;
        SetOrigin(width / 2, height / 2);
    }
    protected override void Move()
    {
        base.Move();
        _counter += Time.deltaTime;
        if(_counter >= STEP)
        {
            _counter = 0;
            RotateTowardsTarget();
        }
    }
    private void RotateTowardsTarget()
    {
        _target = TakeClosestUnitPosition();
        rotation = Mathf.LerpAngle
        (
            rotation,
            AngleFromAToB
            (
                new Vector2(x, y),
                -_target
            ),
            ROTATION_SPEED
        );
    }
    private float AngleFromAToB(Vector2 A, Vector2 B)
    {
        Vector2 Direction = (B - A).normalized;
        return Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
    }
    private Vector2 TakeClosestUnitPosition()
    {
        Unit[] units = Level.Units.ToArray();
        float minDist = -1;
        Unit closestUnit = null;
        foreach (Unit unit in units)
        {
            if (unit.GetType() != Owner.GetType()) 
            {
                float dist = Vector2.Distance(new Vector2(x, y), new Vector2(unit.x, unit.y));
                if (dist < minDist || minDist == -1)
                {
                    minDist = dist;
                    closestUnit = unit;
                }   
            }
        }
        return new Vector2(closestUnit.x , closestUnit.y);
    }
    protected override void ReturnToPool() => ObjectPool<ChaoticBullet>.GetInstance(typeof(ChaoticBullet)).ReturnObject(this);
}