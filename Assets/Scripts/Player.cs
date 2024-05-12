using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance {get; protected set;}

    public enum PlayerState 
    {
        STANDING,
        WALKING,
        ATTACKING,
        DODGING,
        DAMAGED
    }

    public float hp = 3;

    [SerializeField]
    private Vector2 _movement;

    const float DODGE_TIME = .5f;

    const float ATTACK_TIME = .4f;
    const float ATTACK_COOLDOWN = 1.0f;
    const float DAMAGED_TIME = .5f;

    public PlayerState State { get; protected set; }

    protected Timer _timer;

    protected PlayerController _playerController = null;
    protected PlayerInputActions _inputActions = null;

    public virtual void Awake() 
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _timer = gameObject.AddComponent<Timer>();
        _playerController = gameObject.AddComponent<PlayerController>();

        _inputActions = new PlayerInputActions();
        _inputActions.Tuki.Enable();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        State = PlayerState.STANDING;
        _inputActions.Tuki.Movement.performed += ctx => _playerController.OnMovement(ctx.ReadValue<Vector2>()); 
        _inputActions.Tuki.Movement.canceled += ctx => _playerController.OnMovement(Vector2.zero);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        switch (State) 
        {
            case PlayerState.DODGING:
                if(_timer.Time > DODGE_TIME) SetPlayerState(PlayerState.WALKING);
                break;
            case PlayerState.ATTACKING:
                if(_timer.Time > ATTACK_TIME) SetPlayerState(PlayerState.STANDING);
                break;
            case PlayerState.DAMAGED:
                if(_timer.Time > DAMAGED_TIME) SetPlayerState(PlayerState.STANDING);
                break;
            case PlayerState.WALKING:
                if(_movement.magnitude == 0) SetPlayerState(PlayerState.STANDING);
                break;
            case PlayerState.STANDING:
                if(_movement.magnitude > 0) SetPlayerState(PlayerState.WALKING);
                break;
            default:
                break;
        }
    }

    protected virtual void SetPlayerState(PlayerState state) {
        switch (state)
        {
            case PlayerState.DAMAGED:
                hp--;
                _playerController.TakeDamageToggle();
                break;
            case PlayerState.ATTACKING:
                _playerController.AttackToggle();
                break;
            case PlayerState.DODGING:
                _playerController.Dodge();
                break;
            default:
                if(State == PlayerState.DAMAGED) _playerController.TakeDamageToggle();
                if(State == PlayerState.ATTACKING) _playerController.AttackToggle();
                break;
        }

        State = state;
        _timer.ResetTime();
    }

    protected void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
