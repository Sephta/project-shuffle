using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;


public class UIHandManager : MonoBehaviour
{
#region Event Channels
    // EVENT CHANNELS
    [Foldout("Event Channels")] public CardEventChannelSO CardDataPassthrough;
    [Foldout("Event Channels")] public CardIntEventChannelSO UpdateCardDataEvent;
#endregion


    [Header("Dependencies")]
    [Required] public GameObject _cardPrefab = null;
    [SerializeField, ReadOnly] private UICardManager _cardManagerPrefabRefr = null;

    [Header("Configurable Data")]
    [SerializeField] private List<GameObject> _cards = new List<GameObject>();


#region Unity Functions
    void Awake()
    {
        if (_cardPrefab != null)
            _cardManagerPrefabRefr = _cardPrefab.GetComponent<UICardManager>();
    }

    void OnEnable()
    {
        if (CardDataPassthrough != null)
        {
            CardDataPassthrough.OnEventRaised += AddCardToHand;
        }
    }

    void OnDisable()
    {
        if (CardDataPassthrough != null)
        {
            CardDataPassthrough.OnEventRaised -= AddCardToHand;
        }
    }

    // void Start() {}
    // void Update() {}
    // void FixedUpdate() {}
#endregion

#region Class Methods

    // public void AddCardToHand() {}
    public void AddCardToHand(CardSO newCard)
    {
        if (newCard == null)
            return;

        GameObject refr = Instantiate(_cardPrefab, gameObject.transform.position,
                                      gameObject.transform.rotation, gameObject.transform);

        // Send card data through event channel of the new card
        if (UpdateCardDataEvent != null)
            UpdateCardDataEvent.RaiseEvent(newCard, refr.GetInstanceID());

        // Add new card to "hand" list
        _cards.Add(refr);

        UpdateCardLayout();
    }

    private void UpdateCardLayout()
    {

    }

#endregion
}
