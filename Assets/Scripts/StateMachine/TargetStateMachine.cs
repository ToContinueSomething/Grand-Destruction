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

        private bool _isEnable;
        public event Action Finished;
        
        private int _currentStateIndex;
        private bool CanEnterNextState => _currentStateIndex < _states.Count;

        public void Enable()
        {
            _isEnable = true;
        }

        public void Disable()
        {
            _isEnable = false;
        }
        
        public void EnterState()
        {
            if(_isEnable == false)
                return;
            
            _states[_currentStateIndex].Value.Enter(_target);
        }

        public void NextState()
        {
            if(_isEnable == false)
                return;
            
            _currentStateIndex++;
            
            if (CanEnterNextState == false)
                return;
            
            _states[_currentStateIndex].Value.Enter(_target);

            if (_states[_currentStateIndex].Value is DisableTargetState) 
                Finished?.Invoke();
        }
    }
}