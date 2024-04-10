using System.Collections;
using System.Collections.Generic;
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

    public PlayerState State {
        get {
            return _state;
        } 
        protected set {
            _state = value;
        }
    }

    [SerializeField]
    protected PlayerState _state;

    protected Timer _timer;

    protected Rigidbody2D _rb = null;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        State = PlayerState.STANDING; 
    }

    // Update is called once per frame
    void Update()
    {
        if(_rb.velocity.magnitude <= 0.1f 
        && State != PlayerState.DAMAGED 
        && State != PlayerState.ATTACKING 
        && State != PlayerState.DODGING
        ) {
            State = PlayerState.STANDING;
        }
        else
        {
            State = PlayerState.WALKING;
        }
    }

    public Vector2 Position {
        get {
            return _rb.position;
        }
    }

    protected virtual void SetPlayerState(PlayerState state) {
        State = state;
        _timer.ResetTime();
    }
}
