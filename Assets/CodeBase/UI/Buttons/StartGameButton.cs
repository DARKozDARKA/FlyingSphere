using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData.Strings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Buttons
{
    public class StartGameButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
    
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        private void OnEnable() => 
            _button.onClick.AddListener(StartGame);

        private void StartGame() => 
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.Game);
    }
}
