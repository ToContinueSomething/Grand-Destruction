using System;
using UnityEngine;

namespace Sources.Logic
{
    public class Raycaster : MonoBehaviour
    {
        private Camera _camera;
        private Ray _ray;
        
        public Vector3 HitPoint { get; private set; }
        public Vector3 Normal { get; private set; }
        public Vector3 ColliderDirection { get; private set; }

        private void Awake()
        {
            _camera = Camera.main;
        }

        public bool Raycast(Vector3 to)
        {
            _ray = _camera.ScreenPointToRay(to);

            if (Physics.Raycast(_ray, out RaycastHit hit, 1000f))
            {
                Normal = hit.normal;
                HitPoint = hit.point;
                ColliderDirection = hit.transform.right;
                return true;
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            if (_camera == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(HitPoint, 0.1f);
            Gizmos.DrawLine(_camera.transform.position, _camera.transform.position + _ray.direction * 1000f);
        }
    }
}