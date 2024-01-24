using Sources.Infrastructure.Factory;
using Sources.Infrastructure.States;
using Sources.Logic.UI.Services.Windows;
using Sources.Logic.UI.Window;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Logic
{
    public class GameFinish : MonoBehaviour
    {
        private IWindowService _windowService;
        private LoseWindow _loseWindow;
        private WinWindow _winWindow;

        private ResultWindow _currentResultWindow;
        private ShopWindow _shopWindow;
        private IGameStateMachine _gameStateMachine;

        public void Construct(IWindowService windowService, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
        }

        public void Finish(bool isWin)
        {
            isWin = true;
            
            if (isWin)
            {
                _winWindow = _windowService.Create<WinWindow>();
                _currentResultWindow = _winWindow;
                SubscribeResultWindow(_currentResultWindow);
            }
            else
            {
                _loseWindow = _windowService.Create<LoseWindow>();
                _loseWindow.RestartButtonClicked += OnRestartButtonClicked;
                
                _currentResultWindow = _loseWindow;
                SubscribeResultWindow(_currentResultWindow);
            }

            _shopWindow = _windowService.Create<ShopWindow>();
            _shopWindow.Hide();
        }

        private void OnRestartButtonClicked()
        {
            _loseWindow.RestartButtonClicked -= OnRestartButtonClicked;
            _gameStateMachine.Enter<LoadLevelState,string>(SceneManager.GetActiveScene().name);
        }

        private void SubscribeResultWindow(ResultWindow window)
        {
            window.MapClicked += OnMapClicked;
            window.ShopClicked += OnShopClicked;
        }

        private void UnSubscribeResultWindow(ResultWindow window)
        {
            window.MapClicked -= OnMapClicked;
            window.ShopClicked -= OnShopClicked; 
        }

        private void OnShopContinueClicked()
        {
            _shopWindow.Hide();
            _currentResultWindow.Show();
        }

        private void OnShopClicked(ResultWindow resultWindow)
        {
            resultWindow.Hide();
            _shopWindow.Show();
            _shopWindow.ContinueClicked += OnShopContinueClicked;
        }

        private void OnMapClicked(ResultWindow window)
        {
            UnSubscribeResultWindow(window);
            _gameStateMachine.Enter<LoadLevelMapState>();
        }
    }
}