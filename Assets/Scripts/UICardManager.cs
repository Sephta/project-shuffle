using UnityEngine;
using TMPro;
using NaughtyAttributes;


/// <summary>
/// Class for handling data to be displayed on a given card UI prefab.
/// </summary>
public class UICardManager : MonoBehaviour
{
#region Event Channels
    // EVENT CHANNELS
    [Foldout("Event Channels")] public CardIntEventChannelSO UpdateCardDataEvent;
#endregion


    [Header("Configurable Data")]
    public CardSO _cardData;

    [Header("Card UI References")]
    [SerializeField] private TextMeshProUGUI cardName = null;


#region Unity Functions
    // void Awake() {}

    void OnEnable()
    {
        if (UpdateCardDataEvent != null)
        {
            UpdateCardDataEvent.OnEventRaised += UpdateCardData;
        }
    }
    
    void OnDisable()
    {
        if (UpdateCardDataEvent != null)
        {
            UpdateCardDataEvent.OnEventRaised -= UpdateCardData;
        }
    }

    void Start()
    {
        if (_cardData != null)
            UpdateCardData(_cardData, gameObject.GetInstanceID());
    }

    // void Update() {}
    // void FixedUpdate() {}
#endregion

#region Class Methods
    private void UpdateCardData(CardSO cardData, int idToCheck)
    {
        if (cardData == null)
            return;

        if (idToCheck != gameObject.GetInstanceID())
            return;

        _cardData = cardData;

        if (cardName != null)
        {
            gameObject.name = "Card - " + _cardData.name;
            cardName.text = _cardData.name;
        }
    }
#endregion
}
