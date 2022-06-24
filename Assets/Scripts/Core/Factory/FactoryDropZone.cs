using System;
using System.Collections.Generic;
using Core.Environment;
using UnityEngine;

namespace Core.Factory
{
    public class FactoryDropZone : MonoBehaviour
    {
        public Stack<IPickable> PickableBoxes { get; private set; } = new Stack<IPickable>();
        public PickableBoxType BoxType { get; private set; }
        public bool HaveSpace => PickableBoxes.Count < _maxBoxes; 
        
        private int _maxBoxes;
        private MeshRenderer _meshRenderer;

        public event Action OnBoxDropped;

        public void Init(Material dropZoneMaterial, PickableBoxType boxType, int maxBoxes)
        {
            _meshRenderer.material = dropZoneMaterial;
            BoxType = boxType;
            _maxBoxes = maxBoxes;
        }

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void TakeBox(IPickable box)
        {
            PickableBoxes.Push(box);
            OnBoxDropped?.Invoke();
        }
    }
}