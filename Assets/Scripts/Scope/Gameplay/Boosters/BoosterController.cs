using System.Collections.Generic;
using ProjectMM.Core.Services;
using ProjectMM.Scope.Gameplay.Level;
using ProjectMM.Scope.Root;

namespace ProjectMM.Scope.Gameplay.Boosters
{
    [UnityEngine.Scripting.Preserve]
    public class BoosterController
    {
        private readonly IPlayerRepositoryService _playerRepository;

        //INFO: These dependencies won't be needed once we have BoosterFactory and PlayerData
        private readonly BoardTracker _boardTracker;
        private readonly LevelTimer _levelTimer;
        private readonly ItemPrototypeSpawner _spawner;

        public BoosterController(IPlayerRepositoryService playerRepository, BoardTracker boardTracker, LevelTimer levelTimer, ItemPrototypeSpawner spawner)
        {
            _playerRepository = playerRepository;

            _boardTracker = boardTracker;
            _levelTimer = levelTimer;
            _spawner = spawner;
        }

        public IReadOnlyList<IBooster> GetAvailableBoosters()
        {
            // TODO: Get boosters from player data which is not implemented yet create them via BoosterFactory that also is not implemented yet
            return new List<IBooster>
            {
                new Cleaner(_boardTracker, _spawner),
                new TimeFreeze(_levelTimer)
            };
        }
    }
}
