using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ProjectMM.Core.Scene
{
    public interface ISceneInitializer
    {
        public UniTask InitializeAsync(CancellationToken token, ISceneOptions options = null, IProgress<float> progress = null);
        public void OnSceneActivated();
    }
}