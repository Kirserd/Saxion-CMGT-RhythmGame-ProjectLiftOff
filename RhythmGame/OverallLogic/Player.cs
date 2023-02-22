using GXPEngine;

public class Player : Unit
{
    public const float DASH_POWER = 110;
    public const float DASH_QUICKNESS = 0.8f;

    private bool[] _directionState = new bool[2] { true, true };

    private Vector2 _direction;
    private Vector2 _lastDirection;
    private Vector2 _dashDestination;

    private bool _isActive = true;
    private bool _isDashing = false;

    private bool _isOnCD = false;
    private float _dashCDCounter = 0;
    private const float DASH_COOLDOWN = 1f;

    private float _dashTimer = 0;
    private const float DASH_CUTOFF = 1f;

    private int _id;

    public bool IsImmortal { get => (_isDashing || _isImmortal); }
    private bool _isImmortal;
    private bool _alphaDirection = false;
    private byte _blinkCounter = 0;

    private float _scoreTimer;
    private const float _scoreStep = 6f;

    private int _hpPrevAmount;

    public Player(Vector2 position, int hp, Stat ms) : base(position, hp, ms, "Empty", 1, 1)
    {
        HP = hp;
        _hpPrevAmount = hp;
        Level.OnPlayerAdded += SubscribeToInput;
        SetOrigin(width / 2, height / 2);
    }
    private void Immortality()
    {
        if (alpha >= 0 && !_alphaDirection)
        {
            alpha -= Time.deltaTime / 10;
            if (alpha <= 0)
            {
                _alphaDirection = true;
                _blinkCounter++;
            }
        }
        else if (alpha <= 1 && _alphaDirection)
        {
            alpha += Time.deltaTime / 10;
            if (alpha >= 1)
            {
                _alphaDirection = false;
                _blinkCounter++;
            }
        }
        if (_blinkCounter >= 24)
        {
            _isImmortal = false;
            _blinkCounter = 0;
            _alphaDirection = false;
            alpha = 1;
        }
    }
    private void SubscribeToInput()
    {
        _id = Level.Players.Count - 1;
        InputManager.OnUpButtonPressed[_id] += () => SetDirection(new Vector2(_direction.x, 1));
        InputManager.OnDownButtonPressed[_id] += () => SetDirection(new Vector2(_direction.x, -1));
        InputManager.OnRightButtonPressed[_id] += () => SetDirection(new Vector2(1, _direction.y));
        InputManager.OnLeftButtonPressed[_id] += () => SetDirection(new Vector2(-1, _direction.y));
        InputManager.OnSpaceButtonPressed[_id] += () => StartDash();

        ResetParameters("Player" + _id);

        Level.OnPlayerAdded -= SubscribeToInput;
    }
    private void Update()
    {
        if (!ValidateUpdate())
            return;

        if (_isImmortal)
            Immortality();

        if (_hpPrevAmount > HP)
        {
            SoundManager.PlayOnce("PlayerDamage");
            _hpPrevAmount = HP;
            Camera.Shake();
            _isImmortal = true;
        }

        if (HP > 0)
        {
            _scoreTimer += Time.deltaTime / 10;
            if (_scoreTimer >= _scoreStep)
            {
                _scoreTimer = 0;
                Level.AddScore(4, _id);
            }
        }
        else
        {
            Desubscribe();
            LateDestroy();
            Level.Players.Remove(this);
            Level.CheckPlayers();
        }

        if (_isActive)
        {
            if (_isDashing)
                Dash();
            else
                Move(_direction);
        }
        ClampToBoundaries();

        if (_isOnCD)
        {
            _dashCDCounter += Time.deltaTime;
            if (_dashCDCounter >= DASH_COOLDOWN)
            {
                _dashCDCounter = 0;
                _isOnCD = false;
            }
        }
    }
    private void Desubscribe()
    {
        InputManager.OnUpButtonPressed[_id] -= () => SetDirection(new Vector2(_direction.x, 1));
        InputManager.OnDownButtonPressed[_id] -= () => SetDirection(new Vector2(_direction.x, -1));
        InputManager.OnRightButtonPressed[_id] -= () => SetDirection(new Vector2(1, _direction.y));
        InputManager.OnLeftButtonPressed[_id] -= () => SetDirection(new Vector2(-1, _direction.y));
        InputManager.OnSpaceButtonPressed[_id] -= () => StartDash();
    }
    public void SetDirectionActive(int id, bool state) => _directionState[id] = state;
    private void SetDirection(Vector2 direction)
    {
        if (!_directionState[0]) direction = new Vector2(_direction.x, direction.y);
        if (!_directionState[1]) direction = new Vector2(direction.x, _direction.y);

        _direction = direction;
        if (direction == Vector2.zero)
            return;

        _lastDirection = direction;
    }
    private void Move(Vector2 direction)
    {
        SetXY
        (
            x + MoveSpeed.CurrentAmount * direction.x * Time.deltaTime,
            y + MoveSpeed.CurrentAmount * direction.y * Time.deltaTime
        );
        _direction = Vector2.zero;
    }
    private void ClampToBoundaries()
    {
        Vector2 center = new Vector2(Game.main.width / 2, Game.main.height / 2);
        Vector2 playerPos = new Vector2(x, y);
        Vector2 clampedPos = Vector2.Distance(playerPos, center) <= Level.MAX_RADIUS ? playerPos : center + (playerPos - center).normalized * Level.MAX_RADIUS;
        SetXY(clampedPos.x, clampedPos.y);
    }
    private void StartDash()
    {
        if (_isOnCD)
            return;

        SoundManager.PlayOnce("Dash");
        _isDashing = true;
        _dashDestination = new Vector2
        (
             x + DASH_POWER * _lastDirection.x,
             y + DASH_POWER * _lastDirection.y
        );
    }
    private void Dash()
    {
        Vector2 interpolatedPosition = GetInterpolatedDashStep();
        SetXY(interpolatedPosition.x, interpolatedPosition.y);
    }
    private Vector2 GetInterpolatedDashStep()
    {
        Vector2 interpolatedDashStep = Vector2.Lerp
        (
            new Vector2(x, y),
            _dashDestination,
            DASH_QUICKNESS
        );
        if (Vector2.Distance(interpolatedDashStep, _dashDestination) < 1)
            StopDashing();
        
        _dashTimer += Time.deltaTime;
        if (_dashTimer >= DASH_CUTOFF)
        {
            _dashTimer = 0;
            StopDashing();
        }

        void StopDashing()
        {
            _isDashing = false;
            _isOnCD = true;
            _dashDestination = Vector2.zero;
        }

        return interpolatedDashStep;
    }
}
