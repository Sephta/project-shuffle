using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockLocalYPosition : MonoBehaviour
{
    [Header("Configurable Data")]
    [SerializeField] private float lockPositionAt = 0f;

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y < 0f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
                                                  lockPositionAt,
                                                  transform.localPosition.z);
        }
    }
}
