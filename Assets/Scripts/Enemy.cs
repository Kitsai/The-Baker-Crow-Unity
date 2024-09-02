using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Moving,
        Attacking,
        Idling,
        Damaged,
    }
}
