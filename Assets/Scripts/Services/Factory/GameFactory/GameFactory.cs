using System.Collections.Generic;
using Core;
using Core.Environment;
using Core.Factory;
using Core.Player;
using Services.Input;
using Services.Providers.AssetProvider;
using Services.Providers.ConfigProvider;
using UnityEngine;

namespace Services.Factory.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInputService _inputService;
        private readonly IGameConfigProvider _configProvider;

        public GameFactory(IAssetProvider assetProvider, IInputService inputService, IGameConfigProvider configProvider)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
            _configProvider = configProvider;
        }

        public GameObject Player { get; private set; }

        public GameObject CreateCamera()
        {
            GameObject cameraObject = Object.Instantiate(_assetProvider.GetCameraPrefab());
            cameraObject.GetComponent<CameraFollow>().Init(Player.transform);
            return cameraObject;
        }

        public GameObject CreatePlayer()
        {
            GameObject playerObject = Object.Instantiate(_assetProvider.GetPlayerPrefab());
            
            playerObject
                .GetComponent<PlayerMove>()
                .Init(_inputService, _configProvider.GetCurrentPlayerConfig().PlayerMoveSpeed);
            playerObject
                .GetComponent<PlayerRotate>()
                .Init(_inputService, _configProvider.GetCurrentPlayerConfig().PlayerRotateSpeed);
            playerObject
                .GetComponent<PlayerInventory>()
                .Init(_configProvider.GetCurrentPlayerConfig().MaxBoxCount);

            Player = playerObject;

            return playerObject;
        }

        public GameObject CreateGround()
        {
            GameObject groundObject = Object.Instantiate(_assetProvider.GetGroundPrefab());
            return groundObject;
        }

        public GameObject CreateFirstFactory()
        {
            GameObject firstFactoryObject = Object.Instantiate(_assetProvider.GetFirstFactoryPrefab(),
                new Vector3(15.0f, 2.0f, 5.0f), Quaternion.identity);

            firstFactoryObject.GetComponent<Core.Factory.Factory>()
                .Init(
                    new FirstFactoryWay(_configProvider.GetCurrentGameConfig().FirstFactoryBox,
                        PickableBoxType.First, _configProvider.GetCurrentGameConfig().MaxBox));
            
            return firstFactoryObject;
        }

        public GameObject CreateSecondFactory()
        {
            GameObject secondFactoryObject = Object.Instantiate(_assetProvider.GetSecondFactoryPrefab(),
                new Vector3(0.0f, 2.0f, 5.0f), Quaternion.identity);

            Core.Factory.Factory secondFactory = secondFactoryObject.GetComponent<Core.Factory.Factory>();
            
            List<Stack<IPickable>> list = new List<Stack<IPickable>>();
            
            foreach (FactoryDropZone dropZone in secondFactory.GetListOfDropZones())
            {
                list.Add(dropZone.PickableBoxes);
            }

            secondFactoryObject.GetComponent<Core.Factory.Factory>()
                .Init(new SecondFactoryWay(list, _configProvider.GetCurrentGameConfig().SecondFactoryBox,
                    PickableBoxType.Second, _configProvider.GetCurrentGameConfig().MaxBox));
            
            secondFactoryObject.GetComponentInChildren<FactoryDropZone>()
                .Init(_configProvider.GetCurrentGameConfig().FirstDropZoneMaterial,
                    _configProvider.GetCurrentGameConfig().FirstDropZoneBoxType,
                    _configProvider.GetCurrentGameConfig().MaxBox);

            return secondFactoryObject;
        }

        public GameObject CreateThirdFactory()
        {
            GameObject thirdFactoryObject = Object.Instantiate(_assetProvider.GetThirdFactoryPrefab(),
                new Vector3(-15.0f, 2.0f, 5.0f), Quaternion.identity);
            
            Core.Factory.Factory secondFactory = thirdFactoryObject.GetComponent<Core.Factory.Factory>();
            
            List<Stack<IPickable>> list = new List<Stack<IPickable>>();
            
            foreach (FactoryDropZone dropZone in secondFactory.GetListOfDropZones())
            {
                list.Add(dropZone.PickableBoxes);
            }

            thirdFactoryObject.GetComponent<Core.Factory.Factory>()
                .Init(new ThirdFactoryWay(list, _configProvider.GetCurrentGameConfig().ThirdFactoryBox,
                    PickableBoxType.Third, _configProvider.GetCurrentGameConfig().MaxBox));

            FactoryDropZone[] factoryDropZones = thirdFactoryObject.GetComponentsInChildren<FactoryDropZone>();
            
            factoryDropZones[0].Init(_configProvider.GetCurrentGameConfig().FirstDropZoneMaterial,
                _configProvider.GetCurrentGameConfig().FirstDropZoneBoxType,
                _configProvider.GetCurrentGameConfig().MaxBox);
            
            factoryDropZones[1].Init(_configProvider.GetCurrentGameConfig().SecondDropZoneMaterial,
                _configProvider.GetCurrentGameConfig().SecondDropZoneBoxType,
                _configProvider.GetCurrentGameConfig().MaxBox);

            return thirdFactoryObject;
        }
    }
}