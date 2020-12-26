using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardSystem/Cards/WeaponCard", order = 1)]
public class WeaponCardSO : CardSO
{
    [Header("Weapon-Card Specific Data")]
    [SerializeField] private GameObject _projPrefab = null;

    public GameObject ProjectilePrefab => _projPrefab;
}
