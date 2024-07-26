using UnityEngine;

public class TukiOW : Player
{     

    public override void Start()
    {
        base.Start();
    }


    public void OnAttack()
    {
        if(State != PlayerState.DODGING && State != PlayerState.ATTACKING && State != PlayerState.DAMAGED && playerController.CanAttack)
        {
            SetPlayerState(PlayerState.ATTACKING);
        }
    }


    public void OnDash()
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
