using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Loads the Initialization Scene so that needed system dependencies can be accessed from any other scene in the editor
/// </summary>
public class InitManager : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Scene Data")]
    public string sceneNameToLoad = "";

    void Awake()
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			if (scene.name == sceneNameToLoad)
			{
				return;
			}
		}
		SceneManager.LoadSceneAsync(sceneNameToLoad, LoadSceneMode.Additive);
    }
#endif
}
