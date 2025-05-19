using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ProjectMM.Core.Common.Factories;
using ProjectMM.Core.Constants;
using ProjectMM.Core.Scene;
using ProjectMM.Core.Services;
using ProjectMM.Scope.Gameplay.Item;
using ProjectMM.Scope.Gameplay.Level;
using ProjectMM.Scope.Gameplay.Presenters;
using ProjectMM.Scope.Loader;
using ProjectMM.Scope.Root;
using UnityEngine;
using VContainer;

namespace ProjectMM.Scope.Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Transform _SceneSetup;

        #endregion
        
        [Inject] private IObjectResolver _container;
        [Inject] private IPresenterFactory _presenterFactory;
        [Inject] private ItemPrototypeSpawner _itemPrototypeSpawner;
        [Inject] private ItemSlotsController _slotController;
        [Inject] private BoardTracker _boardTracker;
        [Inject] private LevelTimer _levelTimer;
        [Inject] private IPlayerRepositoryService _playerRepository;

        private const int MatchCount = 3;

        private static int _itemsLayerMask;

        private Camera _camera;
        private bool _isInputDisabled;
        private bool _isGamePaused;

        private void Awake()
        {
            _camera = Camera.main;
            _itemsLayerMask = LayerMask.GetMask("Items");
        }

        private void Start()
        {
            _levelTimer.IsRunning = true;
        }

        private void OnEnable()
        {
            _levelTimer.TimerEnd += OnTimerEnd;
            _boardTracker.OrdersCompleted += OnOrdersCompleted;
        }

        private void OnDisable()
        {
            _levelTimer.TimerEnd -= OnTimerEnd;
            _boardTracker.OrdersCompleted -= OnOrdersCompleted;
        }

        private void FixedUpdate()
        {
            if (_isGamePaused)
            {
                return;
            }

            Physics.Simulate(Time.fixedDeltaTime);
        }

        public void Update()
        {
            if (_slotController.IsSlotsFull && !_isInputDisabled)
            {
                ShowGameOver().Forget();
                return;
            }

            if (Input.GetMouseButtonDown(0) && !_isInputDisabled && !_isGamePaused)
            {
                CheckItem();
            }
        }

        public void EndGameplay()
        {
            DOTween.KillAll();
            _itemPrototypeSpawner.ReleaseAll();

            SceneLoader.LoadSceneAsync(GameConstants.SceneNames.Loader, this.GetCancellationTokenOnDestroy(), options: new LoaderSceneOptions { SceneName = GameConstants.SceneNames.Home }).Forget();
        }

        private void CheckItem()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out var hitInfo, 50, _itemsLayerMask);
            if (!hitInfo.collider)
            {
                return;
            }

            var item = hitInfo.collider.GetComponent<ItemPrototype>();
            var (position, matches) = _slotController.GetAvailableSlot(item);
            var remaining = _boardTracker.GetCount(item.Type);
            var count = matches?.Count;
            var isMatched = (count == MatchCount || count == remaining);
            _isInputDisabled = isMatched;

            item.MoveToSlot(position, isMatched ? () => DestroyMatchedItems(matches) : null);
        }

        private void DestroyMatchedItems(IReadOnlyList<ItemPrototype> items)
        {
            const float offset = 1f;
            var firstPositionX = items[0].transform.position.x;
            var lastPositionX = items[^1].transform.position.x;
            var mergePointX = (firstPositionX + lastPositionX) / 2;

            foreach (var item in items)
            {
                item.MoveToMergePoint(new Vector3(mergePointX, 0, item.transform.position.z + offset), () => _itemPrototypeSpawner.ReleaseItemPrototype(item));
            }
            _boardTracker.RemoveItemCount(items[0].Type, items.Count);
            _slotController.ReleaseLastMatchedSlots();
            DOVirtual.DelayedCall(0.5f, () => _isInputDisabled = false);
        }

        private void OnOrdersCompleted()
        {
            var player = _playerRepository.Load();
            player.latestCompletedLevel = player.level;
            player.level += 1;
            player.gold += 1;
            _playerRepository.Save(player);

            ShowGameOver().Forget();
        }

        private void OnTimerEnd()
        {
            ShowGameOver().Forget();
        }

        private async UniTaskVoid ShowGameOver()
        {
            _isGamePaused = true;
            _levelTimer.IsRunning = false;

            var presenter = await _presenterFactory.CreateAsync("GameEndPresenter", _SceneSetup, _container, this.GetCancellationTokenOnDestroy());
            presenter.SetUICamera(_camera);
            presenter.GameObject.SetActive(true);
        }
    }
}