using UnityEngine;

[CreateAssetMenu]
public class EnemySO : ScriptableObject
{
    [Header("BaseDate")]
    [SerializeField] float speed = 2f;
    [SerializeField] bool attacker = false;
    [SerializeField] float attackDistance = 5;
    [SerializeField] int health = 100;
    [Header("Sounds")]
    [SerializeReference] AudioClip attackSound;
    [Header("Animations")]
    [SerializeReference] RuntimeAnimatorController animatorController;

    public float GetAttackDistance()
    {
        return attackDistance;
    }
    public bool IsAttacker()
    {
        return attacker;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public int GetHealth()
    {
        return health;
    }
    public void SetAttackSound(AudioSource source)
    {
        source.clip = attackSound;
    }
    public void PlayAttackSound(AudioSource source)
    {
        source.PlayOneShot(attackSound);
    }
    public RuntimeAnimatorController GetAnimatorController()
    {
        return animatorController;
    }
}
