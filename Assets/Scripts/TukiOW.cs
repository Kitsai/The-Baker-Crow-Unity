using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TukiOW : Player
{
    public override void Update()
    {
        if(hp <= 0) 
        {
            DestroyPlayer();
        }

        if(Input.GetKeyDown(KeyCode.X) && State != PlayerState.DODGING && State != PlayerState.ATTACKING && State != PlayerState.DAMAGED)
        {
            SetPlayerState(PlayerState.ATTACKING);
        }

        if(Input.GetKeyDown(KeyCode.Z) && State != PlayerState.DODGING && State != PlayerState.ATTACKING && State != PlayerState.DAMAGED)
        {
            SetPlayerState(PlayerState.DODGING);
        }

        if(Input.GetKeyDown(KeyCode.K) && State != PlayerState.DODGING && State != PlayerState.ATTACKING && State != PlayerState.DAMAGED)
        {
            SetPlayerState(PlayerState.DAMAGED);
        }

        base.Update();
    }
}
