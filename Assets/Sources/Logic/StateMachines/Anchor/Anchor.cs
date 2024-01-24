using System;
using DG.Tweening;
using Sources.Models;
using UnityEngine;

namespace Sources.Logic.StateMachines.Anchor
{
    public class Anchor : MonoBehaviour
    {
        [SerializeField] private Transform _upPoint;
        [SerializeField] private Transform _lowPoint;

        [SerializeField] private Transform _parent;
        [SerializeField] private Raycaster _raycaster;
        [SerializeField] private Attempts _attempts;
        
        private Transform _transform;
        private float _radius;
        private float _offset = 5;
        private Vector3 _verticalOffset = new Vector3(0,14f,0);
        private Tween _moveTween;
        public Raycaster Raycaster => _raycaster;
        public bool CanAttack => _attempts.Value > 0;

        public event Action Finished;

        private void Awake()
        {
            _transform = transform;
        }

        public void Construct(float radius)
        {
            
        }

        public void SetPosition(Vector3 point, Vector3 direction)
        {
            transform.position = Vector3.zero;
            transform.LookAt(-direction,transform.forward);
           
            
            var height = (_lowPoint.position - _upPoint.position).magnitude;
            transform.position = new Vector3(point.x,point.y + height,point.z);
            
            /*_transform.position = point + (normal * _offset) + _verticalOffset;
           
            */
            var eulerAngles = _transform.rotation.eulerAngles;
            _transform.rotation = Quaternion.Euler(75,eulerAngles.y,90);
        }

        public void PullDown()
        {
        }

        public void ReduceAttempts()
        {
            _attempts.Reduce();
        }
        
        public void Move()
        {
            _moveTween =  _transform.DORotate(new Vector3(-90,transform.eulerAngles.y,90),2f).OnComplete(() => Finished?.Invoke());
        }

        public void Disable()
        {
            _moveTween.Kill();
            gameObject.SetActive(false);
        }
    }
}