using System.Collections;
using CodeBase.Infrastructure.States.DTO;
using CodeBase.Services.DynamicData;
using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.Services.Unity;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IPayloadedState<GameLoopStateDTO>
    {
        private GameStateMachine _gameStateMachine;
        private IStaticDataService _staticDataService;

        private float _upgradeTime;
        private ICoroutineRunner _coroutineRunner;
        private IEnumerator _upgradeCoroutine;
        private ITimeCounter _timeCounter;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private GameLoopStateDTO _gameLoopStateDTO;
        private IPrefabFactory _prefabFactory;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine,
            ICoroutineRunner coroutineRunner, IStaticDataService staticDataService, ITimeCounter timeCounter,
            IProgressService progressService, ISaveLoadService saveLoadService, IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _timeCounter = timeCounter;
            _coroutineRunner = coroutineRunner;
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(GameLoopStateDTO payload)
        {
            _gameLoopStateDTO = payload;
            _timeCounter.StartCountingTime();

            _upgradeTime = _staticDataService.GetGameData().PlayerUpgradeTime;

            _upgradeCoroutine = UpgradePlayer();
            _coroutineRunner.StartCoroutine(_upgradeCoroutine);
        }

        public void Exit()
        {
            _coroutineRunner.StopCoroutine(_upgradeCoroutine);
        }

        private IEnumerator UpgradePlayer()
        {
            while (true)
            {
                yield return new WaitForSeconds(_upgradeTime);
                _gameLoopStateDTO.PlayerMovement.IncreaseLevel();
            }
        }

        public void EndGame()
        {
            Object.Destroy(_gameLoopStateDTO.AliveUI);
            _prefabFactory.CreateDeadUI();
            _progressService.PlayerProgress.AttemptsAmount++;
            _saveLoadService.SaveProgress();
        }
    }
}