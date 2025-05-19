using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Core.Services;
using ProjectMM.Scope.Gameplay.Item;
using ProjectMM.Scope.Root;
using ProjectMM.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

namespace ProjectMM.Scope.Gameplay.Level
{
    [UnityEngine.Scripting.Preserve]
    public class LevelLoader
    {
        private const string LevelData = "LevelData_";
        
        private readonly ItemPrototypeSpawner _spawner;
        private readonly IPlayerRepositoryService _playerRepository;
        private readonly ItemPrototypeDataProvider _prototypeDataProvider;
        private readonly BoardTracker _boardTracker;
        private readonly LevelTimer _levelTimer;

        public LevelLoader(IPlayerRepositoryService playerRepository, ItemPrototypeDataProvider prototypeDataProvider, ItemPrototypeSpawner spawner, BoardTracker boardTracker, LevelTimer timer)
        {
            _playerRepository = playerRepository;
            _prototypeDataProvider = prototypeDataProvider;
            _spawner = spawner;
            _boardTracker = boardTracker;
            _levelTimer = timer;
        }

        public async UniTask LoadLevel(CancellationToken token)
        {
            var player = _playerRepository.Load();

            var key = $"{LevelData}{player.level}";
            var isKeyExists = await AddressableKeyChecker.KeyExistsAsync(key);
            if (!isKeyExists)
            {
                player.level -= 1;
                _playerRepository.Save(player);

                key = $"{LevelData}{player.latestCompletedLevel}";
            }
            var handle = Addressables.LoadAssetAsync<LevelData>(key);
            var isCancelled = await handle.ToUniTask(cancellationToken: token).SuppressCancellationThrow();
            if (isCancelled)
            {
                return;
            }

            var levelData = handle.Result;

            foreach (var orders in levelData.Data.itemOrders)
            {
                _boardTracker.AddOrder(orders.type, orders.count);
            }
            foreach (var layout in levelData.Data.itemLayouts)
            {
                if (!_prototypeDataProvider.PrototypeData.TryGetValue(layout.type, out var prototypeData))
                {
                    continue;
                }

                var itemCount = layout.count;
                for (var i = 0; i < itemCount; i++)
                {
                    var item = _spawner.GetItemPrototype();
                    item.UpdateItem(prototypeData);
                    item.gameObject.SetActive(true);
                    item.transform.position = new Vector3(Random.Range(-3, 3), 3 + Random.Range(layout.minVolume, layout.maxVolume), Random.Range(-5, 5));
                    item.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

                    _boardTracker.Items.Add(item);
                }

                _boardTracker.AddItemCount(layout.type, itemCount);
                _levelTimer.SetTimer(levelData.Data.seconds);
            }

            // Since we create positions randomly, some items might overlap. We simulate physics to make these items move away from each other.
            for (var i = 0; i < 10; i++)
            {
                Physics.Simulate(Time.fixedDeltaTime);
            }
        }
    }
}