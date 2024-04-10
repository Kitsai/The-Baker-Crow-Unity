using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TukiOW : Player
{
    public float hp = 3;

    [SerializeField]
    private Vector2 _movement;

    const float DODGE_FORCE = 12000f;
    const float DODGE_TIME = .5f;

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Z) && State != PlayerState.DODGING && State != PlayerState.ATTACKING && State != PlayerState.DAMAGED)
        {
            SetPlayerState(PlayerState.DODGING);
        }

        if(State == PlayerState.DODGING && _timer.Time > DODGE_TIME)
        {
            SetPlayerState(PlayerState.WALKING);
        }   
    }


    override protected void SetPlayerState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.DODGING:
                _rb.AddForce(_movement * DODGE_FORCE);
                break;
            default:
                break;
        }
        base.SetPlayerState(state);
    }
}
