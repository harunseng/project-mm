using ProjectMM.Core.Common.Factories;
using VContainer;
using VContainer.Unity;

namespace ProjectMM.Core.Bootstrapper
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IPresenterFactory, AddressablePresenterFactory>(Lifetime.Singleton);
        }
    }
}
