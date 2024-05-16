using System;
using System.Collections;
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

    // COMPONENTS
    private Rigidbody2D _RigidBody = null;
    private Animator _animator = null;

    // CONSTANTS
    const float BASE_FORCE = 20.0f;
    const float DODGE_FORCE = 1200f;

    const float ATTACK_TIME = .4f;
    const float ATTACK_COOLDOWN = 1.0f;
    const float DAMAGED_TIME = .5f;

    // PROPERTIES
    public FaceDirection Facing {get; private set;}
    public Vector2 axis;
    private bool _dash = false;
    public bool Attacking {get; private set;}
    public  bool CanAttack {get; private set;}
    public GameObject _attack;
    public bool Damaged {get; private set;}

    void Awake()
    {
        _RigidBody = GetComponentInChildren<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        Facing = FaceDirection.Down;
        CanAttack = true;
    }

    void FixedUpdate()
    {
        if(_dash)
        {
            _RigidBody.AddForce(DODGE_FORCE * new Vector2(axis.x, axis.y));
            _dash = false;
        }

        _RigidBody.AddForce(new Vector2(axis.x, axis.y) * BASE_FORCE);

        if(Attacking || Damaged)
        {
            _RigidBody.velocity = Vector2.zero;
            _RigidBody.totalForce = Vector2.zero;
            Facing = FaceDirection.Down;
        }

        if(_RigidBody.velocity.magnitude <= 0.2f && !Attacking && !Damaged) _animator.SetBool("Idle", true);
        else _animator.SetBool("Idle", false);
    }

    public void Dodge() 
    {
        _dash = true;
    }

    public void AttackTrigger() 
    {
        if(!Attacking) StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        Attacking = true;
        GameObject attack = Instantiate(_attack, transform.position, Quaternion.identity);
        attack.GetComponent<Attack>().origin = gameObject;
        _animator.SetTrigger("Attacking");


        yield return new WaitForSeconds(ATTACK_TIME);

        Destroy(attack);
        Attacking = false;
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        CanAttack = false;
        yield return new WaitForSeconds(ATTACK_COOLDOWN);
        CanAttack = true;
    }

    public void TakeDamageTrigger() 
    {
        if(!Damaged) StartCoroutine(PerformDamage());
    }

    private IEnumerator PerformDamage()
    {
        Damaged = true;
        _animator.SetTrigger("Damaged");

        yield return new WaitForSeconds(DAMAGED_TIME);

        Damaged = false;
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
