using ProjectMM.Core.Common.Factories;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ProjectMM.Scope.Root
{
    public class RootLifetimeScope : LifetimeScope
    {
        #region Inspector

        [SerializeField] private ItemPrototypeSpawner itemPrototypeSpawner;

        #endregion
        
        protected override void Configure(IContainerBuilder builder)
        {
            var itemManager = Instantiate(itemPrototypeSpawner);
            DontDestroyOnLoad(itemManager);

            builder.Register<IPresenterFactory, AddressablePresenterFactory>(Lifetime.Singleton);

            builder.RegisterInstance(itemManager).AsSelf().AsImplementedInterfaces();
        }
    }
}
