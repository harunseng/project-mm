using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ProjectMM.Scope.Home
{
    public class HomeLifetimeScope : LifetimeScope
    {
        #region Inspector

        [SerializeField] private HomeSceneInitializer _HomeSceneInitializer;

        #endregion

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_HomeSceneInitializer);
        }
    }
}