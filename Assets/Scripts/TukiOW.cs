using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TukiOW : Player
{
    public float hp = 3;

    [SerializeField]
    private Vector2 _movement;

    const float DODGE_FORCE = 10000f;
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

        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");

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
            case PlayerState.STANDING:
                Sprite spS;
                    if (Math.Abs(_movement.x) > Math.Abs(_movement.y))
                    {
                        spS = Resources.Load<Sprite>("Assets/Images/tuki_lado_idle.png");
                        _sr.flipX = _movement.x > 0;
                    }
                    else
                    {
                        if (_movement.y > 0)
                        {
                            spS = Resources.Load<Sprite>("Assets/Images/Tuki_idle_costas.png");
                        }
                        else
                        {
                            spS = Resources.Load<Sprite>("Assets/Images/Tuki_idle_front.png");
                        }
                    }
                    _sr.sprite = spS;
                break;
            case PlayerState.WALKING:
                Sprite spW;
                    if (Math.Abs(_movement.x) > Math.Abs(_movement.y))
                    {
                        spW = Resources.Load<Sprite>("Assets/Images/tuki_lado_idle.png");
                        _sr.flipX = _movement.x > 0;
                    }
                    else
                    {
                        if (_movement.y > 0)
                        {
                            spW = Resources.Load<Sprite>("Assets/Images/Tuki_idle_costas.png");
                        }
                        else
                        {
                            spW = Resources.Load<Sprite>("Assets/Images/Tuki_idle_front.png");
                        }
                    }
                    _sr.sprite = spW;
                break;
            case PlayerState.DAMAGED:
                if (State != PlayerState.DAMAGED)
                {
                    hp--;
                    _sr.sprite = Resources.Load<Sprite>("Assets/Images/tuki_anim_dano.png");
                }
                break;
            case PlayerState.ATTACKING:
                _sr.sprite = Resources.Load<Sprite>("Assets/Images/tuki_anim_attac.png"); 
                break;
            case PlayerState.DODGING:
                _rb.AddForce(_movement.normalized * DODGE_FORCE);
                break;
            default:
                break;
        }
        base.SetPlayerState(state);
    }
    
    void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
