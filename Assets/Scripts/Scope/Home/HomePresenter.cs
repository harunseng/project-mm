using System;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Common.Presenter;
using ProjectMM.Core.Constants;
using ProjectMM.Core.Scene;
using ProjectMM.Core.Services;
using ProjectMM.Scope.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace ProjectMM.Scope.Home
{
    public class HomePresenter : Presenter
    {
        #region Inspector

        [SerializeField] private Button _StartGameButton;
        [SerializeField] private Button _ClearPlayerDataButton;

        #endregion

        [Inject] private IPlayerRepositoryService _playerRepository;

        private void OnEnable()
        {
            _StartGameButton.onClick.AddListener(OnStartButtonClicked);
            _ClearPlayerDataButton.onClick.AddListener(OnClearPlayerDataButtonClicked);
        }

        private void OnDisable()
        {
            _StartGameButton.onClick.RemoveListener(OnStartButtonClicked);
            _ClearPlayerDataButton.onClick.RemoveListener(OnClearPlayerDataButtonClicked);
        }

        private async void OnStartButtonClicked()
        {
            try
            {
                await SceneLoader.LoadSceneAsync(GameConstants.SceneNames.Loader, this.GetCancellationTokenOnDestroy(), options: new LoaderSceneOptions { SceneName = GameConstants.SceneNames.Gameplay }).SuppressCancellationThrow();
            }
            catch (Exception)
            {
                //NOOP
            }
        }

        private void OnClearPlayerDataButtonClicked()
        {
            _playerRepository.Reset();
            SceneManager.LoadScene(GameConstants.SceneNames.Splash);
        }
    }
}