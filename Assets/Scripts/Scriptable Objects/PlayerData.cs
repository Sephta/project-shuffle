using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
#region Private Variables
    /* --------------------------------------- */
    /* --------------- Private --------------- */
    /* --------------------------------------- */
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField, Range(0f, 1f)] private float colliderRadius = 0f;
    [SerializeField] private Vector2 colliderPos = Vector2.zero;
#endregion


#region Public Variables
    /* --------------------------------------- */
    /* ---------------- Public --------------- */
    /* --------------------------------------- */
    public float MoveSpeed { get { return moveSpeed; } }
    public float ColliderRadius { get { return colliderRadius; } }
    public Vector2 ColliderPosition { get { return colliderPos; } }
#endregion
}
