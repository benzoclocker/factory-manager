using Services.Input;
using UnityEngine;

namespace Core.Player
{
    public class PlayerRotate : MonoBehaviour
    {
        private IInputService _inputService;

        private float _rotateSpeed;

        public void Init(IInputService inputService, float rotateSpeed)
        {
            _inputService = inputService;
            _rotateSpeed = rotateSpeed;
        }

        private void Update()
        {
            if (_inputService.InputInAction)
            {
                Rotate();
            }
        }

        private void Rotate()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(_inputService.Direction),
                _rotateSpeed * Time.deltaTime);
        }
    }
}