using System.Collections; 
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rigidBody = null;
    protected Animator animator = null;
    protected AudioSource audioSource = null;
    [SerializeReference] protected EnemySO metadata;

    public const float DAMAGED_INVULNERABILITY_TIME = 0.5f;

    virtual public void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animator.runtimeAnimatorController = metadata.GetAnimatorController();
        SetState(EnemyState.Idling);
        Hp = metadata.GetHealth();
        if(metadata.IsAttacker()) metadata.SetAttackSound(audioSource);
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
            case EnemyState.Idling:
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
    protected virtual IEnumerator WaitIdleTime()
    {
        float idleTime = Random.Range(0, 5000) * 0.001f + 2;
        yield return new WaitForSeconds(idleTime);
        if(CheckAttack()) SetState(EnemyState.Attacking);
        else SetState(EnemyState.Moving);
    }
    protected virtual IEnumerator WaitAttackAnimation()
    {
        animator.GetNextAnimatorClipInfo(0);
        float attackAnimationTime = 0f;

        yield return new WaitForSeconds(attackAnimationTime);

        if(Random.Range(0,10) < 7) SetState(EnemyState.Idling);
        else SetState(EnemyState.Moving);
    }

    protected virtual IEnumerator WaitForDamageInterval()
    {
       GetComponent<Collider>().enabled = false; 

       yield return new WaitForSeconds(DAMAGED_INVULNERABILITY_TIME);

       GetComponent<Collider>().enabled = true;
       SetState(EnemyState.Idling);
    }
    

    public EnemyState State {get;protected set;}
    public int Hp {get;protected set;}

    protected Vector2 moveTarget;
    protected virtual void SetState(EnemyState newState)
    {
        State = newState;
        switch(newState)
        {
            case EnemyState.Moving:
                moveTarget = transform.position + new Vector3(Random.Range(0,20) - 10, Random.Range(0,20) - 10, 0); 
                animator.SetBool("Moving", true);
                transform.localScale = new Vector2(Mathf.Sign(moveTarget.x), 1f);
                break;
            case EnemyState.Idling:
                animator.SetBool("Moving", false);
                StartCoroutine(WaitIdleTime());
                break;
            case EnemyState.Damaged:
                break;
            case EnemyState.Attacking:
                metadata.PlayAttackSound(audioSource);
                animator.SetTrigger("Attack");
                StartCoroutine(WaitAttackAnimation());
                break;
        }
    }
    protected void Move() 
    {
        if(Vector2.Distance(moveTarget,(Vector2)transform.position) < Mathf.Epsilon)
        {
            transform.position = moveTarget;
            rigidBody.position = Vector2.zero;
            SetState(EnemyState.Idling);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTarget, metadata.GetSpeed()*Time.deltaTime);  
            rigidBody.linearVelocity = (moveTarget - (Vector2)transform.position).normalized * metadata.GetSpeed();
        }
    }
    protected bool CheckAttack()
    {
        if(!metadata.IsAttacker()) return false;
        Vector2 playerPos = Player.Instance.gameObject.transform.position;
        float playerDist = Vector2.Distance((Vector2)transform.position,playerPos);
        if(playerDist > metadata.GetAttackDistance()) return false;
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
    public void ReceiveDamage(int damage)
    {
        Hp -= damage;
    }
    public void OnCollisionEnter2D()
    {
        moveTarget = transform.position;
        rigidBody.linearVelocity = Vector2.zero;
        SetState(EnemyState.Idling);
    }
}
