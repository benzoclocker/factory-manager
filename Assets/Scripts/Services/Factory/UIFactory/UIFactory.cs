using Services.Providers.AssetProvider;
using UnityEngine;

namespace Services.Factory.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        public UIFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public GameObject CreateControllerUI()
        {
            return Object.Instantiate(_assetProvider.GetControllerCanvasPrefab());
        }
    }
}