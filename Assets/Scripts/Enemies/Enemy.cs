using System.Collections; 
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public const int ENEMY_ATTACK_DIST = 10;
    protected Rigidbody2D rigidBody = null;
    protected Animator animator = null;
    [SerializeField] Ingredient dropItem;
    [SerializeField] GameObject ingredientInstance;

    virtual public void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        State = EnemyState.Idling;
        Attacker = false;
    }
    virtual public void Update()
    {
        if(Hp <= 0) Defeated();

        switch (State)
        {
            case EnemyState.Moving:
                Move();
                break;
            case EnemyState.Attacking:
                break;
            case EnemyState.Idling:
                break;
            case EnemyState.Damaged:
                break; 
        }
    }
    public enum EnemyState
    {
        Moving,
        Attacking,
        Idling,
        Damaged,
    }

    protected virtual IEnumerator WaitAndSetState(EnemyState state, float time)
    {
        yield return new WaitForSeconds(time);
        SetState(state);
    }
    protected virtual IEnumerator WaitAndDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    

    public EnemyState State {get;protected set;}
    public int Hp {get;protected set;}
    public bool Attacker {get; protected set;}

    protected Vector2 moveTarget;
    protected float moveAngle;
    protected float speed;
    protected virtual void SetState(EnemyState newState)
    {
        State = newState;
    }
    protected void Move() 
    {
        if(Vector2.Distance(moveTarget,(Vector2)transform.position) < Mathf.Epsilon)
        {
            transform.position = moveTarget;
            SetState(EnemyState.Idling);
        }
        else
        {
            Vector2.MoveTowards(transform.position, moveTarget, speed*Time.deltaTime);  
        }
    }
    protected bool CheckAttack()
    {
        if(!Attacker) return false;
        Vector2 playerPos = Player.Instance.gameObject.transform.position;
        float playerDist = Vector2.Distance((Vector2)transform.position,playerPos);
        if(playerDist > ENEMY_ATTACK_DIST) return false;
        return true;
    }
    protected void DropItem()
    {
        
    }
    protected void Defeated()
    {
       animator.SetTrigger("Death");
       DropItem();
       StartCoroutine(WaitAndDestroy(animator.GetCurrentAnimatorStateInfo(0).length)); 
    }
}
