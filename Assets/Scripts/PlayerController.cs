using System;
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

    private Rigidbody2D _RigidBody = null;
    private Animator _animator = null;

    const float BASE_FORCE = 20.0f;
    const float DODGE_FORCE = 1200f;

    public FaceDirection Facing {get; private set;}
    public Vector2 axis;
    private bool _dash = false;
    private bool _attack = false;
    private bool _damaged = false;

    void Awake()
    {
        _RigidBody = GetComponentInChildren<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        Facing = FaceDirection.Down;
    }

    void FixedUpdate()
    {
        if(_dash)
        {
            _RigidBody.AddForce(DODGE_FORCE * new Vector2(axis.x, axis.y));
            _dash = false;
        }

        _RigidBody.AddForce(new Vector2(axis.x, axis.y) * BASE_FORCE);

        if(_attack || _damaged)
        {
            _RigidBody.velocity = Vector2.zero;
            _RigidBody.totalForce = Vector2.zero;
            Facing = FaceDirection.Down;
        }

        if(_RigidBody.velocity.magnitude <= 0.2f && !_attack && !_damaged) _animator.SetBool("Idle", true);
        else _animator.SetBool("Idle", false);
    }

    public void Dodge() 
    {
        _dash = true;
    }

    public void AttackToggle() 
    {
        _attack = !_attack;
        if(_attack) _animator.SetTrigger("Attacking");
    }

    public void TakeDamageToggle() 
    {
        _damaged = !_damaged;
        if(_damaged) _animator.SetTrigger("Damaged");
    }

    public void OnMovement(Vector2 movement)
    {
        axis = movement;
        if(Math.Abs(axis.x) > Math.Abs(axis.y)) 
        {
            if(axis.x > 0) Facing = FaceDirection.Right;
            else Facing = FaceDirection.Left;
        } 
        else if (Math.Abs(axis.x) < Math.Abs(axis.y))
        {
            if(axis.y > 0) Facing = FaceDirection.Up;
            else Facing = FaceDirection.Down;
        }
        
        _animator.SetInteger("Facing", (int)Facing);
    }
}
