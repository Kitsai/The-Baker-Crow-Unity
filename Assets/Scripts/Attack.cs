using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    const string PLAYER_TAG = "Player";

    public enum AttackType
    {
        Melee,
        Ranged
    }

    public GameObject origin = null;

    public bool Friendly {get; private set;}

    void Start()
    {
        Friendly = origin.CompareTag(PLAYER_TAG);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!Friendly && other.CompareTag(PLAYER_TAG))
        {
            Player player = Player.Instance;
        }
    }
}
