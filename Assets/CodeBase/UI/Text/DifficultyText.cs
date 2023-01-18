using CodeBase.Services.DifficultyService;
using CodeBase.StaticData.ScriptableObjects;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Text
{
    public class DifficultyText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textUGUI;
        
        [SerializeField]
        private string _preText;

        private IDifficultyService _difficultyService;

        [Inject]
        private void Construct(IDifficultyService difficultyService)
        {
            _difficultyService = difficultyService;
            _difficultyService.OnDifficultyChanged += ChangeDifficulty;
        }

        private void Start()
        {
            ChangeDifficulty(_difficultyService.GetDifficultyData());
        }

        private void OnDestroy() => 
            _difficultyService.OnDifficultyChanged -= ChangeDifficulty;

        private void ChangeDifficulty(DifficultyData data) => 
            SetText(data.DifficultyName);

        private void SetText(string text) => 
            _textUGUI.text = _preText + text;
    }
}


