using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Scene;
using ProjectMM.Core.Services;
using VContainer;

namespace ProjectMM.Scope.Home
{
    public class HomeSceneInitializer : AbstractSceneInitializer
    {
        private const string HomePresenter = "HomePresenter";

        [Inject] private IPlayerRepositoryService _playerRepository;

        public override async UniTask InitializeAsync(CancellationToken token, ISceneOptions options = null, IProgress<float> progress = null)
        {
            var presenter = await PresenterFactory.CreateAsync(HomePresenter, _SceneSetup, Container, token);
            presenter.SetUICamera(_UICamera);
            presenter.GameObject.SetActive(true);

            var player = _playerRepository.Load();
            if (player.level == 0)
            {
                player.level = 1;
                _playerRepository.Save(player);
            }
        }
    }
}