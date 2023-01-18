using CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class PlayerHitDetector : MonoBehaviour
    {
        [SerializeField]
        private CharacterController _characterController;
    
        private GameLoopState _gameLoopState;
        private bool _died;

        [Inject]
        private void Construct(GameLoopState gameLoopState)
        {
            _gameLoopState = gameLoopState;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (_died)
                return;

            _died = true;
            _gameLoopState.EndGame();
        }
    }
}
