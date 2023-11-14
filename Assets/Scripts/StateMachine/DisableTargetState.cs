using UnityEngine;

namespace StateMachine
{
    public class DisableTargetState : MonoBehaviour, IState<Target>
    {
        public void Enter(Target target)
        {
            target.Disable();
        }
    }
}