using UnityEngine;

public class EnemySO : ScriptableObject
{
    [Header("BaseDate")]
    [SerializeField] float speed = 50;
    [SerializeField] bool attacker = false;
    [SerializeField] float attackDistance = 20;
    [SerializeField] int health = 100;
    [Header("Sounds")]
    [SerializeReference] AudioClip deathSound;
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
    public void PlayDeathSound(AudioSource source)
    {
        source.Play(deathSound);
    }
    public void PlayAttackSound(AudioSource source)
    {
        source.Play(attackSound);
    }
    public RuntimeAnimatorController GetAnimatorController()
    {
        return animatorController;
    }
}
