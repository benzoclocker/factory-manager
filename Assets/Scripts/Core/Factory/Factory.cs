using System.Collections.Generic;
using Core.Environment;
using UnityEngine;

namespace Core.Factory
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] private List<FactoryDropZone> _dropZones;
        
        private IFactoryWay _factoryWay;

        public void Init(IFactoryWay factoryWay)
        {
            _factoryWay = factoryWay;
        }

        private void Start()
        {
            foreach (var dropZone in _dropZones)
            {
                List<Stack<IPickable>> list = new List<Stack<IPickable>>();
                list.Add(dropZone._pickableBoxes);
            }
        }

        private void StartFactory()
        {
            if (_factoryWay.IsReadyToCreateBox)
            {
                CreateBox();
            }
        }

        private void CreateBox()
        {
            
        }
    }
}