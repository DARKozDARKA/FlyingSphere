using System.Collections.Generic;
using CodeBase.Tools;
using UnityEngine;

namespace CodeBase.Logic.Spawners
{
    public class ObjectsInBoundSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _objectPrefab;

        [SerializeField]
        private float _span;

        [SerializeField]
        protected Transform _startPoint;

        [SerializeField]
        private Transform _target;

        [SerializeField]
        private float _targetView;

        private List<GameObject> _spawnedObjects = new();
        private int _lastObjectID;
        private Vector3 _lastSpawnPosition;


        private void Start()
        {
            _lastSpawnPosition = _startPoint.position;
        
            SpawnStartObjects();

            SetNextObjectAsFirst();
        
            void SetNextObjectAsFirst() =>
                _lastObjectID = 0;
        }

        private void Update()
        {
            if (ObjectIsOutOfBottomBound(ObjectHorizontalPosition(), _target.position, _targetView))
            {
                SetNextPosition(ref _lastSpawnPosition);
                SetNewPosition();
                SetNextObjectAsLast();
            }

            float ObjectHorizontalPosition() => 
                _spawnedObjects[_lastObjectID].transform.position.x;

            void SetNewPosition() => 
                _spawnedObjects[_lastObjectID].transform.position = _lastSpawnPosition;
        }

        private void SetNextObjectAsLast()
        {
            _lastObjectID++;
            if (_lastObjectID >= _spawnedObjects.Count)
                _lastObjectID = 0;
        }

        private void SpawnStartObjects()
        {
            while (true)
            {
                CreateObject();

                if (ObjectIsOutOfTopBound(_lastSpawnPosition.x + _span, _target.position, _targetView))
                    break;

                SetNextPosition(ref _lastSpawnPosition);
            }
        }

        protected virtual void SetNextPosition(ref Vector3 position) => 
            position.x += _span;

        private void CreateObject() =>
            Instantiate(_objectPrefab, _lastSpawnPosition, Quaternion.identity)
                .With(_ => _spawnedObjects.Add(_))
                .With(_ => _.transform.parent = transform); 

        protected virtual bool ObjectIsOutOfTopBound(float objectHorizontalPosition, Vector3 targetPosition, float targetView) => 
            objectHorizontalPosition > targetPosition.x + targetView;
    
        private bool ObjectIsOutOfBottomBound(float objectHorizontalPosition, Vector3 targetPosition, float targetView) => 
            objectHorizontalPosition <= targetPosition.x - targetView;
    }
}
