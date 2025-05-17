using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Scene;
using UnityEngine;

namespace ProjectMM.Scope.Loader
{
    public class LoaderSceneInitializer : MonoBehaviour, ISceneInitializer
    {
        #region Inspector

        [SerializeField] private LoaderView _LoaderView;

        #endregion

        public UniTask InitializeAsync(CancellationToken token, IProgress<float> progress = null)
        {
            return UniTask.CompletedTask;
        }

        public void OnSceneActivated()
        {
            _LoaderView.LoadGameplayScene();
        }
    }
}