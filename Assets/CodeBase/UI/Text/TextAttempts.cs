using CodeBase.Services.DynamicData;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Text
{
    public class TextAttempts : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private string _pretext;

        private IProgressService _progressService;

        [Inject]
        private void Construct(IProgressService progressService)
        {
            _progressService = progressService;
        }

        private void Start()
        {
            _text.text = _pretext + _progressService.PlayerProgress.AttemptsAmount;
        }
    }
}