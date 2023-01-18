using UnityEngine;

namespace CodeBase.Logic.Spawners
{
    public class ObstacleSpawner : ObjectsInBoundSpawner
    {
        [SerializeField]
        private float _offset;
    
        [SerializeField]
        private float _verticalScatter;

        protected override void SetNextPosition(ref Vector3 position)
        {
            base.SetNextPosition(ref position);
            position.y = _startPoint.position.y + Random.Range(-_verticalScatter, _verticalScatter);
        }
    
        protected override bool ObjectIsOutOfTopBound(float objectHorizontalPosition, Vector3 targetPosition, float targetView) => 
            objectHorizontalPosition > targetPosition.x + targetView + _offset;
    }
}