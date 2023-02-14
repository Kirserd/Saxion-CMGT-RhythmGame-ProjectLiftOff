using GXPEngine;

public class Bullet : AnimationSprite
{
    private float _damage;
    public void SetDamage(float amount, object sender) => _damage = sender is Unit? amount : _damage;
    

    public Unit Owner;
    public Bullet() : base("Empty", 0, 0, addCollider: true)
    {
        ResetBaseParameters();
    }
    protected virtual void ResetBaseParameters() => ResetParameters("StandardBullet", 1, 1);
    private void FixedUpdate()
    {
        if (!visible)
            return;
        Move();
    }
    protected virtual void Move() { x += Mathf.Sin(rotation); y += Mathf.Cos(rotation); }

    private void OnCollision(GameObject other)
    {
        if (visible && other is Unit unit)
            if (DealDamage(unit))
                ReturnToPool();
    }
    protected virtual bool DealDamage(Unit unit)
    {
        if (Owner is null || unit.GetType() == Owner.GetType())
            return false;

        unit.HP.ChangeAmount(-_damage);
        return true;
    }
    protected virtual void ReturnToPool() => ObjectPool<Bullet>.GetInstance(typeof(Bullet)).ReturnObject(this);
}