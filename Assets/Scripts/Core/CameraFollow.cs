using System;
using UnityEngine;

namespace Core
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _followingObject;

        private Vector3 _startPosition;

        public void Init(Transform followingObject)
        {
            _followingObject = followingObject;
        }

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void LateUpdate()
        {
            transform.position = _followingObject.transform.position + _startPosition;
        }
    }
}