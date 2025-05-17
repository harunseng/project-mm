using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Scene;

namespace ProjectMM.Scope.Home
{
    public class HomeSceneInitializer : AbstractSceneInitializer
    {
        private const string HomePresenter = "HomePresenter";

        public override async UniTask InitializeAsync(CancellationToken token, IProgress<float> progress = null)
        {
            var presenter = await PresenterFactory.CreateAsync(HomePresenter, _SceneSetup, token);
            presenter.SetUICamera(_UICamera);
        }
    }
}