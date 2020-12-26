using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class ItemDropHandler : MonoBehaviour
{
    [Header("Drop Data")]
    public CardSO cardData = null;

    [Header("Debug Data")]
    [ReadOnly] public ItemType cardType;


    // void Awake() {}

    void Start()
    {
        cardType = cardData.CardType;
    }

    // void Update() {}
    // void FixedUpdate() {}
}
