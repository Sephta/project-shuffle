using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    weapon = 0,
    consumable = 1,
    ai = 2
}


// [CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardSystem/CardAsset", order = 3)]
public class CardSO : ScriptableObject
{
    [Header("Card Specific Data")]
    [SerializeField] private ItemType _type = (ItemType)0;
    [SerializeField] private Sprite _sprite = null;

    public ItemType CardType => _type;
    public Sprite CardSprite => _sprite;
}
