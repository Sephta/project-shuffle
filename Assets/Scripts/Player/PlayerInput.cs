// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


/// <summary>
/// Handles player input delegates
/// </summary>
public class PlayerInput : MonoBehaviour
{
    [Header("Input Events")]
    public VoidEventChannelSO PlayerMoveEvent;
    public VoidEventChannelSO PlayerAttack01Event;


#region Unity Functions
    // void Awake() {}

    void OnEnable()
    {
        if (PlayerMoveEvent != null)
        {
            // PlayerMoveEvent.OnEventRaised += ;
        }
        if (PlayerAttack01Event != null)
        {
            // PlayerAttack01Event.OnEventRaised += ;
        }
    }

    void OnDisable()
    {
        if (PlayerMoveEvent != null)
        {
            // PlayerMoveEvent.OnEventRaised -= ;
        }
        if (PlayerAttack01Event != null)
        {
            // PlayerAttack01Event.OnEventRaised -= ;
        }
    }

    // void Start() {}
    // void Update() {}
#endregion
}
