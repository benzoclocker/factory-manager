using Core;
using Core.UI;
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

        public IAlert Alert { get; private set; }

        public GameObject CreateControllerUI()
        {
            return Object.Instantiate(_assetProvider.GetControllerCanvasPrefab());
        }

        public GameObject CreateAlertUI()
        {
            GameObject alertUI = Object.Instantiate(_assetProvider.GetAlertUIPrefab());
            AlertUI component = alertUI.GetComponent<AlertUI>();
            Alert = new Alert(component);
            return alertUI;
        }
    }
}