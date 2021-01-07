// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using NaughtyAttributes;


public class PlayerController : MonoBehaviour
{
    // COMPONENT REFERENCES
    [Foldout("Components")] public GameObject _child = null;
    [Foldout("Components")] public Rigidbody2D _rb = null;
    [Foldout("Components")] public Collider2D _col = null;
    [Foldout("Components")] public Animator _anim = null;

    // EVENT CHANNELS
    [Foldout("Event Channels")] public VoidEventChannelSO KnockbackEvent;
    [Foldout("Event Channels")] public CardSOEvent RecieveCardEvent;
    [Foldout("Event Channels")] public VoidEventChannelSO EquipEvent;
    [Foldout("Event Channels")] public VoidEventChannelSO UpdatePlayerVisuals;

    [Header("Dependencies")]
    public GameObject _proj = null;
    public GameObject _projParentObject = null;
    [Required] public GameObject _equipmentSlot = null;
    public Transform _projSpawnLocation = null;

    [Header("Configurable Data")]
    [Expandable] public PlayerData pData = null;
    [Expandable] public HandSO pHand = null;

    [Header("Debug Data")]
#if UNITY_EDITOR
    public bool ShowDebugData = false;
    [SerializeField, ReadOnly, ShowIf("ShowDebugData")] private CardSO _currEquip;
#endif

    [ShowIf("ShowDebugData"), Tooltip("Will allow collider data to be changed at runtime.")]
    public bool changeColliderData = false;
    [SerializeField, ReadOnly, ShowIf("ShowDebugData")] private SpriteRenderer _equipedVisuals = null;
    [SerializeField, ReadOnly, ShowIf("ShowDebugData")] public Vector2 direction = Vector2.zero;    [SerializeField, Range(0f, 0.25f), ShowIf("ShowDebugData")] public float attackDownTime = 0.5f;
    [SerializeField, ReadOnly, ShowIf("ShowDebugData")] private float downTime = 0f;
    [SerializeField, ReadOnly, ShowIf("ShowDebugData")] private float timeSinceLastAttack = 0f;


#region Unity Functions
    void Awake()
    {
        if (_projParentObject == null && GameObject.Find("~Projectiles") != null)
            _projParentObject = GameObject.Find("~Projectiles");

        if (_child == null)
            _child = transform.GetChild(0).gameObject;

        if (_rb == null && GetComponent<Rigidbody2D>() != null)
            _rb = GetComponent<Rigidbody2D>();
        
        if (_col == null && GetComponent<CircleCollider2D>() != null)
            _col = GetComponent<CircleCollider2D>();
        
        if (_anim == null && _child.GetComponent<Animator>() != null)
            _anim = _child.GetComponent<Animator>();
        
        if (_equipmentSlot != null)
        {
            if (_equipedVisuals == null && _equipmentSlot.GetComponent<SpriteRenderer>() != null)
                _equipedVisuals = _equipmentSlot.GetComponent<SpriteRenderer>();
        }
    }

    void OnEnable()
    {
        if (KnockbackEvent != null)
        {
            KnockbackEvent.OnEventRaised += ApplyKnockback;
        }
        if (RecieveCardEvent != null)
        {
            RecieveCardEvent.OnEventRaised += AddCardToHand;
        }
        if (EquipEvent != null)
        {
            EquipEvent.OnEventRaised += EquipItem;
        }
        if (UpdatePlayerVisuals != null)
        {
            UpdatePlayerVisuals.OnEventRaised += RefreshEquipmentVisuals;
        }
    }
    
    void OnDisable()
    {
        if (KnockbackEvent != null)
        {
            KnockbackEvent.OnEventRaised -= ApplyKnockback;
        }
        if (RecieveCardEvent != null)
        {
            RecieveCardEvent.OnEventRaised -= AddCardToHand;
        }
        if (EquipEvent != null)
        {
            EquipEvent.OnEventRaised -= EquipItem;
        }
        if (UpdatePlayerVisuals != null)
        {
            UpdatePlayerVisuals.OnEventRaised -= RefreshEquipmentVisuals;
        }

        pHand.ResetHand();
    }

    void Start()
    {
        if (pHand != null)
            pHand.InitHand();

        EquipEvent.RaiseEvent();
    }

