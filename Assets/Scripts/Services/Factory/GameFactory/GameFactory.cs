using System.Collections.Generic;
using Core;
using Core.Environment;
using Core.Factory;
using Core.GameConfigs;
using Core.Player;
using Services.Factory.UIFactory;
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
        private readonly IUIFactory _uiFactory;

        public GameFactory(IAssetProvider assetProvider, IInputService inputService,
            IGameConfigProvider configProvider, IUIFactory uiFactory)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
            _configProvider = configProvider;
            _uiFactory = uiFactory;
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
            PlayerConfig config = _configProvider.GetCurrentPlayerConfig();
            
            GameObject playerObject = Object.Instantiate(_assetProvider.GetPlayerPrefab());

            ITimer timer = new Timer();

            ProgressBar progressBar = playerObject.GetComponent<ProgressBar>();

            playerObject
                .GetComponent<PlayerMove>()
                .Init(_inputService, config.PlayerMoveSpeed);
            playerObject
                .GetComponent<PlayerRotate>()
                .Init(_inputService, config.PlayerRotateSpeed);
            playerObject
                .GetComponent<PlayerInventory>()
                .Init(config.MaxBoxCount, config.UnPickDelay, new Progress(progressBar, timer, config.UnPickDelay), timer);

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
            GameConfig config = _configProvider.GetCurrentGameConfig();
            
            GameObject firstFactoryObject = Object.Instantiate(_assetProvider.GetFirstFactoryPrefab(),
                new Vector3(15.0f, 2.0f, 5.0f), Quaternion.identity);

            ProgressBar progressBar = firstFactoryObject.GetComponent<ProgressBar>();

            ITimer timer = new Timer();
            
            firstFactoryObject.GetComponent<Core.Factory.Factory>()
                .Init(
                    new FirstFactoryWay(config.FirstFactoryBox, 
                        PickableBoxType.First, 
                        config.MaxBox, 
                        config.CreationTime), 
                    timer, 
                    new Progress(progressBar, timer, config.CreationTime), 
                    _uiFactory.Alert
                    );
            
            return firstFactoryObject;
        }

        public GameObject CreateSecondFactory()
        {
            GameConfig config = _configProvider.GetCurrentGameConfig();
            
            GameObject secondFactoryObject = Object.Instantiate(_assetProvider.GetSecondFactoryPrefab(),
                new Vector3(0.0f, 2.0f, 5.0f), Quaternion.identity);

            Core.Factory.Factory secondFactory = secondFactoryObject.GetComponent<Core.Factory.Factory>();
            
            List<Stack<IPickable>> list = new List<Stack<IPickable>>();
            
            foreach (FactoryDropZone dropZone in secondFactory.GetListOfDropZones())
            {
                list.Add(dropZone.PickableBoxes);
            }

            ProgressBar progressBar = secondFactory.GetComponent<ProgressBar>();
            
            ITimer timer = new Timer();

            secondFactoryObject.GetComponent<Core.Factory.Factory>()
                .Init(
                    new SecondFactoryWay(list, config.SecondFactoryBox,
                    PickableBoxType.Second, config.MaxBox,
                    config.CreationTime), 
                    timer,
                    new Progress(progressBar, timer, config.CreationTime),
                    _uiFactory.Alert
                    );
            
            secondFactoryObject.GetComponentInChildren<FactoryDropZone>()
                .Init(config.FirstDropZoneMaterial, config.FirstDropZoneBoxType,
                    config.MaxBox);

            return secondFactoryObject;
        }

        public GameObject CreateThirdFactory()
        {
            GameConfig config = _configProvider.GetCurrentGameConfig();
            
            GameObject thirdFactoryObject = Object.Instantiate(_assetProvider.GetThirdFactoryPrefab(),
                new Vector3(-15.0f, 2.0f, 5.0f), Quaternion.identity);
            
            Core.Factory.Factory thirdFactory = thirdFactoryObject.GetComponent<Core.Factory.Factory>();
            
            List<Stack<IPickable>> list = new List<Stack<IPickable>>();
            
            foreach (FactoryDropZone dropZone in thirdFactory.GetListOfDropZones())
            {
                list.Add(dropZone.PickableBoxes);
            }

            ProgressBar progressBar = thirdFactory.GetComponent<ProgressBar>();
            
            ITimer timer = new Timer();

            thirdFactoryObject.GetComponent<Core.Factory.Factory>()
                .Init(
                    new ThirdFactoryWay(list, config.ThirdFactoryBox,
                    PickableBoxType.Third, config.MaxBox,
                    config.CreationTime),
                    timer, 
                    new Progress(progressBar, timer, config.CreationTime),
                    _uiFactory.Alert
                );

            FactoryDropZone[] factoryDropZones = thirdFactoryObject.GetComponentsInChildren<FactoryDropZone>();
            
            factoryDropZones[0]
                .Init(config.FirstDropZoneMaterial, config.FirstDropZoneBoxType, config.MaxBox);
            
            factoryDropZones[1]
                .Init(config.SecondDropZoneMaterial, config.SecondDropZoneBoxType, config.MaxBox);

            return thirdFactoryObject;
        }
    }
}