using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class ItemDropHandler : MonoBehaviour
{
    [Header("Drop Data")]
    [Expandable] public CardSO cardData = null;
    [Required] public VoidEventChannelSO pickupEvent;

    [Header("Debug Data")]
    [ReadOnly] public ItemType cardType;
    [SerializeField, Tag] private string tagToDetect = "";

#region Unity Functions
    // void Awake() {}
    void OnEnable()
    {
        if (pickupEvent != null)
        {
            pickupEvent.OnEventRaised += DestoryEntity;
        }
    }
    void OnDisable()
    {
        if (pickupEvent != null)
        {
            pickupEvent.OnEventRaised -= DestoryEntity;
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
        Destroy(this.gameObject);
    }
#endregion
}