    void Update()
    {
        CheckDebugFlags();
        CheckPlayerActions();

        downTime = (Time.time - timeSinceLastAttack);

#if UNITY_EDITOR
        if (pHand != null)
            _currEquip = pHand.CurrentlySelected;
#endif
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
#endregion

#region Custom Functions
    /* --------------------------------------- */
    /* ---------------- Public --------------- */
    /* --------------------------------------- */

    public void FireProjectile()
    {
        if ((Time.time - timeSinceLastAttack) >= attackDownTime
            && pHand.CurrentlySelected != null)
        {
            timeSinceLastAttack = Time.time;
            GameObject refr = LeanPool.Spawn(_proj, _projSpawnLocation.position, _projSpawnLocation.rotation, _projParentObject.transform);
            KnockbackEvent.RaiseEvent();
        }
    }

    public void ApplyKnockback()
    {
        if (pHand.CurrentlySelected == null)
            return;
        
        WeaponCardSO weapon = (pHand.CurrentlySelected.CardType == ItemType.weapon) ? pHand.CurrentlySelected as WeaponCardSO : null;

        Vector3 dir = _projSpawnLocation.transform.position - transform.position;

        if (weapon != null)
            _rb.AddForce(weapon.KnockBackMagnitude * -dir.normalized * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    public void AddCardToHand(CardSO card)
    {
        for (var i = 0; i < pHand.Cards.Count; i++)
        {
            if (pHand.Cards[i] == null)
            {
                pHand.Cards[i] = card;
                pHand.SetCurrentSelect(pHand.Cards[i]);
                EquipEvent.RaiseEvent();
                break;
            }
        }
    }


    /* --------------------------------------- */
    /* --------------- Private --------------- */
    /* --------------------------------------- */

    /// <summary>
    /// Calculates and moves the player if necessary dependencies are available
    /// </summary>
    private void MovePlayer()
    {
        if (_rb == null)
            return;
        if (_col == null)
            return;
        
        if (InputManager._inst != null)
        {
            GetDirectionVector();
            _rb.AddForce(((direction * pData.MoveSpeed * Time.fixedDeltaTime) + _rb.velocity) - _rb.velocity, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Finds and normalizes input direction using custom InputManager system
    /// </summary>
    private void GetDirectionVector()
    {
        if (InputManager._inst == null)
            return;
        
        InputManager iManager = InputManager._inst;

        if (Input.GetKey(iManager._keyBindings[InputAction.moveUp]))
            direction.y = 1.0f;
        else if (Input.GetKey(iManager._keyBindings[InputAction.moveDown]))
            direction.y = -1.0f;
        else
            direction.y = 0;
        if (Input.GetKey(iManager._keyBindings[InputAction.moveLeft]))
            direction.x = -1.0f;
        else if (Input.GetKey(iManager._keyBindings[InputAction.moveRight]))
            direction.x = 1.0f;
        else
            direction.x = 0;
        
        direction = direction.normalized;
    }

    private void CheckDebugFlags()
    {
        if (changeColliderData)
            UpdateColliderData();
    }

    private void UpdateColliderData()
    {
        if (_col == null)
            return;
        
        CircleCollider2D col = _col as CircleCollider2D;

        Vector2 newColOffset = new Vector2(pData.ColliderPosition.x, pData.ColliderPosition.y);

        col.radius = pData.ColliderRadius;
        col.offset = newColOffset;
    }

    private void CheckPlayerActions()
    {
        if (InputManager._inst == null)
            return;
        
        if (Input.GetKey(InputManager._inst._keyBindings[InputAction.attack]))
            FireProjectile();
        if (Input.GetKeyDown(InputManager._inst._keyBindings[InputAction.nextItem]))
            if (pHand != null)
                pHand.NextCard();
        if (Input.GetKeyDown(InputManager._inst._keyBindings[InputAction.prevItem]))
            if (pHand != null)
                pHand.PrevCard();
    }

    /// <summary> Will modify properties of the currently equiped item. </summary>
    private void EquipItem()
    {
        if (_equipedVisuals == null)
            return;
        
        if (pHand.CurrentlySelected == null)
        {
            _equipedVisuals.sprite = null;
            return;
        }

        _equipedVisuals.sprite = pHand.CurrentlySelected.CardSprite;
        
    }

    // ! Currently the same as EquipItem(), but in the future will likely perform less things
    /// <summary> For each visual element of the player, will update visuals based on current data. </summary>
    private void RefreshEquipmentVisuals()
    {
        if (_equipedVisuals == null)
            return;
        
        // if the currently selected item is null, player equipment should display as empty
        if (pHand.CurrentlySelected == null)
        {
            _equipedVisuals.sprite = null;
            return;
        }

        // Otherwise show whats in the players equiped slot
        _equipedVisuals.sprite = pHand.CurrentlySelected.CardSprite;
    }
#endregion
}
