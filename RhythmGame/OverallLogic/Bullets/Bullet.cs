using GXPEngine;

public class Bullet : AnimationSprite
{
    #region CUI(Code User Interface)
    protected virtual void SetCorrespondingParameters()
    {
        ResetParameters
        (
            "StandardBullet",
            cols: 4,
            rows: 1,
            addCollider: false
        );
        SetScaleXY(1f / 4f, 1);
        SetOrigin(width / 2, height / 2);
        CollisionRadius = 30f;
        Speed = 3f;
        Damage = 1;
        SetCycle(0, 3);
    }

    protected virtual void Move()
    {
        x += Speed * Mathf.Sin(rotation);
        y += Speed * Mathf.Cos(rotation);
    }
    protected virtual bool DealDamage(Unit unit)
    {
        if (Owner is null || unit.GetType() == Owner.GetType())
            return false;

        if (unit is Player player && player.IsImmortal)
            return false;

        unit.ChangeHP(-Damage);
        return true;
    }
    protected virtual void ReturnToPool() => ObjectPool<Bullet>.GetInstance(typeof(Bullet)).ReturnObject(this);
    #endregion 

    #region Fields and properties
    public Unit Owner;

    protected float CollisionRadius;
    protected float Speed;
    protected int Damage;
    public void SetDamage(int amount, object sender) => Damage = sender is Unit? amount : Damage;
    #endregion

    #region Constructor
    public Bullet() : base("Empty", 0, 0, addCollider: false) { }
    #endregion

    #region Object pool communication
    public void Activate(Unit owner)
    {
        if (LevelManager.CurrentLevel != null)
        {
            LevelManager.CurrentLevel.AddChild(this);
            SetCorrespondingParameters();
            visible = true;
            Owner = owner;
        }
    }
    public void Deactivate()
    {
        visible = false;
        parent = null;
        Owner = null;
    }
    #endregion

    #region Manager listeners
    protected virtual void FixedUpdate()
    {
        if (!visible)
            return;
        Move();
        CheckBoundaries();
    }
    protected virtual void Update()
    {
        CheckCollisions();
        Animate(0.07f);
    }
    protected void CheckBoundaries()
    {
        float distance = Vector2.Distance
        (
            new Vector2(x, y), 
            new Vector2
            (
                Game.main.width / 2, 
                Game.main.height / 2
            )
        );
        if (distance > 620)
        {
            if (distance > 960)
                ReturnToPool();

            alpha = 0;
        }
        else
            alpha = 1;
    }
    protected virtual void CheckCollisions()
    {
        Unit[] units = Level.Units.ToArray();
        foreach (Unit unit in units)
        {
            if (CustomHitTest(unit))
                CustomOnCollision(unit);
        }
    }
    protected virtual bool CustomHitTest(Unit unit) => Vector2.Distance(new Vector2(x, y), new Vector2(unit.x, unit.y)) < CollisionRadius;  
    protected virtual void CustomOnCollision(Unit unit)
    {
        if (visible)
            if (DealDamage(unit))
                ReturnToPool();
    }
    #endregion
}
