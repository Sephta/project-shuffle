using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


[CreateAssetMenu(fileName = "HandData", menuName = "ScriptableObjects/CardSystem/HandAsset", order = 2)]
public class HandSO : ScriptableObject
{
    [Header("Hand Specific Data")]
    
    [SerializeField] private CardSO _selected = null;
    [SerializeField, ReadOnly] private int _selectedIndex = 0;
    [SerializeField] private int _maxSize = 0;
    [SerializeField] private List<CardSO> _cards = new List<CardSO>();

    [Foldout("Event Channels")] public VoidEventChannelSO PlayerVisuals;
    // [Foldout("Event Channels")] public CardEventChannelSO 

    public List<CardSO> Cards => _cards;
    public int MaxSize => _maxSize;
    public CardSO CurrentlySelected => _selected;


#region Unity Functions
    void OnEneble()
    {
        if (PlayerVisuals != null)
        {
            // PlayerVisuals.OnEventRaised += ;
        }
    }
    
    void OnDisable()
    {
        if (PlayerVisuals != null)
        {
            // PlayerVisuals.OnEventRaised -= ;
        }
    }
#endregion


#region Class Methods

    // Affects whole hand -------------------------------------------------------------------------
    public void InitHand()
    {
        // Find the first card reference that isn't null, if any exists, and make currently selected
        for (var i = 0; i < _cards.Count; i++)
        {
            if (_cards[i] != null)
            {
                _selected = _cards[i];
                _selectedIndex = i;
                break;
            }
        }

        // TODO - any other initialization stuffs.
    }

    /// <summary> Resets this hand by clearing card data. Warning! Will set CurrentlySelected to null. </summary>
    public void ResetHand()
    {
        for (int i = 0; i < _cards.Count; i++)
            _cards[i] = null;
        
        _selected = _cards[0];
        _selectedIndex = 0;

        // Update the player visuals to reflect equipment changes
        if (PlayerVisuals != null)
            PlayerVisuals.RaiseEvent();
    }
    // --------------------------------------------------------------------------------------------

    // Affects one card at a time -----------------------------------------------------------------
    public void NextCard()
    {
        _selectedIndex = (_selectedIndex + 1) % _cards.Count;
        _selected = _cards[_selectedIndex];

        // Update the player visuals to reflect equipment changes
        if (PlayerVisuals != null)
            PlayerVisuals.RaiseEvent();
    }

    public void PrevCard()
    {
        _selectedIndex = (_selectedIndex - 1 + _cards.Count) % _cards.Count;
        _selected = _cards[_selectedIndex];

        // Update the player visuals to reflect equipment changes
        if (PlayerVisuals != null)
            PlayerVisuals.RaiseEvent();
    }

    public void SetCurrentSelect(CardSO newSelection)
    {
        _selected = newSelection;
        _selectedIndex = _cards.IndexOf(newSelection);

        // Update the player visuals to reflect equipment changes
        if (PlayerVisuals != null)
            PlayerVisuals.RaiseEvent();
    }
    // --------------------------------------------------------------------------------------------

#endregion
}
