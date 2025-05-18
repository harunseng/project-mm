using ProjectMM.Scope.Gameplay.Item;
using ProjectMM.Scope.Gameplay.Level;
using ProjectMM.Scope.Gameplay.Presenters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ProjectMM.Scope.Gameplay
{
    public class GameplayLifeTimeScope : LifetimeScope
    {
        #region Inspector

        [SerializeField] private GameplaySceneInitializer _GameplaySceneInitializer;
        [SerializeField] private GameplayManager _GameplayManager;
        [SerializeField] private ItemPrototypeDataProvider _ItemPrototypeDataProvider;

        #endregion

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<BoardTracker>(Lifetime.Scoped);
            builder.Register<LevelLoader>(Lifetime.Scoped);
            builder.Register<ItemSlotsController>(Lifetime.Scoped);
            builder.Register<LevelTimer>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
    
            builder.RegisterComponent(_GameplaySceneInitializer);
            builder.RegisterComponent(_GameplayManager);
            builder.RegisterComponent(_ItemPrototypeDataProvider);
        }
    }
}