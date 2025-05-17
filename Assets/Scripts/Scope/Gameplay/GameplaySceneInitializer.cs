using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Scene;

namespace ProjectMM.Scope.Gameplay
{
    public class GameplaySceneInitializer : AbstractSceneInitializer
    {
        private const string GameplayPresenter = "GameplayPresenter";

        public override async UniTask InitializeAsync(CancellationToken token, IProgress<float> progress = null)
        {
            await UniTask.Delay(1000, cancellationToken: token);
        }
    }
}