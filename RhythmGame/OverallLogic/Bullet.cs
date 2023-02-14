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
        x += Mathf.Sin(rotation);
        y += Mathf.Cos(rotation);
    }
    protected virtual bool DealDamage(Unit unit)
    {
        if (Owner is null || unit.GetType() == Owner.GetType())
            return false;

        unit.HP.ChangeAmount(-_damage);
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
    }
    private void OnCollision(GameObject other)
    {
        if (visible && other is Unit unit)
            if (DealDamage(unit))
                ReturnToPool();
    }
    #endregion
}

public class ExampleBullet : Bullet
{
    public ExampleBullet() { }

    protected override void Move()
    {
        base.Move();       
    }

    protected override void ReturnToPool() => ObjectPool<ExampleBullet>.GetInstance(typeof(ExampleBullet)).ReturnObject(this);

    protected override void SetCorrespondingParameters() =>
    ResetParameters
    (
        "ExampleBullet",
        cols: 1,
        rows: 1
    );
}