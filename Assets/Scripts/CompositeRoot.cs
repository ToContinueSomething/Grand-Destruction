using System;
using StateMachine;
using UnityEngine;

namespace DefaultNamespace
{
    public class CompositeRoot : MonoBehaviour
    {
        [SerializeField] private InputRouter _inputRouter;
        [SerializeField] private Target _target;
        [SerializeField] private Anchor _anchor;
        [SerializeField] private Entity _entity;
        [SerializeField] private TargetStateMachine _targetStateMachine;
        [SerializeField] private Raycaster _raycaster;
        [SerializeField] private CameraMovement _cameraMovement;


        private void OnEnable()
        {
            _inputRouter.Enable();
            _inputRouter.Clicked += OnClicked;
            _targetStateMachine.Finished += OnTargetStateMachineFinished;
            _cameraMovement.Finished += OnCameraMoveFinished;
        }

        private void OnDisable()
        {
            _inputRouter.Clicked -= OnClicked;
            _targetStateMachine.Finished -= OnTargetStateMachineFinished;
            _cameraMovement.Finished -= OnCameraMoveFinished;
        }

        private void OnCameraMoveFinished()
        {
            _anchor.PullDown();
        }

        private void OnTargetStateMachineFinished()
        {
            _anchor.SetPosition(Vector3.zero);
            _cameraMovement.Move();
        }

        private void Start()
        {
            _target.Init(_entity.MinPositionBound,_entity.MaxPositionBound);
            _targetStateMachine.EnterState();
        }

        private void OnClicked()
        {
            _targetStateMachine.NextState();
            _raycaster.Raycast(_target.Position);
        }
    }

    internal class CameraMovement : MonoBehaviour
    {
        public event Action Finished;

        public void Move()
        {
            
        }
    }
}