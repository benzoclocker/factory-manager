using System;
using UnityEngine;

namespace Core.Environment
{
    public class FactoryBox : MonoBehaviour, IPickable
    {
        public Transform BoxTransform { get; private set; }
        public PickableBoxType BoxType { get; private set; }
        public bool IsPickable { get; set; } = true;
        public event Action OnUnPick;
        public void UnPickFromFactory()
        {
            OnUnPick?.Invoke();
        }

        public void Init(PickableBoxType boxType, Transform boxTransform)
        {
            BoxType = boxType;
            BoxTransform = boxTransform;
        }
    }
}