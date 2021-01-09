using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// An event channel that takes no arguements. Used for communication between systems
/// (Example: Exit Game event)
/// </summary>
[CreateAssetMenu(fileName = "VoidEvent", menuName = "ScriptableObjects/Events/VoidEvent", order = 1)]
public class VoidEventChannelSO : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField] private bool debugLogWhenEventRaised = false;
#endif

    public UnityAction OnEventRaised;

    public void RaiseEvent()
	{
#if UNITY_EDITOR
        if (debugLogWhenEventRaised)
            Debug.Log("Generic Event \"" + this.name + "\" has been Invoked.");
#endif
		OnEventRaised?.Invoke();
	}
}
