using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TukiOW : Player
{
    public float hp = 3;

    [SerializeField]
    private Vector2 _movement;
    private bool _dash = false;

    const float DODGE_FORCE = 1000f;
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
                    if (Math.Abs(_movement.x) > Math.Abs(_movement.y))
                    {
                        _sr.sprite = idleSprites[2];
                        _sr.flipX = _movement.x > 0;
                    }
                    else
                    {
                        if (_movement.y > 0)
                        {
                            _sr.sprite = idleSprites[1];
                        }
                        else
                        {
                            _sr.sprite = idleSprites[0];
                        }
                    }
                break;
            case PlayerState.WALKING:
                
                if (Math.Abs(_movement.x) > Math.Abs(_movement.y))
                {
                    _sr.sprite = idleSprites[2];
                    _sr.flipX = _movement.x > 0;
                }
                else
                {
                    if (_movement.y > 0)
                    {
                        _sr.sprite = idleSprites[1];
                    }
                    else
                    {
                        _sr.sprite = idleSprites[0];
                    }
                }
                    
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
                _dash = true;
                break;
            default:
                break;
        }
        base.SetPlayerState(state);
    }
    
    void FixedUpdate() 
    {
        if(_dash) 
        {
            _dash = false;
            _rb.AddForce(_movement * DODGE_FORCE);
        }
    }

    void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
