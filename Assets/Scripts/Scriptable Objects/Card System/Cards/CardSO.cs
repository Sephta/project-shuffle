using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


#region EnumTypes
public enum ItemType
{
    weapon = 0,
    consumable = 1,
    ai = 2
}
#endregion


// [CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardSystem/CardAsset", order = 3)]
public class CardSO : ScriptableObject
{
    [HorizontalLine(color: EColor.Black)]
    [Header("Card Specific Data")]
    
    [SerializeField] private ItemType _type = (ItemType)0;
    
    [SerializeField, ShowAssetPreview(16, 16)]
    private Sprite _sprite = null;


    public ItemType CardType => _type;
    public Sprite CardSprite => _sprite;
}
