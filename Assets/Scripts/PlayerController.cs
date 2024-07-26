using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    Rigidbody2D rigidBody = null;
    Animator animator = null;

    // CONSTANTS
    [SerializeField] float BASE_FORCE = 20.0f;
    [SerializeField] float DODGE_FORCE = 1200f;

    [SerializeField] float ATTACK_TIME = .4f;
    [SerializeField] float ATTACK_COOLDOWN = 1.0f;
    [SerializeField] float DAMAGED_TIME = .5f;

    // PROPERTIES
    public FaceDirection Facing {get; private set;}
    public Vector2 axis;
    private bool dash = false;
    public bool Attacking {get; private set;}
    public bool CanAttack {get; private set;}
    public bool Damaged {get; private set;}

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        Facing = FaceDirection.Down;
        CanAttack = true;
    }

    void Update() 
    {
        if(!Attacking && !Damaged) animator.SetBool("Idle", rigidBody.velocity.magnitude <= 0.2f);
    }
    void LateUpdate() 
    {
        if(Attacking || Damaged) 
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.totalForce = Vector2.zero;
            Facing = FaceDirection.Down;
        }  
    }

    void FixedUpdate()
    {
        if(dash)
        {
            rigidBody.AddForce(DODGE_FORCE * new Vector2(axis.x, axis.y));
            dash = false;
        }

        rigidBody.AddForce(new Vector2(axis.x, axis.y) * BASE_FORCE);
    }


    public void Dodge() 
    {
        dash = true;
    }

    public void AttackTrigger() 
    {
        if(!Attacking) StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        Attacking = true;
        animator.SetTrigger("Attacking");

        yield return new WaitForSeconds(ATTACK_TIME);

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
        animator.SetTrigger("Damaged");

        yield return new WaitForSeconds(DAMAGED_TIME);

        Damaged = false;
    }

    public void OnMove(InputValue value)
    {
        axis = value.Get<Vector2>();
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
        
        animator.SetInteger("Facing", (int)Facing);
    }
}
