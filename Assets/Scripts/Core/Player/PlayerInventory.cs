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
        
        private Stack<IPickable> _pickableBoxes = new Stack<IPickable>();
        private TriggerObserver _triggerObserver;
        public PickableBoxType _currentBoxType;

        private bool _readyToDrop = true;
        private int _maxBoxCount;

        public void Init(int maxBoxCount)
        {
            _maxBoxCount = maxBoxCount;
        }

        private void Awake()
        {
            _triggerObserver = GetComponent<TriggerObserver>();
            _triggerObserver.OnTriggerEvent += PickUpFactoryBox;
            _triggerObserver.OnTriggerStayEvent += DropFactoryBox;
        }

        private void DropFactoryBox(Collider other)
        {
            if (other.TryGetComponent(out FactoryDropZone dropZone) && _readyToDrop && _pickableBoxes.Count > 0)
            {
                if (dropZone.BoxType == _currentBoxType && dropZone.HaveSpace)
                {
                    IPickable box = _pickableBoxes.Pop();
                    box.BoxTransform.parent = dropZone.transform;
                    box.BoxTransform.position = dropZone.transform.position;
                    dropZone.TakeBox(box);
                }
            }
        }

        private void PickUpFactoryBox(Collider other)
        {
            if (other.TryGetComponent(out IPickable pickable) && pickable.IsPickable && _pickableBoxes.Count < _maxBoxCount)
            {
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
    }
}