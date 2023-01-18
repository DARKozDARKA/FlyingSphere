using CodeBase.Services.DifficultyService;
using CodeBase.StaticData.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Buttons
{
    public class DifficultyButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private Difficulties _difficulty;

        private IDifficultyService _difficultyService;

        [Inject]
        private void Construct(IDifficultyService difficultyService) => 
            _difficultyService = difficultyService;
    
        private void Awake() => 
            _button.onClick.AddListener(OnClicked);

        private void OnClicked() => 
            _difficultyService.SetDifficulty(_difficulty);
    }
}
