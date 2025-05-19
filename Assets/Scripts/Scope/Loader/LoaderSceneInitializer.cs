using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Constants;
using ProjectMM.Core.Scene;
using UnityEngine;

namespace ProjectMM.Scope.Loader
{
    public class LoaderSceneInitializer : MonoBehaviour, ISceneInitializer
    {
        #region Inspector

        [SerializeField] private LoaderView _LoaderView;

        #endregion

        private ISceneOptions _options;
        
        public UniTask InitializeAsync(CancellationToken token, ISceneOptions options = null, IProgress<float> progress = null)
        {
            _options = options;
            return UniTask.CompletedTask;
        }

        public void OnSceneActivated()
        {
            if (_options is not LoaderSceneOptions loaderSceneOptions)
            {
                return;
            }

            if (loaderSceneOptions.SceneName == GameConstants.SceneNames.Gameplay)
            {
                _LoaderView.LoadGameplayScene();
            }
            else
            {
                _LoaderView.LoadHomeScene();
            }
        }
    }
}