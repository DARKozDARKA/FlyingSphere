using CodeBase.Services.InputService;
using CodeBase.StaticData.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private CharacterController _characterController;

        private PlayerData _playerData;
    
        private Vector3 _currentDirection;
        private int _level = 0;
        private IInputService _inputService;
        private float _levelSpeedModifier => _level == 0 ? 1 : _level * _playerData.UpgradeModifier;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
    
        private void Start()
        {
            _currentDirection.x = _playerData.HorizontalSpeed;
        }

        private void Update()
        {
            if (_inputService.GetUpButton())
                _currentDirection.y += _playerData.VerticalSpeed * Time.deltaTime * _levelSpeedModifier;
            else
                _currentDirection.y -= _playerData.VerticalSpeed * Time.deltaTime * _levelSpeedModifier;
        
            _characterController.Move(_currentDirection * Time.deltaTime);
        }
    
        public void SetPlayerData(PlayerData playerData) => 
            _playerData = playerData;

        public void IncreaseLevel() =>
            _level++;
    }
}
