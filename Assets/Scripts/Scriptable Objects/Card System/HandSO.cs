using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HandData", menuName = "ScriptableObjects/CardSystem/HandAsset", order = 2)]
public class HandSO : ScriptableObject
{
    [Header("Hand Specific Data")]
    [SerializeField] private int _maxSize = 0;
    [SerializeField] private List<CardSO> _cards = new List<CardSO>();

    public List<CardSO> Cards => _cards;
    public int MaxSize => _maxSize;
}
