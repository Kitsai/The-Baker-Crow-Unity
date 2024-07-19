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

    Vector2 movement;

    [SerializeField] float DODGE_TIME = .5f;
    public PlayerState State { get; protected set; }

    protected PlayerController playerController = null;
    protected PlayerInputActions inputActions = null;

    public virtual void Awake() 
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        playerController = GetComponent<PlayerController>();

        inputActions = new PlayerInputActions();
        inputActions.Base.Enable();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        State = PlayerState.STANDING;
        inputActions.Base.Movement.performed += ctx => playerController.OnMovement(ctx.ReadValue<Vector2>()); 
        inputActions.Base.Movement.canceled += ctx => playerController.OnMovement(Vector2.zero);
    }

    public virtual void OnEnable()
    {
        inputActions.Base.Enable();
    }

    public virtual void OnDisable()
    {
        inputActions.Base.Disable();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        switch (State) 
        {
            case PlayerState.ATTACKING:
                {
                    if(!playerController.Attacking) SetPlayerState(PlayerState.STANDING);
                    break;
                }
            case PlayerState.DAMAGED:
                {               
                    if(!playerController.Damaged) SetPlayerState(PlayerState.STANDING);
                    break;
                }
            case PlayerState.WALKING:
                {
                    if(movement.magnitude == 0) SetPlayerState(PlayerState.STANDING);
                    break;
                }
            case PlayerState.STANDING:
                {
                    if(movement.magnitude > 0) SetPlayerState(PlayerState.WALKING);
                    break;
                }
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
                playerController.TakeDamageTrigger();
                break;
            case PlayerState.ATTACKING:
                playerController.AttackTrigger();
                break;
            case PlayerState.DODGING:
                playerController.Dodge();
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
