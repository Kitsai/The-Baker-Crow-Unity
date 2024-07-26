using UnityEngine;

public class TukiOW : Player
{     
    public void OnAttack()
    {
        if((State is PlayerState.STANDING or PlayerState.WALKING)  && playerController.CanAttack) {
            SetPlayerState(PlayerState.ATTACKING);
        }
    }


    public void OnDash()
    {
        if(State is PlayerState.STANDING or PlayerState.WALKING)
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

        if(Input.GetKeyDown(KeyCode.K) && (State is PlayerState.STANDING or PlayerState.WALKING))
        {
            SetPlayerState(PlayerState.DAMAGED);
        }

        base.Update();
    }
}
