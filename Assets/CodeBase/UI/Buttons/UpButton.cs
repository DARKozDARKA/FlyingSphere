using CodeBase.Services.InputService;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.UI.Buttons
{
    public class UpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private IInputServiceProvider _inputServiceProvider;

        [Inject]
        private void Construct(IInputServiceProvider inputServiceProvider)
        {
            _inputServiceProvider = inputServiceProvider;
        }

        public void OnPointerDown(PointerEventData eventData) =>
            _inputServiceProvider.SetUpButtonDown();

        public void OnPointerUp(PointerEventData eventData) =>
            _inputServiceProvider.SetUpButtonUp();

        private void OnDestroy() => 
            _inputServiceProvider.SetUpButtonUp();
    }
}