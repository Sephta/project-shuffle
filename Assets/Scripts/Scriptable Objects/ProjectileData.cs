using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/ProjectileData", order = 2)]
public class ProjectileData : ScriptableObject
{
 #region Private Variables
    /* --------------------------------------- */
    /* --------------- Private --------------- */
    /* --------------------------------------- */
    [SerializeField] private Sprite projSprite = null;
    
    [SerializeField, Range(0f, 100f)] private float speed = 0f;
    
    [SerializeField] private int projDamage = 0;
    
    [SerializeField, Tooltip("Lifetime of the projectile in seconds.")] 
    private float projLifetime = 0f;
#endregion


#region Public Variables
    /* --------------------------------------- */
    /* ---------------- Public --------------- */
    /* --------------------------------------- */
    public Sprite ProjectileSprite { get { return projSprite; } }
    public float Speed { get { return speed; } }
    public int ProjectileDamage { get { return projDamage; } }
    public float ProjectileLifetime { get { return projLifetime; } }
#endregion
}
