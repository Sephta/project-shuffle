using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DeckData", menuName = "ScriptableObjects/CardSystem/DeckAsset", order = 1)]
public class DeckSO : ScriptableObject
{
    [Header("Deck Specific Data")]
    
    [SerializeField] private List<CardSO> _cards = new List<CardSO>();

    public List<CardSO> Cards => _cards;
}
