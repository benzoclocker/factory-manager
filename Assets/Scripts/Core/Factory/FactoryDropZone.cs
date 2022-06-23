using System;
using System.Collections.Generic;
using Core.Environment;
using UnityEngine;

namespace Core.Factory
{
    public class FactoryDropZone : MonoBehaviour
    {
        public Stack<IPickable> _pickableBoxes { get; private set; } = new Stack<IPickable>();
        public PickableBoxType BoxType { get; private set; }

        public void Init(PickableBoxType boxType)
        {
            BoxType = boxType;
        }

        private void Start()
        {
            Init(PickableBoxType.First);
        }

        public void TakeBox(IPickable box)
        {
            _pickableBoxes.Push(box);
        }
    }
}