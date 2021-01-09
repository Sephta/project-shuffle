using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Generic typed single-variable event channel.
/// </summary>
public class GenericEventChanelSO<T> : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField] private bool debugLogWhenEventRaised = false;
#endif


    public UnityAction<T> OnEventRaised;

    public void RaiseEvent(T var)
    {
#if UNITY_EDITOR
        if (debugLogWhenEventRaised)
            Debug.Log("Generic Event \"" + this.name + "\" has been Invoked.");
#endif
        OnEventRaised?.Invoke(var);
    }
}


/// <summary>
/// Generic typed two-variable event channel.
/// </summary>
public class GenericEventChanelSO<TOne, TTwo> : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField] private bool debugLogWhenEventRaised = false;
#endif

    public UnityAction<TOne, TTwo> OnEventRaised;

    public void RaiseEvent(TOne var1, TTwo var2)
    {
#if UNITY_EDITOR
        if (debugLogWhenEventRaised)
            Debug.Log("Generic Event \"" + this.name + "\" has been Invoked.");
#endif
        OnEventRaised?.Invoke(var1, var2);
    }
}


/// <summary>
/// Generic typed three-variable event channel.
/// </summary>
public class GenericEventChanelSO<TOne, TTwo, TThree> : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField] private bool debugLogWhenEventRaised = false;
#endif

    public UnityAction<TOne, TTwo, TThree> OnEventRaised;

    public void RaiseEvent(TOne var1, TTwo var2, TThree var3)
    {
#if UNITY_EDITOR
        if (debugLogWhenEventRaised)
            Debug.Log("Generic Event \"" + this.name + "\" has been Invoked.");
#endif
        OnEventRaised?.Invoke(var1, var2, var3);
    }
}


/// <summary>
/// Generic typed four-variable event channel.
/// </summary>
public class GenericEventChanelSO<TOne, TTwo, TThree, TFour> : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField] private bool debugLogWhenEventRaised = false;
#endif

    public UnityAction<TOne, TTwo, TThree, TFour> OnEventRaised;

    public void RaiseEvent(TOne var1, TTwo var2, TThree var3, TFour var4)
    {
#if UNITY_EDITOR
        if (debugLogWhenEventRaised)
            Debug.Log("Generic Event \"" + this.name + "\" has been Invoked.");
#endif
        OnEventRaised?.Invoke(var1, var2, var3, var4);
    }
}
