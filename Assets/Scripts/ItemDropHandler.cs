using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class ItemDropHandler : MonoBehaviour
{
    [Header("Drop Data")]
    public CircleCollider2D _col = null;
    [Expandable] public CardSO cardData = null;
    [OnValueChanged("OnRadiusValueChanged")] public float colliderRadius = 0f;

    [Header("Item Events")]
    [Required] public VoidEventChannelSO pickupEvent;
    [Required] public CardSOEvent grantPlayerCardEvent;

    [Header("Debug Data")]
    [ReadOnly] public ItemType cardType;
    [SerializeField, Tag] private string tagToDetect = "";

#region Unity Functions
    void Awake()
    {
        if (_col == null && GetComponent<CircleCollider2D>() != null)
            _col = GetComponent<CircleCollider2D>();
    }

    void OnEnable()
    {
        if (pickupEvent != null)
        {
            pickupEvent.OnEventRaised += DestoryEntity;
        }
        if (grantPlayerCardEvent != null)
        {
            // grantPlayerCardEvent.OnEventRaised += 
        }
    }
    void OnDisable()
    {
        if (pickupEvent != null)
        {
            pickupEvent.OnEventRaised -= DestoryEntity;
        }
        if (grantPlayerCardEvent != null)
        {
            // grantPlayerCardEvent.OnEventRaised -= 
        }
    }

    void Start()
    {
        cardType = cardData.CardType;
    }

    // void Update() {}
    // void FixedUpdate() {}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == tagToDetect)
        {
            if (pickupEvent != null)
                pickupEvent.RaiseEvent();
        }
    }

    // void OnTriggerExit2D(Collider2D collider) {}
#endregion

#region Class Functions
    private void DestoryEntity()
    {
        if (grantPlayerCardEvent != null)
            grantPlayerCardEvent.RaiseEvent(cardData);
        Destroy(this.gameObject);
    }

    private void OnRadiusValueChanged()
    {
        if (_col == null)
            return;

        _col.radius = colliderRadius;
    }
#endregion
}
