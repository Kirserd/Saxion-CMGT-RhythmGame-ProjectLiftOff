using GXPEngine;

public class Bullet : AnimationSprite
{
    #region CUI(Code User Interface)
    protected virtual void SetCorrespondingParameters()
    {
        ResetParameters
        (
            "StandardBullet",
            cols: 1,
            rows: 1,
            addCollider: false
        );
    }
    protected virtual void Move()
    {
        x += 7 * Mathf.Sin(rotation);
        y += 7 * Mathf.Cos(rotation);
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
    protected const float COLLISION_RADIUS = 20f;
    public Unit Owner;
    
    private float _damage;
    public void SetDamage(float amount, object sender) => _damage = sender is Unit? amount : _damage;
    #endregion

    #region Constructor
    public Bullet() : base("Empty", 0, 0, addCollider: false) { }
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
    private void Update() => CheckCollisions();
    private void CheckBoundaries()
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
    private void CheckCollisions()
    {
        Unit[] units = Level.Units.ToArray();
        foreach (Unit unit in units)
        {
            if (CustomHitTest(unit))
                CustomOnCollision(unit);
        }
    }
    private bool CustomHitTest(Unit unit)
    {
        return Vector2.Distance(new Vector2(x, y), new Vector2(unit.x, unit.y)) < COLLISION_RADIUS;
    }
    private void CustomOnCollision(Unit unit)
    {
        if (visible)
            if (DealDamage(unit))
                ReturnToPool();
    }
    #endregion
}