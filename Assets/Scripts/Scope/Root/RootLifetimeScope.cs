using ProjectMM.Core.Common.Factories;
using ProjectMM.Core.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ProjectMM.Scope.Root
{
    public class RootLifetimeScope : LifetimeScope
    {
        #region Inspector

        [SerializeField] private ItemPrototypeSpawner _ItemPrototypeSpawner;

        #endregion
        
        protected override void Configure(IContainerBuilder builder)
        {
            var itemManager = Instantiate(_ItemPrototypeSpawner);
            DontDestroyOnLoad(itemManager);

            builder.Register<IPlayerRepositoryService, PlayerRepositoryService>(Lifetime.Singleton);
            builder.Register<IPresenterFactory, AddressablePresenterFactory>(Lifetime.Singleton);

            builder.RegisterInstance(itemManager).AsSelf().AsImplementedInterfaces();
        }
    }
}