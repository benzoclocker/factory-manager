using System.Collections;
using System.Collections.Generic;
using Core.Environment;
using Core.Factory;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(TriggerObserver))]
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private Transform _inventoryPlace;
        [SerializeField] private Vector3 _inventoryItemOffset;
        
        private PickableBoxType _currentBoxType;
        private bool _isDropping;
        private int _maxBoxCount;
        private float _dropDelay;

        private readonly Stack<IPickable> _pickableBoxes = new Stack<IPickable>();
        
        private TriggerObserver _triggerObserver;
        private IProgress _progressBar;
        private ITimer _timer;

        public void Init(int maxBoxCount, float delay, IProgress progress, ITimer timer)
        {
            _maxBoxCount = maxBoxCount;
            _dropDelay = delay;
            _progressBar = progress;
            _timer = timer;
        }

        private void Awake()
        {
            _triggerObserver = GetComponent<TriggerObserver>();
            _triggerObserver.OnTriggerEvent += PickUpFactoryBox;
            _triggerObserver.OnTriggerEvent += DropFactoryBox;
            _triggerObserver.OnTriggerExitEvent += StopDrop;
        }

        private void Start()
        {
            _progressBar.DeactivateProgress();
        }

        private void Update()
        {
            if (_isDropping)
            {
                _timer.Tick();
                _progressBar.Tick();
            }
        }

        private void StopDrop(Collider obj)
        {
            if (obj.TryGetComponent(out FactoryDropZone dropZone))
            {
                StopAllCoroutines();
                OffProgressBar();
            }
        }

        private void DropFactoryBox(Collider other)
        {
            if (other.TryGetComponent(out FactoryDropZone dropZone))
            {
                StartCoroutine(StartDropRoutine(dropZone));
            }
        }

        private IEnumerator StartDropRoutine(FactoryDropZone dropZone)
        {
            _timer.ResetDelay();
            _progressBar.ActivateProgress();
            _isDropping = true;

            if (dropZone.BoxType == _currentBoxType && dropZone.HaveSpace)
            {
                while (_pickableBoxes.Count > 0)
                {
                    yield return new WaitForSeconds(_dropDelay);
                    
                    IPickable box = _pickableBoxes.Pop();
                    box.BoxTransform.parent = dropZone.transform;
                    box.BoxTransform.position = dropZone.transform.position;
                    dropZone.TakeBox(box);
                    
                    _timer.ResetDelay();
                }
            }

            OffProgressBar();
        }

        private void PickUpFactoryBox(Collider other)
        {
            if (other.TryGetComponent(out IPickable pickable) && pickable.IsPickable && HaveSpaceForOneMoreBox())
            {
                pickable.UnPickFromFactory();
                
                if (_pickableBoxes.Count == 0)
                {
                    _currentBoxType = pickable.BoxType;
                    pickable.IsPickable = false;
                    pickable.BoxTransform.parent = transform;
                    pickable.BoxTransform.position = _inventoryPlace.position;
                    pickable.BoxTransform.rotation = transform.rotation;
                    _pickableBoxes.Push(pickable);
                }
                else
                {
                    bool neededBoxType = pickable.BoxType == _currentBoxType;
                    
                    if (neededBoxType)
                    {
                        pickable.IsPickable = false;
                        pickable.BoxTransform.parent = _pickableBoxes.Peek().BoxTransform;
                        pickable.BoxTransform.position =
                            _pickableBoxes.Peek().BoxTransform.position + _inventoryItemOffset;
                        pickable.BoxTransform.rotation =
                            _pickableBoxes.Peek().BoxTransform.rotation;
                        _pickableBoxes.Push(pickable);
                    }
                }
            }
        }

        private bool HaveSpaceForOneMoreBox() => 
            _pickableBoxes.Count < _maxBoxCount;

        private void OffProgressBar()
        {
            _isDropping = false;
            _timer.ResetDelay();
            _progressBar.DeactivateProgress();
        }
    }
}