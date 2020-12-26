using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ProjectileBehavior
{
    straight = 0,
    sway = 1,
    spiral = 2,
    zigzag = 3
}

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/ProjectileData", order = 2)]
public class ProjectileData : ScriptableObject
{
 #region Private Variables
    /* --------------------------------------- */
    /* --------------- Private --------------- */
    /* --------------------------------------- */
    [Header("General Stats")]
    [SerializeField] private Sprite projSprite = null;
    
    [SerializeField] private ProjectileBehavior _behaviorType;
    
    [SerializeField, Range(0f, 100f)] private float speed = 0f;
    
    [SerializeField] private int projDamage = 0;
    
    [SerializeField, Tooltip("Lifetime of the projectile in seconds.")] 
    private float projLifetime = 0f;

    [Header("Collider Data")]
    [SerializeField] private Vector2 colliderOffset = Vector2.zero;
    [SerializeField] private Vector2 colliderSize = Vector2.zero;
#endregion


#region Public Variables
    /* --------------------------------------- */
    /* ---------------- Public --------------- */
    /* --------------------------------------- */
    public Sprite ProjectileSprite { get { return projSprite; } }
    public ProjectileBehavior BehaviorType { get { return _behaviorType; } }
    public float Speed { get { return speed; } }
    public int ProjectileDamage { get { return projDamage; } }
    public float ProjectileLifetime { get { return projLifetime; } }
    public Vector2 ColliderOffset { get { return colliderOffset; } }
    public Vector2 ColliderSize { get { return colliderSize; } }
#endregion
}
