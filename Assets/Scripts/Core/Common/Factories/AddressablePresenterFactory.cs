using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Common.Presenter;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace ProjectMM.Core.Common.Factories
{
    [UnityEngine.Scripting.Preserve]
    public class AddressablePresenterFactory : IPresenterFactory
    {
        public async UniTask<IPresenter> CreateAsync(string address, Transform parent, IObjectResolver container, CancellationToken token)
        {
            var handle = Addressables.InstantiateAsync(address, parent);

            try
            {
                var instance = await handle.ToUniTask(cancellationToken: token);
                var presenter = instance.GetComponent<IPresenter>();
                if (presenter == null)
                {
                    throw new Exception($"Couldn't create the presenter. address: {address}");
                }

                container.InjectGameObject(instance);

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