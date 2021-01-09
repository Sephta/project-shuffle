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
    [Foldout("Event Channels")] public IntEventChannelSO UpdateCurrentlySelected;
#endregion


    [Header("Dependencies")]
    [Required] public Canvas uiCanvas = null;
    [Required] public GameObject _cardPrefab = null;
    [SerializeField, ReadOnly] private UICardManager _cardManagerPrefabRefr = null;

    [Header("Configurable Data")]
    public GameObject _currentlySelected = null;
    [SerializeField] private GameObject _previouslySelected = null;
    public int _currIndex = 0;
    [SerializeField] private int _prevIndex = -1;
    [SerializeField] private List<GameObject> _cards = new List<GameObject>();
    [SerializeField, Range(0f, 180f), OnValueChanged("UpdateCardLayout")] private float cardRotationMax = 0f;
    [SerializeField, Range(0f, 2f), OnValueChanged("UpdateCardLayout")] private float cardRotationMagnitude = 0f;
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
        if (UpdateCurrentlySelected != null)
        {
            UpdateCurrentlySelected.OnEventRaised += UpdateCurrSelectedPos;
        }
    }

    void OnDisable()
    {
        if (CardDataPassthrough != null)
        {
            CardDataPassthrough.OnEventRaised -= AddCardToHand;
        }
        if (UpdateCurrentlySelected != null)
        {
            UpdateCurrentlySelected.OnEventRaised -= UpdateCurrSelectedPos;
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
        
        // Update cache of currenly/previously selected cards
        _previouslySelected = _currentlySelected;
        _prevIndex = _currIndex;
        
        _currentlySelected = refr;
        _currIndex = _cards.IndexOf(refr);

        // Update the layout
        UpdateCardLayout();
    }

    [Button]
    private void UpdateCardLayout()
    {
        // return if nothing to update
        if (_cards.Count <= 0)
            return;

        // If there are an even number of cards
        if (_cards.Count%2 == 0)
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
                    dy = -(((cardTransform.rect.width * uiCanvas.scaleFactor) / 4) * (cardHeightMax / _cards.Count) * Mathf.Abs(centerIndex - i));
                    dz_rotation = (Mathf.Abs(centerIndex - i) + (cardRotationMax / _cards.Count)) * cardRotationMagnitude;
                }

                // Right side of hand
                else if (i >= centerIndex)
                {
                    dx = ((cardTransform.rect.width * uiCanvas.scaleFactor) * cardOffsetFactor * Mathf.Abs(centerIndex - i));
                    dy = -(((cardTransform.rect.width * uiCanvas.scaleFactor) / 4) * (cardHeightMax / _cards.Count) * (Mathf.Abs(centerIndex - i) + 1));
                    dz_rotation = -(Mathf.Abs(centerIndex - i) + (cardRotationMax / _cards.Count)) * cardRotationMagnitude;
                }

                cardTransform.localPosition = new Vector3(dx,
                                                 dy,
                                                 0f);
            
                cardTransform.localEulerAngles = new Vector3(0f, 0f, dz_rotation);
            }
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
                    dy = -(((cardTransform.rect.width * uiCanvas.scaleFactor) / 4) * (cardHeightMax / _cards.Count) * Mathf.Abs(centerIndex - i));
                    dz_rotation = (Mathf.Abs(centerIndex - i) + (cardRotationMax / _cards.Count)) * cardRotationMagnitude;
                }

                // Right side of hand
                else if (i > centerIndex)
                {
                    dx = ((cardTransform.rect.width * uiCanvas.scaleFactor) * cardOffsetFactor * Mathf.Abs(centerIndex - i));
                    dy = -(((cardTransform.rect.width * uiCanvas.scaleFactor) / 4) * (cardHeightMax / _cards.Count) * Mathf.Abs(centerIndex - i));
                    dz_rotation = -(Mathf.Abs(centerIndex - i) + (cardRotationMax / _cards.Count)) * cardRotationMagnitude;
                }

                cardTransform.localPosition = new Vector3(dx,
                                                 dy,
                                                 0f);
            
                cardTransform.localEulerAngles = new Vector3(0f, 0f, dz_rotation);
            }
        }

        _currentlySelected.transform.localPosition = new Vector3(_currentlySelected.transform.localPosition.x,
                                                                _currentlySelected.transform.localPosition.y + 112f,
                                                                _currentlySelected.transform.localPosition.z);
        _currentlySelected.transform.SetSiblingIndex(_cards.Count - 1);
        // _currentlySelected.transform.localEulerAngles = Vector3.zero;
        // _currentlySelected.transform.SetSiblingIndex(0);
    }

    private void UpdateCurrSelectedPos(int newCurr)
    {
        if (newCurr < 0 || _cards.Count <= newCurr)
            return;
        
        if (newCurr != _currIndex)
        {
            _previouslySelected = _cards[_currIndex];
            _prevIndex = _currIndex;

            _currentlySelected = _cards[newCurr];
            _currIndex = newCurr;

            UpdateCardLayout();
        }
    }

#endregion
}
