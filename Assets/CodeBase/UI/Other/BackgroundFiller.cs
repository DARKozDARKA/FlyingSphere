using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Other
{
    public class BackgroundFiller : MonoBehaviour
    {
        [SerializeField]
        private float _fillSpeed;

        [SerializeField]
        private bool _fillOnStart;

        [SerializeField]
        private Color _startColor;

        [SerializeField]
        private Color _endColor;

        [SerializeField]
        private Image _image;

        public Action OnEnded;

        private void Start()
        {
            if (_fillOnStart)
                StartFilling();
        }

        public void StartFilling()
        {
            StartCoroutine(Fill());
        }

        private IEnumerator Fill()
        {
            float percentage = 0;
            while (true)
            {
                yield return null;
            
                _image.color = Color.Lerp(_startColor, _endColor, percentage);
                percentage += _fillSpeed * Time.deltaTime;

                if (percentage >= 1)
                {
                    OnEnded?.Invoke();
                    break;
                }
            }
        }
    }
}