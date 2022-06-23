using Services.Input;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        private CharacterController _characterController;
        private IInputService _inputService;

        private Vector3 _moveDirection;

        public void Init(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _inputService = new InputService();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            CalculateDirection();
        }

        private void CalculateDirection() => 
            _moveDirection = _inputService.Direction * Time.deltaTime;

        private void Move() => 
            _characterController.Move(_moveDirection);
    }
}