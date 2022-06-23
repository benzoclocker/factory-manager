using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    public class SceneLoader : ISceneLoader
    {
        public void LoadScene(string sceneName, Action onLoaded = null)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.completed += _ => onLoaded?.Invoke();
        }
    }
}