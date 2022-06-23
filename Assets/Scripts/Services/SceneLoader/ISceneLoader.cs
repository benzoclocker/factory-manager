using System;
using Infrastructure.Container;

namespace Services.SceneLoader
{
    public interface ISceneLoader : IService
    {
        void LoadScene(string sceneName, Action onLoaded = null);
    }
}