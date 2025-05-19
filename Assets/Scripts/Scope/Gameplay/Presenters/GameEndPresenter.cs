using ProjectMM.Core.Common.Presenter;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ProjectMM.Scope.Gameplay.Presenters
{
    public class GameEndPresenter : Presenter
    {
        #region Inspector

        [SerializeField] private Button _Home;

        #endregion
        
        [Inject] private GameplayManager _gameplayManager;

        private void OnEnable()
        {
            _Home.onClick.AddListener(OnHomeButtonClicked);
        }

        private void OnDisable()
        {
            _Home.onClick.RemoveListener(OnHomeButtonClicked);
        }

        private void OnHomeButtonClicked()
        {
            _gameplayManager.EndGameplay();
        }
    }
}