using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ProjectMM.Scope.Gameplay
{
    public class GameplayLifeTimeScope : LifetimeScope
    {
        #region Inspector

        [SerializeField] private GameplaySceneInitializer _GameplaySceneInitializer;

        #endregion

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_GameplaySceneInitializer);
        }
    }
}