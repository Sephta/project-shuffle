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
    [SerializeField, Range(0f, 100f)] private float speed = 0f;
#endregion


#region Public Variables
    /* --------------------------------------- */
    /* ---------------- Public --------------- */
    /* --------------------------------------- */
    public float Speed { get { return speed; } }
#endregion
}
