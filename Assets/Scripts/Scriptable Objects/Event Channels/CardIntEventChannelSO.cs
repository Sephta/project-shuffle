using UnityEngine;


/// <summary>
/// An event with two arguements of type CardSO and int. Used to grant player new cards.
/// </summary>
[CreateAssetMenu(fileName = "CardIntEvent", menuName = "ScriptableObjects/Events/CardIntEvent")]
public class CardIntEventChannelSO : GenericEventChanelSO<CardSO, int> {}
