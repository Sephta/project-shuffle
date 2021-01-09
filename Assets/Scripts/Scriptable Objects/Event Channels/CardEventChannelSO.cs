using UnityEngine;


/// <summary>
/// An event with one arguement of type CardSO. Used to grant player new cards.
/// </summary>
[CreateAssetMenu(fileName = "CardEvent", menuName = "ScriptableObjects/Events/CardEvent", order = 2)]
public class CardEventChannelSO : GenericEventChanelSO<CardSO> {}
