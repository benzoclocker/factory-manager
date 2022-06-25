using System.Collections;
using System.Collections.Generic;
using Core.Environment;
using Core.Reasons;
using UnityEngine;

namespace Core.Factory
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] private List<FactoryDropZone> _dropZones;
        
        private readonly Stack<FactoryBox> _currentFactoryStack = new Stack<FactoryBox>();
        private bool _isFactoryWorking;
        
        private IFactoryWay _factoryWay;
        private ITimer _timer;
        private IProgress _progress;
        private IAlert _alert;

        public void Init(IFactoryWay factoryWay, ITimer timer, IProgress progress, IAlert alert)
        {
            _factoryWay = factoryWay;
            _timer = timer;
            _progress = progress;
            _alert = alert;
            
            SubscribeToEvents();
        }

        public List<FactoryDropZone> GetListOfDropZones() =>
            _dropZones;

        private void SubscribeToEvents()
        {
            foreach (FactoryDropZone dropZone in _dropZones)
            {
                dropZone.OnBoxDropped += StartFactory;
            }
        }

        private void Start()
        {
            _progress.DeactivateProgress();
            
            StartFactory();
        }

        private void Update()
        {
            if (_isFactoryWorking)
            {
                _timer.Tick();
                _progress.Tick(); 
            }
        }

        private void StartFactory()
        {
            if (_isFactoryWorking == false)
            {
                StartCoroutine(StartFactoryRoutine());
            }
        }

        private IEnumerator StartFactoryRoutine()
        {
            _timer.ResetDelay();
            _progress.ActivateProgress();

            _isFactoryWorking = true;

            while ((_factoryWay.IsReadyToCreateBox || !HasAnyDropZone()) && HaveSpaceForMoreBoxes())
            {
                yield return new WaitForSeconds(_factoryWay.CreationDelay);
                CreateBox();
            }
            
            AlertFromFactory();

            _isFactoryWorking = false;
            _progress.DeactivateProgress();
        }

        private void CreateBox()
        {
            _timer.ResetDelay();
            
            foreach (FactoryDropZone dropZone in _dropZones)
            {
                FactoryBox box = (FactoryBox)dropZone.PickableBoxes.Pop();
                box.gameObject.SetActive(false);
            }

            float zPosition = _currentFactoryStack.Count > 0
                ? _currentFactoryStack.Peek().BoxTransform.position.z - 4.0f
                : transform.position.z;
            
            FactoryBox factoryBox = Instantiate(_factoryWay.Box,
                new Vector3(transform.position.x - 5.0f, transform.position.y, zPosition),
                Quaternion.identity);
            factoryBox.Init(_factoryWay.BoxType, factoryBox.transform);
            factoryBox.OnUnPick += UnPick;

            _currentFactoryStack.Push(factoryBox);
        }

        private void UnPick()
        {
            _currentFactoryStack.Pop();

            if (HaveSpaceForMoreBoxes())
            {
                StartFactory();
            }
        }

        private bool HasAnyDropZone() => 
            _dropZones.Count != 0;

        private bool HaveSpaceForMoreBoxes() => 
            _currentFactoryStack.Count < _factoryWay.MaxFactoryCapacity;

        private void AlertFromFactory()
        {
            if (!_factoryWay.IsReadyToCreateBox)
            {
                _alert.AlertPlayer(this, new BoxStackInFactoryEmpty());
            }

            if (!HaveSpaceForMoreBoxes())
            {
                _alert.AlertPlayer(this, new NoSpaceForNewBoxesReason());
            }
        }
    }
}