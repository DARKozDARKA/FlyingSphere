using CodeBase.Infrastructure.States.DTO;
using CodeBase.Logic.Camera;
using CodeBase.Logic.Player;
using CodeBase.Services.DifficultyService;
using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.Services.Unity;
using CodeBase.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private GameStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;
        private GameStateMachine _stateMachine;
        private IStaticDataService _staticDataService;
        private IPrefabFactory _prefabFactory;
        private IDifficultyService _difficultyService;

        [Inject]
        public void Construct(GameStateMachine stateMachine, ISceneLoader sceneLoader,
            IStaticDataService staticDataService, IPrefabFactory prefabFactory, IDifficultyService difficultyService)
        {
            _difficultyService = difficultyService;
            _prefabFactory = prefabFactory;
            _staticDataService = staticDataService;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string name)
        {
            _sceneLoader.LoadAsync(name, OnLoaded);
        }

        public void Exit() { }

        private void OnLoaded()
        {
            PlayerMovement playerMover = SpawnPlayer();

            _prefabFactory.CreateUIRoot();
            GameObject alvieUI = _prefabFactory.CreateAliveUI();

            GameLoopStateDTO dto = CreateGameLoopStateDTO(playerMover, alvieUI);
            _stateMachine.Enter<GameLoopState, GameLoopStateDTO>(dto);
        }

        private GameLoopStateDTO CreateGameLoopStateDTO(PlayerMovement playerMovement, GameObject aliveUI) =>
            new GameLoopStateDTO()
                .With(_ => _.PlayerMovement = playerMovement)
                .With(_ => _.AliveUI = aliveUI);

        private PlayerMovement SpawnPlayer()
        {
            GameObject player = _prefabFactory.CreatePlayer(_staticDataService.GetLevels()[SceneManager.GetActiveScene().name].PlayerStartPoint);
            PlayerMovement playerMover = player.GetComponent<PlayerMovement>();
            playerMover.SetPlayerData(_staticDataService.GetPlayerData(_difficultyService.GetDifficultyData()));
            Camera.main.GetComponent<CameraFollower>().SetTarget(player);
            return playerMover;
        }
    }
}