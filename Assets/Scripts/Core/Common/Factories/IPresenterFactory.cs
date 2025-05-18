using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Common.Presenter;
using UnityEngine;
using VContainer;

namespace ProjectMM.Core.Common.Factories
{
    public interface IPresenterFactory
    {
        public UniTask<IPresenter> CreateAsync(string address, Transform parent, IObjectResolver container, CancellationToken token);
    }
}