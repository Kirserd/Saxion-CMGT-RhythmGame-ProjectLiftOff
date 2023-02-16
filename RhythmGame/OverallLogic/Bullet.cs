using GXPEngine;

public class Bullet : AnimationSprite
{
    #region CUI(Code User Interface)
    protected virtual void SetCorrespondingParameters() => 
    ResetParameters
    (
        "StandardBullet", 
        cols: 1, 
        rows: 1
    );
    protected virtual void Move()
    {
        x += 10 * Mathf.Sin(rotation);
        y += 10 * Mathf.Cos(rotation);
    }
    protected virtual bool DealDamage(Unit unit)
    {
        if (Owner is null || unit.GetType() == Owner.GetType())
            return false;

        if (unit is Player player && player.IsImmortal)
            return false;

        unit.HealthPoints.ChangeAmount(-_damage);
        return true;
    }
    protected virtual void ReturnToPool() => ObjectPool<Bullet>.GetInstance(typeof(Bullet)).ReturnObject(this);
    #endregion 

    #region Fields and properties
    public Unit Owner;
    
    private float _damage;
    public void SetDamage(float amount, object sender) => _damage = sender is Unit? amount : _damage;
    #endregion

    #region Constructor
    public Bullet() : base("Empty", 0, 0, addCollider: true) { }
    #endregion

    #region Object pool communication
    public void Activate(Unit owner)
    {
        LevelManager.CurrentLevel.AddChild(this);
        SetCorrespondingParameters();
        visible = true;
        Owner = owner;
    }
    public void Deactivate()
    {
        visible = false;
        parent = null;
        Owner = null;
    }
    #endregion

    #region Manager listeners
    private void FixedUpdate()
    {
        if (!visible)
            return;
        Move();
        CheckBoundaries();
    }
    private void CheckBoundaries()
    {
        if (Vector2.Distance(new Vector2(x, y), new Vector2(Owner.x, Owner.y)) > 1200)
            ReturnToPool();
    }
    private void OnCollision(GameObject other)
    {
        if (visible && other is Unit unit)
            if (DealDamage(unit))
                ReturnToPool();
    }
    #endregion
}