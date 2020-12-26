using UnityEngine;
using Lean.Pool;
using NaughtyAttributes;


/// <summary>
/// Monobehavior for simulating projectile data, requires use of LeanPooling
/// </summary>
public class ProjectileHandler : MonoBehaviour, IPoolable
{
    [Header("References")]
    public ProjectileData _projData = null;
    public Rigidbody2D _rb = null;
    public CapsuleCollider2D _col = null;

    [Header("Simulated Data")]
    [ReadOnly] public float speed = 0f;
    [ReadOnly] public float lifeTime = 0f;

    [Header("Debug Data")]
    [SerializeField, ReadOnly] private float currLifetime = 0f;


#region Unity Functions
    void Awake()
    {
        if (_col == null && GetComponent<CapsuleCollider2D>() != null)
            _col = GetComponent<CapsuleCollider2D>();
        
        if (_rb == null && GetComponent<Rigidbody2D>() != null)
            _rb = GetComponent<Rigidbody2D>();
    }

    // void Start() {}

    void Update()
    {
        TickLifetimer();
    }

    void FixedUpdate()
    {
        switch (_projData.BehaviorType)
        {
            case ProjectileBehavior.straight:
                MovementBehavior_straight();
                break;
        }
    }
#endregion

#region IPoolable Functions
    void IPoolable.OnSpawn()
    {
        InitializeProjectileData();
    }

    void IPoolable.OnDespawn()
    {
        ResetProjectileData();
    }
#endregion

#region ProjectileHandler Methods
    private void InitializeProjectileData()
    {
        speed = _projData.Speed;
        currLifetime = lifeTime = _projData.ProjectileLifetime;

        if (_col != null)
        {
            _col.offset = _projData.ColliderOffset;
            _col.size = _projData.ColliderSize;
        }
    }

    private void ResetProjectileData()
    {
        currLifetime = lifeTime;
    }

    private void MovementBehavior_straight()
    {
        if (_rb == null)
            return;
        _rb.velocity = transform.right * speed*10 * Time.fixedDeltaTime;
    }

    private void TickLifetimer()
    {
        currLifetime -= Time.deltaTime;
        currLifetime = Mathf.Clamp(currLifetime, 0, lifeTime);

        if (currLifetime <= 0)
            LeanPool.Despawn(this.gameObject, 0f);
    }
#endregion
}
