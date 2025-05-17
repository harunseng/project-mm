using System;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Common.Presenter;
using ProjectMM.Core.Constants;
using ProjectMM.Core.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectMM.Scope.Home
{
    public class HomePresenter : Presenter
    {
        #region Inspector

        [SerializeField] private Button _StartGameButton;

        #endregion

        private void OnEnable()
        {
            _StartGameButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnDisable()
        {
            _StartGameButton.onClick.RemoveListener(OnStartButtonClicked);
        }

        private async void OnStartButtonClicked()
        {
            try
            {
                await SceneLoader.LoadSceneAsync(GameConstants.SceneNames.Loader, this.GetCancellationTokenOnDestroy()).SuppressCancellationThrow();
            }
            catch (Exception e)
            {
            }
        }
    }
}