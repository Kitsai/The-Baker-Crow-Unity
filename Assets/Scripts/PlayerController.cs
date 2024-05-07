using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FaceDirection 
    {
        Up,
        Down,
        Left,
        Right
    }

    private Rigidbody2D _rb = null;

    readonly float BASE_FORCE = 13.0f;

    public FaceDirection Facing {get; private set;}
    private float _horizontalAxis;
    private float _verticalAxis;
    private bool _dash = false;
    private

    void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody2D>();
    }
    void Start()
    {
        Facing = FaceDirection.Down;
    }


    void FixedUpdate()
    {
        _horizontalAxis = Input.GetAxis("Horizontal");
        _verticalAxis = Input.GetAxis("Vertical");

        if(_dash)
        {
            _rb.AddForce(2 * BASE_FORCE * new Vector2(_horizontalAxis, _verticalAxis));
            _dash = false;
        }

        _rb.AddForce(new Vector2(_horizontalAxis, _verticalAxis) * BASE_FORCE);

        if(Math.Abs(_horizontalAxis) > Math.Abs(_verticalAxis)) 
        {
            if(_horizontalAxis > 0)
            {
                Facing = FaceDirection.Right;
            }
            else
            {
                Facing = FaceDirection.Left;
            }
        } else {
            if(_verticalAxis > 0)
            {
                Facing = FaceDirection.Up;
            }
            else
            {
                Facing = FaceDirection.Down;
            }
        }
    }

    public void Dash() 
    {
        _dash = true;
    }

    public void Attack() 
    {
        // Attack logic
    }
}
