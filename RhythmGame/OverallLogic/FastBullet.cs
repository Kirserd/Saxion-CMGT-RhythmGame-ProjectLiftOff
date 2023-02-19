public class FastBullet : Bullet
{
    public FastBullet(){}
    protected override void SetCorrespondingParameters()
    {
        ResetParameters
        (
            "FastBullet",
            cols: 1,
            rows: 1,
            addCollider: false
        );
        CollisionRadius = 30f;
        Speed = 10f;
        Damage = 1f;
    }
    protected override void ReturnToPool() => ObjectPool<FastBullet>.GetInstance(typeof(FastBullet)).ReturnObject(this);
}
