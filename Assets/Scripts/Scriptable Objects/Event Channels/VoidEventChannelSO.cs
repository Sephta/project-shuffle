using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// An event channel that takes no arguements. Used for communication between systems
/// (Example: Exit Game event)
/// </summary>
[CreateAssetMenu(fileName = "VoidEvent", menuName = "ScriptableObjects/Events/VoidEvent", order = 1)]
public class VoidEventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
	{
		Debug.Log("Void Event \"" + this.name + "\" has been Invoked.");
		OnEventRaised?.Invoke();
	}
}
