using UnityEngine;

namespace Services.Providers.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        private const string PlayerPrefabPath = "Prefabs/Player";
        private const string FirstFactoryPrefabPath = "Prefabs/Factory/FirstFactory";
        private const string SecondFactoryPrefabPath = "Prefabs/Factory/SecondFactory";
        private const string ThirdFactoryPrefabPath = "Prefabs/Factory/ThirdFactory";
        private const string GroundPrefabPath = "Prefabs/Ground";
        private const string ControllerCanvasPrefabPath = "Prefabs/UI/ControllerCanvas";
        private const string CameraPrefabPath = "Prefabs/Main Camera";
        
        public GameObject GetPlayerPrefab() => 
            Resources.Load<GameObject>(PlayerPrefabPath);

        public GameObject GetFirstFactoryPrefab() => 
            Resources.Load<GameObject>(FirstFactoryPrefabPath);

        public GameObject GetSecondFactoryPrefab() => 
            Resources.Load<GameObject>(SecondFactoryPrefabPath);

        public GameObject GetThirdFactoryPrefab() => 
            Resources.Load<GameObject>(ThirdFactoryPrefabPath);

        public GameObject GetGroundPrefab() => 
            Resources.Load<GameObject>(GroundPrefabPath);

        public GameObject GetControllerCanvasPrefab() => 
            Resources.Load<GameObject>(ControllerCanvasPrefabPath);

        public GameObject GetCameraPrefab() => 
            Resources.Load<GameObject>(CameraPrefabPath);
    }
}