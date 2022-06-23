using System;
using UnityEngine;

namespace Core
{
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEvent;
        public event Action<Collider> OnTriggerStayEvent; 

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEvent?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerStayEvent?.Invoke(other);
        }
    }
}