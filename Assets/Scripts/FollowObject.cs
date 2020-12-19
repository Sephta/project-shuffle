using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Header("Object to follow")]
    public Transform toFollow = null;

    [Header("Follow Data")]
    public float followSpeed = 0f;

    void Update()
    {
        // Basically keeps this object attatched to the follow object
        transform.position = toFollow.position;
    }
}
