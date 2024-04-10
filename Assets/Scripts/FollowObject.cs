using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{

    public GameObject go;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(go.transform.position.x, go.transform.position.y, -100);
    }
}
