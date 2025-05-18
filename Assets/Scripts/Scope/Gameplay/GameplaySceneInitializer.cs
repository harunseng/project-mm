using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Scene;
using ProjectMM.Scope.Gameplay.Level;
using VContainer;

namespace ProjectMM.Scope.Gameplay
{
    public class GameplaySceneInitializer : AbstractSceneInitializer
    {
        private const string GameplayPresenter = "GameplayPresenter";

        [Inject] private LevelLoader _levelLoader;
        [Inject] private GameplayManager _gameplayManager;
        
        public override async UniTask InitializeAsync(CancellationToken token, ISceneOptions options = null, IProgress<float> progress = null)
        {
            await _levelLoader.LoadLevel(this.GetCancellationTokenOnDestroy()).SuppressCancellationThrow();

            var presenter = await PresenterFactory.CreateAsync(GameplayPresenter, _SceneSetup, Container, token);
            presenter.SetUICamera(_UICamera);
            presenter.GameObject.SetActive(true);
        }
    }
}