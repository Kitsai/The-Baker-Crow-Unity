using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TukiOW : Player
{
    [SerializeField]
    private Vector2 _movement;

    [SerializeField]
    private float _dodgeSpeed = 100000000.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.X))
        {
            State = PlayerState.DODGING;
            _rb.velocity = _movement.normalized * _dodgeSpeed * Time.deltaTime;
        }      
    }
}
