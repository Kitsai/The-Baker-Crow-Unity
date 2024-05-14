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

    [SerializeField]
    private GameObject _origin = null;

    public bool Friendly {get; private set;}

    void Awake()
    {
        Friendly = _origin.CompareTag(PLAYER_TAG);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
