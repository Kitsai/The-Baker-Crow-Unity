using UnityEngine;


public class TukiOW : Player
{     

    public override void Start()
    {
        base.Start();
        _inputActions.Tuki.Attack.performed += ctx => OnAttack();
        _inputActions.Tuki.Dodge.performed += ctx => OnDodge();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _inputActions.Tuki.Enable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _inputActions.Tuki.Disable();
    }

    public void OnAttack()
    {
        if(State != PlayerState.DODGING && State != PlayerState.ATTACKING && State != PlayerState.DAMAGED && _playerController.CanAttack)
        {
            SetPlayerState(PlayerState.ATTACKING);
        }
    }


    public void OnDodge()
    {
        if(State != PlayerState.DODGING && State != PlayerState.ATTACKING && State != PlayerState.DAMAGED)
        {
            SetPlayerState(PlayerState.DODGING);
        }
    }

    public override void Update()
    {
        if(hp <= 0) 
        {
            DestroyPlayer();
        }

        if(Input.GetKeyDown(KeyCode.K) && State != PlayerState.DODGING && State != PlayerState.ATTACKING && State != PlayerState.DAMAGED)
        {
            SetPlayerState(PlayerState.DAMAGED);
        }

        base.Update();
    }
}
