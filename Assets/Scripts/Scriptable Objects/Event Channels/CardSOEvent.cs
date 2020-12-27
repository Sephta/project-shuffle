using UnityEngine;


/// <summary>
/// An event with one arguement of type CardSO. Used to grant player new cards.
/// </summary>
[CreateAssetMenu(fileName = "CardSOEvent", menuName = "ScriptableObjects/Events/CardSOEvent", order = 2)]
public class CardSOEvent : GenericEventChanelSO<CardSO> {}
