using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ProjectMM.Core.Scene
{
    public interface ISceneInitializer
    {
        public UniTask InitializeAsync(CancellationToken token, IProgress<float> progress = null);
        public void OnSceneActivated();
    }
}

