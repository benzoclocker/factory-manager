using System.Collections;
using System.Collections.Generic;
using Core.Environment;
using UnityEngine;

namespace Core.Factory
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] private List<FactoryDropZone> _dropZones;
        private Stack<FactoryBox> _factoryBoxes = new Stack<FactoryBox>();

        private IFactoryWay _factoryWay;

        public List<FactoryDropZone> GetListOfDropZones() => _dropZones;

        public void Init(IFactoryWay factoryWay)
        {
            _factoryWay = factoryWay;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            foreach (var dropZone in _dropZones)
            {
                dropZone.OnBoxDropped += StartFactory;
            }
        }

        private void Start()
        {
            if (_dropZones.Count == 0)
            {
                StartCoroutine(CreateBoxRoutine());
            }
        }

        private IEnumerator CreateBoxRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(3.0f);
                if (!(_factoryBoxes.Count >= _factoryWay.MaxFactoryCapacity))
                {
                    CreateBox();
                }
            }
        }

        private void StartFactory()
        {
            while (_factoryWay.IsReadyToCreateBox)
            {
                CreateBox();
            }
        }

        private void CreateBox()
        {
            foreach (FactoryDropZone dropZone in _dropZones)
            {
                FactoryBox box = (FactoryBox)dropZone._pickableBoxes.Pop();
                box.gameObject.SetActive(false);
            }

            FactoryBox factoryBox = Instantiate(_factoryWay.Box,
                new Vector3(transform.position.x - 5.0f, transform.position.y, transform.position.z),
                Quaternion.identity);
            factoryBox.Init(_factoryWay.BoxType, factoryBox.transform);

            _factoryBoxes.Push(factoryBox);
        }
    }
}