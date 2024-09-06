using System.Collections; 
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rigidBody = null;
    protected Animator animator = null;
    protected AudioSource audioSource = null;
    protected Collider2D collider = null;
    [SerializeReference] protected EnemySO metadata;

    public const float DAMAGED_INVULNERABILITY_TIME = 0.5f;

    virtual public void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();
        State = EnemyState.Idling;
        animator.runtimeAnimatorController = metadata.GetAnimatorController();
        Hp = metadata.GetHealth();
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
        else SetState(EnemyState.Idling);
    }
    protected virtual IEnumerator WaitAttackAnimation()
    {
        metadata.PlayAttackSound(audioSource);
        animator.GetNextAnimatorClipInfo(0);
        float attackAnimationTime = 0f;

        yield return new WaitForSeconds(attackAnimationTime);

        if(Random.Range(0,10) < 7) SetState(EnemyState.Idling);
        else SetState(EnemyState.Moving);
    }

    protected virtual IEnumerator WaitForDamageInterval()
    {
       collider.enabled = false; 

       yield return new WaitForSeconds(DAMAGED_INVULNERABILITY_TIME);

       collider.enabled = true;
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
                moveTarget = transform.position + new Vector3(Random.Range(0,100) - 50, Random.Range(0,100) - 50, 0); 
                break;
            case EnemyState.Idling:
                StartCoroutine(WaitIdleTime());
                break;
            case EnemyState.Damaged:
                break;
            case EnemyState.Attacking:
                StartCoroutine(WaitAttackAnimation());
                break;
        }
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
            Vector2.MoveTowards(transform.position, moveTarget, metadata.GetSpeed()*Time.deltaTime);  
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
}
