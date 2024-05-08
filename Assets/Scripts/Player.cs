using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    protected Rigidbody2D _rb = null;

    protected Collider2D _col = null;

    protected PlayerController _pc = null;

    void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        _timer = gameObject.AddComponent<Timer>();
        _rb = GetComponentInChildren<Rigidbody2D>();
        _col = GetComponentInChildren<Collider2D>();
        _pc = GetComponentInChildren<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        State = PlayerState.STANDING; 
    }

    // Update is called once per frame
    public virtual void Update()
    {
        switch (State) 
        {
            case PlayerState.DODGING:
                if(_timer.Time > DODGE_TIME)
                {
                    SetPlayerState(PlayerState.WALKING);
                }
                break;
            case PlayerState.ATTACKING:
                if(_timer.Time > ATTACK_TIME)
                {
                    SetPlayerState(PlayerState.STANDING);
                }
                break;
            case PlayerState.DAMAGED:
                if(_timer.Time > DAMAGED_TIME)
                {
                    SetPlayerState(PlayerState.STANDING);
                }
                break;
            case PlayerState.WALKING:
                if(_movement.magnitude == 0)
                {
                    SetPlayerState(PlayerState.STANDING);
                }
                break;
            case PlayerState.STANDING:
                if(_movement.magnitude > 0)
                {
                    SetPlayerState(PlayerState.WALKING);
                }
                break;
            default:
                break;
        }
    }

    public Vector2 Position {
        get {
            return _rb.position;
        }
    }

    protected virtual void SetPlayerState(PlayerState state) {
        switch (state)
        {
            case PlayerState.DAMAGED:
                hp--;
                _pc.TakeDamageToggle();
                break;
            case PlayerState.ATTACKING:
                _pc.AttackToggle();
                break;
            case PlayerState.DODGING:
                _pc.Dodge();
                break;
            default:
                if(State == PlayerState.DAMAGED) _pc.TakeDamageToggle();
                if(State == PlayerState.ATTACKING) _pc.AttackToggle();
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
