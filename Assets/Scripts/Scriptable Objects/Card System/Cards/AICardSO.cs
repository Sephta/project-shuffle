using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardSystem/Cards/AICard", order = 3)]
public class AICardSO : CardSO
{
    [Header("AI Specific Data")]
    [SerializeField] private int _test = 0;
    public int Test => _test;
}
