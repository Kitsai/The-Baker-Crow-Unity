using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _time = 0.0f;
    public float Time => _time;

    // Update is called once per frame
    void Update()
    {
       _time += UnityEngine.Time.deltaTime; 
    }

    public void ResetTime()
    {
        _time = 0.0f;
    }
}
