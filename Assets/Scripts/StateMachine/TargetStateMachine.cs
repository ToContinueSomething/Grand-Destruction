using System;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace StateMachine
{
    public class TargetStateMachine : MonoBehaviour
    {
        [SerializeField] private Target _target;
        [SerializeField] private  List<SerializableInterface<IState<Target>>> _states;

        public event Action Finished;
        
        private int _currentStateIndex;
        private bool CanEnterNextState => _currentStateIndex < _states.Count;

        public void EnterState()
        {
            _states[_currentStateIndex].Value.Enter(_target);
        }

        public void NextState()
        {
            _currentStateIndex++;
            
            if (CanEnterNextState == false)
                return;
            
            _states[_currentStateIndex].Value.Enter(_target);

            if (_states[_currentStateIndex].Value is DisableTargetState) 
                Finished?.Invoke();
        }
    }
}