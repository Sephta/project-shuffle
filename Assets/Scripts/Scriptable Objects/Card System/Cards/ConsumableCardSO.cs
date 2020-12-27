using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardSystem/Cards/ConsumableCard", order = 2)]
public class ConsumableCardSO : CardSO
{
    [Header("Consumble Specific Data")]
    [SerializeField] private int _test = 0;
    public int Test => _test;
}
