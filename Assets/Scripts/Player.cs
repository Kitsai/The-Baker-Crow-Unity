using System.Collections;
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
    public PlayerState State { get; protected set; }

    protected PlayerController _playerController = null;
    protected PlayerInputActions _inputActions = null;

    public virtual void Awake() 
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _playerController = GetComponentInChildren<PlayerController>();

        _inputActions = new PlayerInputActions();
        _inputActions.Base.Enable();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        State = PlayerState.STANDING;
        _inputActions.Base.Movement.performed += ctx => _playerController.OnMovement(ctx.ReadValue<Vector2>()); 
        _inputActions.Base.Movement.canceled += ctx => _playerController.OnMovement(Vector2.zero);
    }

    public virtual void OnEnable()
    {
        _inputActions.Base.Enable();
    }

    public virtual void OnDisable()
    {
        _inputActions.Base.Disable();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        switch (State) 
        {
            case PlayerState.ATTACKING:
                if(!_playerController.Attacking) SetPlayerState(PlayerState.STANDING);
                break;
            case PlayerState.DAMAGED:
                if(!_playerController.Damaged) SetPlayerState(PlayerState.STANDING);
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

    protected virtual IEnumerator WaitAndSetState(PlayerState state, float time)
    {
        yield return new WaitForSeconds(time);
        SetPlayerState(state);
    }

    protected virtual IEnumerator RunAndWait(IEnumerator coroutine, float time)
    {
        yield return StartCoroutine(coroutine);
        yield return new WaitForSeconds(time);
    }

    protected virtual void SetPlayerState(PlayerState state) {
        switch (state)
        {
            case PlayerState.DAMAGED:
                hp--;
                _playerController.TakeDamageTrigger();
                break;
            case PlayerState.ATTACKING:
                _playerController.AttackTrigger();
                break;
            case PlayerState.DODGING:
                _playerController.Dodge();
                StartCoroutine(WaitAndSetState(PlayerState.STANDING, DODGE_TIME));
                break;
            default:
                break;
        }

        State = state;
    }

    protected void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
