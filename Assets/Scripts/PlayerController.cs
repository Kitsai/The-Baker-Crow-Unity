using System;
using System.Collections;
using System.Collections.Generic;
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

    private Rigidbody2D rb = null;

    [SerializeField]
    private float BASE_FORCE = 13.0f;

    [SerializeField]
    private FaceDirection faceDirection;
    private float horizontalAxis;
    private float verticalAxis;

    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();

        if(Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.LinuxPlayer ) {
            rb.mass = 1.0f;
        } else {
            rb.mass = 10.0f;
        }
    }
    void Start()
    {
        faceDirection = FaceDirection.Down;
    }


    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        rb.AddRelativeForce(new Vector2(horizontalAxis, verticalAxis) * BASE_FORCE);

        if(Math.Abs(horizontalAxis) > Math.Abs(verticalAxis)) 
        {
            if(horizontalAxis > 0)
            {
                faceDirection = FaceDirection.Right;
            }
            else
            {
                faceDirection = FaceDirection.Left;
            }
        } else {
            if(verticalAxis > 0)
            {
                faceDirection = FaceDirection.Up;
            }
            else
            {
                faceDirection = FaceDirection.Down;
            }
        }
    }
}