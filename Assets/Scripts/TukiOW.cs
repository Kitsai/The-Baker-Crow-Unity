using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TukiOW : Player
{   

    public override void Start()
    {
        base.Start();
        _inputActions.Tuki.Attack.performed += ctx => OnAttack();
        _inputActions.Tuki.Dodge.performed += ctx => OnDodge();
 
    }

    public void OnDisable()
    {
        _inputActions.Tuki.Disable();
    }

    public void OnAttack()
    {
        if(State != PlayerState.DODGING && State != PlayerState.ATTACKING && State != PlayerState.DAMAGED)
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
