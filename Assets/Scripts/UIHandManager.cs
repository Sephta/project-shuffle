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
    [Required] public Canvas uiCanvas = null;
    [Required] public GameObject _cardPrefab = null;
    [SerializeField, ReadOnly] private UICardManager _cardManagerPrefabRefr = null;

    [Header("Configurable Data")]
    public GameObject _currentlySelected = null;
    [SerializeField] private GameObject _previouslySelected = null;
    [SerializeField] private List<GameObject> _cards = new List<GameObject>();
    [SerializeField, Range(0f, 180f), OnValueChanged("UpdateCardLayout")] private float cardRotationMax = 0f;
    [SerializeField, Range(0f, 100f), OnValueChanged("UpdateCardLayout")] private float cardHeightMax = 0f;
    [SerializeField, Range(0f, 2f), OnValueChanged("UpdateCardLayout")] private float cardOffsetFactor = 0f;


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

    void Start()
    {
        if (_cards.Count > 0)
        {
            UpdateCardLayout();
            _currentlySelected = _cards[0]; 
            _previouslySelected = _cards[0];  
        }
    }

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

    [Button]
    private void UpdateCardLayout()
    {
        // return if nothing to update
        if (_cards.Count <= 0)
            return;

        // for (int i = 0; i < _cards.Count; i++)
        // {
        //     RectTransform cardTransform = _cards[i].GetComponent<RectTransform>();

        //     float dx = 0f, dy = 0f, dz_rotation = 0f;

        //     if (i < (_cards.Count / 2))  // left half of list
        //     {
        //         dx = -((cardTransform.rect.width * uiCanvas.scaleFactor) * cardOffsetFactor * ((_cards.Count / 2) - (i%(_cards.Count / 2))));
        //         dz_rotation = (cardRotationMax / _cards.Count);
        //     }

        //     else if (i > (_cards.Count / 2))  // right half of list
        //     {
        //         dx = ((cardTransform.rect.width * uiCanvas.scaleFactor) * cardOffsetFactor * ((_cards.Count / 2) - (i%(_cards.Count / 2))));
        //         dz_rotation = -(cardRotationMax / _cards.Count);
        //     }

        //     if (_cards.Count%2 == 0 && i >= (_cards.Count / 2))
        //     {
        //         dx = ((cardTransform.rect.width * uiCanvas.scaleFactor) * cardOffsetFactor * ((_cards.Count / 2) - (i%(_cards.Count / 2))));
        //         dz_rotation = -(cardRotationMax / _cards.Count);
        //     }

        //     cardTransform.localPosition = new Vector3(dx,
        //                                          dy,
        //                                          0f);
            
        //     cardTransform.localEulerAngles = new Vector3(0f, 0f, dz_rotation);
        // }


        // If there are an even number of cards
        if (_cards.Count%2 == 0)
        {

        }
        else // there are an odd number of cards
        {
            int centerIndex = (_cards.Count / 2);

            for (int i = 0; i < _cards.Count; i++)
            {
                RectTransform cardTransform = _cards[i].GetComponent<RectTransform>();
                float dx = 0f, dy = 0f, dz_rotation = 0f;

                // Left side of hand
                if (i < centerIndex)
                {
                    dx = -((cardTransform.rect.width * uiCanvas.scaleFactor) * cardOffsetFactor * Mathf.Abs(centerIndex - i));
                    dz_rotation = (cardRotationMax / _cards.Count);
                }

                // Right side of hand
                else if (i > centerIndex)
                {
                    dx = ((cardTransform.rect.width * uiCanvas.scaleFactor) * cardOffsetFactor * Mathf.Abs(centerIndex - i));
                    dz_rotation = -(cardRotationMax / _cards.Count);
                }

                // Center card of hand
                else
                {

                }

                cardTransform.localPosition = new Vector3(dx,
                                                 dy,
                                                 0f);
            
                cardTransform.localEulerAngles = new Vector3(0f, 0f, dz_rotation);
            }
        }

        // _currentlySelected.transform.localPosition = new Vector3(_currentlySelected.transform.localPosition.x,
        //                                                         112f,
        //                                                         _currentlySelected.transform.localPosition.z);
        // _currentlySelected.transform.SetSiblingIndex(_cards.Count - 1);
        // _currentlySelected.transform.localEulerAngles = Vector3.zero;
    }

#endregion
}
