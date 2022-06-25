using Core.GameConfigs;
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
        private float _moveSpeed;

        public void Init(IInputService inputService, float moveSpeed)
        {
            _inputService = inputService;
            _moveSpeed = moveSpeed;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
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
            _moveDirection = _inputService.Direction.normalized * (_moveSpeed * Time.deltaTime);

        private void Move() => 
            _characterController.Move(_moveDirection);
    }
}