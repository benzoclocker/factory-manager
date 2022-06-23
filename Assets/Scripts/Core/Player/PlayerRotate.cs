using Services.Input;
using UnityEngine;

namespace Core.Player
{
    public class PlayerRotate : MonoBehaviour
    {
        private IInputService _inputService;

        public void Init(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _inputService = new InputService();
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
                10 * Time.deltaTime);
        }
    }
}