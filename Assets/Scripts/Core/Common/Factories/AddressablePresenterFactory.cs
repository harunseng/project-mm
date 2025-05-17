using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Common.Presenter;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace ProjectMM.Core.Common.Factories
{
    public class AddressablePresenterFactory : IPresenterFactory
    {
        private readonly IObjectResolver _container;

        public AddressablePresenterFactory(IObjectResolver container)
        {
            _container = container;
        }

        public async UniTask<IPresenter> CreateAsync(string address, Transform parent, CancellationToken token)
        {
            var handle = Addressables.InstantiateAsync(address, parent);

            try
            {
                var instance = await handle.ToUniTask(cancellationToken: token);
                var presenter = instance.GetComponent<IPresenter>();
                if (presenter == null)
                {
                    throw new Exception($"Presenter not found. address: {address}");
                }

                _container.Inject(presenter);

                return presenter;
            }
            catch (Exception e)
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }

                throw;
            }
        }
    }
}