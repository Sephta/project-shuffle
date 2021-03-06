﻿using UnityEngine;
using NaughtyAttributes;
using MilkShake;


[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardSystem/Cards/WeaponCard", order = 1)]
public class WeaponCardSO : CardSO
{
    [HorizontalLine(color: EColor.Black)]
    [Header("Weapon Specific Data")]
    
    [SerializeField] private GameObject _projPrefab = null;
    [SerializeField, Expandable] private ProjectileData _projData = null;

    [SerializeField, Range(0f, 1f), Tooltip("This var describes the rate of fire as: projectiles per second.")]
    private float _rof = 0f;

    [SerializeField, Range(0f, 50000f)] private float _knockBackMagnitude = 0f;
    [SerializeField, MinMaxSlider(-1f, 1f)] private Vector2 _knockBackX = Vector2.zero;
    [SerializeField, MinMaxSlider(-1f, 1f)] private Vector2 _knockBackY = Vector2.zero;
    
    public VoidEventChannelSO _knockbackEvent;
    [Expandable] public ShakePreset _camShakePreset;

    public GameObject ProjectilePrefab => _projPrefab;
    public ProjectileData ProjectileData => _projData;
    public float ROF => _rof;
    
    public Vector2 KnockBackDir
    {
        get
        {
            return new Vector2(Random.Range(_knockBackX.x, _knockBackX.y), Random.Range(_knockBackY.x, _knockBackY.y));
        }
    }

    public float KnockBackMagnitude => _knockBackMagnitude;
}
