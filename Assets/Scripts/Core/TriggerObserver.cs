using System;
using UnityEngine;

namespace Core
{
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEvent;
        public event Action<Collider> OnTriggerExitEvent; 

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEvent?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitEvent?.Invoke(other);
        }
    }
}