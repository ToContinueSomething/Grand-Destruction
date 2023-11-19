using System;
using Sources.Data;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace DefaultNamespace
{
    public class Anchor : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private Transform _parent;

        private Transform _transform;
        private float _radius;

        private void Awake()
        {
            _transform = transform;
        }

        public void Construct(float radius)
        {
            _radius = radius;
        }
        
        public void SetPosition(Vector3 point)
        {
            //  Vector2 offset = (Vector2)point - (Vector2)_center.position;
            //  _transform.position = new Vector3(_transform.position.x +  + offset.x,_transform.position.y + offset.y, _transform.position.z);
            transform.LookAt(new Vector3(point.x, _transform.position.y, _transform.position.z), Vector3.up);
        }

        public void PullDown()
        {
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.ParameterData.UpgradeForAnchor == null)
                throw new NullReferenceException();

            _radius = progress.ParameterData.UpgradeForAnchor.RadiusForAnchor;
        }
    }
}