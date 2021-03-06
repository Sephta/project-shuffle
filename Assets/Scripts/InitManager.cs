﻿using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;


/// <summary>
/// Loads the Initialization Scene so that needed system dependencies can be accessed from any other scene in the editor
/// </summary>
public class InitManager : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Scene Data")]
    [Scene] public string sceneNameToLoad = "";
	[Scene] public string testAdditiveLoad = "";


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
		SceneManager.LoadSceneAsync(testAdditiveLoad, LoadSceneMode.Additive);
    }
#endif
}
