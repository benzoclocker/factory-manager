using Infrastructure.Container;
using UnityEngine;

namespace Services.Providers.AssetProvider
{
    public interface IAssetProvider : IService
    {
        GameObject GetPlayerPrefab();
        GameObject GetFirstFactoryPrefab();
        GameObject GetSecondFactoryPrefab();
        GameObject GetThirdFactoryPrefab();
        GameObject GetGroundPrefab();
        GameObject GetControllerCanvasPrefab();
        GameObject GetCameraPrefab();
    }
}