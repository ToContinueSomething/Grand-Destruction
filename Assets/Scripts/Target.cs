using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Interfaces;
using UnityEngine;

public class Target : MonoBehaviour, IMovable
{
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _speed;
    [SerializeField] private Ease _ease;
    [SerializeField] private float _duration;

    private RectTransform _rectTransform;
    private Vector3[] _positions;
    private Camera _camera;

    private bool _isInvertPosition;
    private bool _isMove;
    private Coroutine _coroutine;
    private Vector3 _target;
    private Vector3 _upperLeftPosition;
    private Vector3 _lowerLeftPosition;
    private Vector3 _lowerRightPosition;
    private Vector3 _upperRightPosition;

    private bool _isHorizontalRight;
    private bool _isVerticalUp = true;
    private Tween _moveHorizontalTween;
    private Tween _moveVerticalTween;

    public Vector3 ScreenPoint => _rectTransform.position;
    public Vector3 Position => _camera.ScreenToWorldPoint(_rectTransform.position);

    private void Awake()
    {
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(Vector3 minPosition, Vector3 maxPosition)
    {
        var upperLeftPosition = new Vector3(minPosition.x  , maxPosition.y, maxPosition.z);
        var lowerLeftPosition = new Vector3(minPosition.x, minPosition.y, maxPosition.z);

        var lowerRightPosition = new Vector3(maxPosition.x, minPosition.y , minPosition.z);
        var upperRightPosition = new Vector3(maxPosition.x, maxPosition.y, minPosition.z);

        
        _upperLeftPosition = _camera.WorldToScreenPoint(upperLeftPosition);
        _lowerRightPosition = _camera.WorldToScreenPoint(lowerRightPosition);
        _lowerLeftPosition = _camera.WorldToScreenPoint(lowerLeftPosition);
        _upperRightPosition = _camera.WorldToScreenPoint(upperRightPosition);


        //_rectTransform.position = lowerLeftPosition;
        // _coroutine = StartCoroutine(Move());
        
        Debug.Log("init");
    }

    public void MoveToHorizontal()
    {
        _moveVerticalTween.Kill();

        _isHorizontalRight = !_isHorizontalRight;
        
        _target = _isHorizontalRight ?   _lowerRightPosition : _lowerLeftPosition;
        Debug.Log(_target.x);
        _moveHorizontalTween = _rectTransform.DOMoveX(_target.x, _duration).SetEase(_ease).OnComplete(MoveToHorizontal);

      
    }

    public void MoveToVertical()
    {
        _moveHorizontalTween.Kill();
        _isVerticalUp = !_isVerticalUp;
        _target = _isVerticalUp ?   _lowerRightPosition : _upperRightPosition;
        _moveVerticalTween = transform.DOMoveY(_target.y, _duration).SetEase(_ease).OnComplete(MoveToVertical);
    }


    public void Disable()
    {
        _moveHorizontalTween.Kill();
        _moveVerticalTween.Kill();
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (_camera == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(_camera.ScreenToWorldPoint(_upperLeftPosition), 0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_camera.ScreenToWorldPoint(_upperRightPosition), 0.5f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(_camera.ScreenToWorldPoint(_lowerLeftPosition), 0.5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_camera.ScreenToWorldPoint(_lowerRightPosition), 0.5f);
    }
}