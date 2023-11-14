using System.Collections;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace StateMachine
{
    public class MoveTargetState : MonoBehaviour, IState<Target>
    {
        private Target _target;
        protected IMovable Movable => _target;

        public void Enter(Target target)
        {
            _target = target;
            MoveTo();
        }
        
        protected virtual void MoveTo()
        {
            Movable.MoveToHorizontal();
        }

    }
}