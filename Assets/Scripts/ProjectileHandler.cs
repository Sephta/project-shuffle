using UnityEngine;
using Lean.Pool;
using NaughtyAttributes;


/// <summary>
/// Monobehavior for simulating projectile data, requires use of LeanPool
/// </summary>
public class ProjectileHandler : MonoBehaviour, IPoolable
{
    public IntEventChannelSO UpdateProjectileData;

    [Header("References")]
    public ProjectileData _projData = null;
    public Rigidbody2D _rb = null;
    public CapsuleCollider2D _col = null;
    public SpriteRenderer _sr = null;

    [Header("Simulated Data")]
    [ReadOnly] public float speed = 0f;
    [ReadOnly] public float lifeTime = 0f;

    [Header("Debug Data")]
    [SerializeField, ReadOnly] private bool active = false;
    [SerializeField, ReadOnly] private float currLifetime = 0f;


#region Unity Functions
    void Awake()
    {
        if (_col == null && GetComponent<CapsuleCollider2D>() != null)
            _col = GetComponent<CapsuleCollider2D>();
        
        if (_rb == null && GetComponent<Rigidbody2D>() != null)
            _rb = GetComponent<Rigidbody2D>();
        
        if (_sr == null && GetComponent<SpriteRenderer>() != null)
            _sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        if (UpdateProjectileData != null)
        {
            UpdateProjectileData.OnEventRaised += InitializeProjectileData;
        }
    }

    void OnDisable()
    {
        if (UpdateProjectileData != null)
        {
            UpdateProjectileData.OnEventRaised -= InitializeProjectileData;
        }
    }

    // void Start() {}

    void Update()
    {
        if (!active)
            return;

        TickLifetimer();
    }

    void FixedUpdate()
    {
        if (!active)
            return;

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
        // InitializeProjectileData();
    }

    void IPoolable.OnDespawn()
    {
        ResetProjectileData();
    }
#endregion

#region Class Methods
    /// <summary> Initializes this projectile using current ProjectileData. </summary>
    private void InitializeProjectileData()
    {
        if (_projData == null)
        {
            Debug.LogError("ERROR! reference to projectile data on this projectile is null.");
            return;
        }

        // Set the sprite of this projectile
        if (_sr != null)
            _sr.sprite = _projData.ProjectileSprite;

        // Modify the name of the projectile
        gameObject.name = gameObject.name + "_" + _projData.name;

        // Modify stats of this projectile
        speed = _projData.Speed;
        currLifetime = lifeTime = _projData.ProjectileLifetime;

        // Change collider data associated with this projectile
        if (_col != null)
        {
            _col.offset = _projData.ColliderOffset;
            _col.size = _projData.ColliderSize;
        }
    }

    /// <summary> Initializes this projectile, but checks against unique ID. Used with custom event system. </summary>
    private void InitializeProjectileData(int idToCheck)
    {
        if (idToCheck != gameObject.GetInstanceID())
            return;

        if (_projData == null)
        {
            Debug.LogError("ERROR! reference to projectile data on this projectile is null.");
            return;
        }

        // Set the sprite of this projectile
        if (_sr != null)
            _sr.sprite = _projData.ProjectileSprite;

        // Modify the name of the projectile
        gameObject.name = gameObject.name + "_" + _projData.name;

        // Modify stats of this projectile
        speed = _projData.Speed;
        currLifetime = lifeTime = _projData.ProjectileLifetime;

        // Change collider data associated with this projectile
        if (_col != null)
        {
            _col.offset = _projData.ColliderOffset;
            _col.size = _projData.ColliderSize;
        }

        active = true;
    }

    private void ResetProjectileData()
    {
        currLifetime = lifeTime;
        gameObject.name = "Projectile";

        active = false;
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
