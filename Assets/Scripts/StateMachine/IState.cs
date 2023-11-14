using UnityEngine;

namespace StateMachine
{
    public interface IState<in T> where T : MonoBehaviour
    {
         void Enter(T  t);
    }
}