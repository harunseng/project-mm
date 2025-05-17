using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Common.Factories;
using UnityEngine;
using VContainer;

namespace ProjectMM.Core.Scene
{
    public abstract class AbstractSceneInitializer : MonoBehaviour, ISceneInitializer
    {
        #region Inspector

        [SerializeField] protected Transform _SceneSetup;
        [SerializeField] protected Camera _UICamera;

        #endregion

        [Inject] protected IPresenterFactory PresenterFactory;

        public abstract UniTask InitializeAsync(CancellationToken token, IProgress<float> progress = null);

        public virtual void OnSceneActivated()
        {
        }
    }
}