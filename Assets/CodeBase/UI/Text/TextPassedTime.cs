using CodeBase.Services.Unity;
using CodeBase.Tools;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Text
{
    public class TextPassedTime : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private string _pretext;
    
        private ITimeCounter _timeCounter;

        [Inject]
        private void Construct(ITimeCounter timeCounter)
        {
            _timeCounter = timeCounter;
        }

        private void Start()
        {
            string timeText = TimeConverter.FromSecondsToTime(Mathf.RoundToInt(_timeCounter.GetCurrentTimeDifference()));
            _text.text = _pretext + timeText;
        }
    }
}