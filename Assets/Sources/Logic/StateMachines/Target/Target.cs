using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Sources.Logic.StateMachines.Target
{
    public class Target : MonoBehaviour, IMovable
    {
        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;
        [SerializeField] private RectTransform _leftPoint;
        [SerializeField] private RectTransform _rightPoint;
        [SerializeField] private RectTransform _upPoint;
        [SerializeField] private RectTransform _downPoint;

        private RectTransform _rectTransform;
        private Vector3[] _positions;

        private bool _isInvertPosition;
        private bool _isMove;
        private Coroutine _coroutine;
        private Vector3 _target;


        private bool _isHorizontalRight;
        private bool _isVerticalUp = true;
        private Tween _moveHorizontalTween;
        private Tween _moveVerticalTween;
        private Vector3 _finishedPosition;

        public Vector3 ScreenPoint => _rectTransform.position;
       // public Vector3 Position => _camera.ScreenToWorldPoint(_rectTransform.position);
        public Vector3 FinishedPosition => _finishedPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }


        public void MoveToHorizontal()
        {
            _moveVerticalTween.Kill();

            _isHorizontalRight = !_isHorizontalRight;

            _target = _isHorizontalRight ? _rightPoint.position : _leftPoint.position;

            var startDistance = Vector3.Distance(_leftPoint.position, _rightPoint.position);
            var currentDistance = Vector3.Distance(_rectTransform.position, _target);
            var duration = _duration / (startDistance / currentDistance);

            _moveHorizontalTween =
                _rectTransform.DOMoveX(_target.x, duration).SetEase(_ease).OnComplete(MoveToHorizontal);
        }


        public void MoveToVertical()
        {
            _moveHorizontalTween.Kill();

            _isVerticalUp = !_isVerticalUp;

            _target = _isVerticalUp ? _upPoint.position : _downPoint.position;

            var startDistance = Vector3.Distance( _downPoint.position,  _upPoint.position);
            var currentDistance = Vector3.Distance(_rectTransform.position, _target);
            var duration = _duration / (startDistance / currentDistance);

            _moveVerticalTween = transform.DOMoveY(_target.y, duration).SetEase(_ease).OnComplete(MoveToVertical);
        }

        public void Disable()
        {
            DOTween.KillAll();
            _finishedPosition = _rectTransform.position;
            _moveHorizontalTween.Kill();
            _moveVerticalTween.Kill();
            gameObject.SetActive(false);
        }
    }
}