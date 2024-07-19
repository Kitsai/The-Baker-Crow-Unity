using UnityEngine;


public class TukiOW : Player
{     

    public override void Start()
    {
        base.Start();
        inputActions.Tuki.Attack.performed += ctx => OnAttack();
        inputActions.Tuki.Dodge.performed += ctx => OnDodge();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        inputActions.Tuki.Enable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        inputActions.Tuki.Disable();
    }

    public void OnAttack()
    {
        if(State != PlayerState.DODGING && State != PlayerState.ATTACKING && State != PlayerState.DAMAGED && playerController.CanAttack)
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
