using UnityEngine;

namespace CodeBase.UI.Other
{
    public class ActivateAfterFilled : MonoBehaviour
    {
        [SerializeField]
        private BackgroundFiller _backgroundFiller;

        [SerializeField]
        private GameObject _activeObject;

        private void OnEnable() => 
            _backgroundFiller.OnEnded += ActivateObject;
        
        private void OnDisable() => 
            _backgroundFiller.OnEnded -= ActivateObject;

        private void ActivateObject()
        {
            _activeObject.SetActive(true);
        }
    }
}