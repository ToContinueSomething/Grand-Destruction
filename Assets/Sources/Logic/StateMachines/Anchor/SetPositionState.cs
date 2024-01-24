using System.Numerics;
using Sources.Logic.StateMachines.Camera;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Sources.Logic.StateMachines.Anchor
{
    public class SetPositionState : IState
    {
        private readonly AnchorStateMachine _anchorStateMachine;
        private readonly Anchor _anchor;
        private readonly Raycaster _raycaster;
        private readonly Target.Target _target;

        public SetPositionState(AnchorStateMachine anchorStateMachine, Anchor anchor,Raycaster raycaster)
        {
            _anchorStateMachine = anchorStateMachine;
            _anchor = anchor;
            _raycaster = raycaster;
        }

        public void Enter()
        {
            _anchor.gameObject.SetActive(true);
            _anchor.SetPosition(_raycaster.HitPoint,_raycaster.ColliderDirection);
            _anchorStateMachine.EnterStateMachine<CameraStateMachine>();
        }
    }
}