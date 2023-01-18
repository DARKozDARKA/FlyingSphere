using UnityEngine;

namespace CodeBase.Logic.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _cameraDistance;

        [SerializeField]
        private float _lerpValue;
    
        [SerializeField]
        private float _lerpSpeed;
    
        [SerializeField]
        private GameObject _target;
        
        private bool _isTargetSet;

        public void SetTarget(GameObject target)
        {
            _target = target;
            _isTargetSet = true;
        }


        private void Awake()
        {
            if (_target != null)
                _isTargetSet = true;
        }

        private void LateUpdate()
        {
            if (_isTargetSet == false)
                return;
        
            transform.position = Vector3.Lerp(transform.position, _target.transform.position + _cameraDistance, _lerpValue * Time.deltaTime * _lerpSpeed);
        }

        private void OnValidate()
        {
            if (_lerpValue > 1f || _lerpValue < 0f)
            {
                _lerpValue = 1;
                Debug.LogError("Lerp value must be in range of [0; 1]");
            }
        }
    }
}
