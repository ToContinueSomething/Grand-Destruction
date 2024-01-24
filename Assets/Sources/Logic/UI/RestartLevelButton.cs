using Sources.Infrastructure.Factory;
using Sources.Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sources.Logic.UI
{
    public class RestartLevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private IGameStateMachine _gameStateMachine;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void OnClicked()
        {
            _gameStateMachine.Enter<LoadLevelState,string>(SceneManager.GetActiveScene().name);
        }
    }
}