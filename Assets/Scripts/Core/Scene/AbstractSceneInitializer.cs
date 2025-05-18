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
        [Inject] protected IObjectResolver Container;

        public abstract UniTask InitializeAsync(CancellationToken token, ISceneOptions options = null, IProgress<float> progress = null);

        public virtual void OnSceneActivated()
        {
        }
    }
}