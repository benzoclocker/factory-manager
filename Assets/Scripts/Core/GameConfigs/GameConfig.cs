using Core.Environment;
using UnityEngine;

namespace Core.GameConfigs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/Game Config/Core Game Config")]
    public class GameConfig : ScriptableObject
    {
        public FactoryBox FirstFactoryBox;
        public FactoryBox SecondFactoryBox;
        public FactoryBox ThirdFactoryBox;
        
        public Material FirstDropZoneMaterial;
        public Material SecondDropZoneMaterial;
        
        public PickableBoxType FirstDropZoneBoxType;
        public PickableBoxType SecondDropZoneBoxType;
        
        public int MaxBox;
    }
}