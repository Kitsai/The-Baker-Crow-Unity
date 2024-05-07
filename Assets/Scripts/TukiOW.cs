using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TukiOW : Player
{
    public float hp = 3;

    [SerializeField]
    private Vector2 _movement;

    const float DODGE_TIME = .5f;

    const float ATTACK_TIME = .4f;
    const float ATTACK_COOLDOWN = 1.0f;

    public Sprite[] idleSprites;
    public Sprite[] walkSprites;

    // Update is called once per frame
    void Update()
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


    override protected void SetPlayerState(PlayerState state)
    {
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
        base.SetPlayerState(state);
    }


    void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
