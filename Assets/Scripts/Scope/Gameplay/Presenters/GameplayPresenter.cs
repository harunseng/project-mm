using ProjectMM.Core.Common.Presenter;
using ProjectMM.Core.Services;
using ProjectMM.Scope.Gameplay.Item;
using ProjectMM.Scope.Gameplay.Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ProjectMM.Scope.Gameplay.Presenters
{
    public class GameplayPresenter : Presenter
    {
        #region Inspector

        [SerializeField] private TextMeshProUGUI _Level;
        [SerializeField] private TextMeshProUGUI _Timer;
        [SerializeField] private Image _TimerState;
        [SerializeField] private Button _Close;

        [SerializeField] private Transform[] _Slots;

        #endregion

        [Inject] private LevelTimer _levelTimer;
        [Inject] private IPlayerRepositoryService _playerRepository;
        [Inject] private GameplayManager _gameplayManager;

        [Inject]
        private void Initialize(ItemSlotsController itemSlotsController)
        {
            itemSlotsController.SetSlotTransforms(_Slots);

            _Level.SetText($"Level {_playerRepository.Load().level}");
            OnTimerTick(_levelTimer.RemainingSeconds);
        }

        private void OnEnable()
        {
            _levelTimer.TimerTick += OnTimerTick;
            _levelTimer.TimerStateChanged += OnTimerStateChanged;

            _Close.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _levelTimer.TimerTick -= OnTimerTick;
            _levelTimer.TimerStateChanged -= OnTimerStateChanged;

            _Close.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnTimerStateChanged(bool isRunning)
        {
            _TimerState.color = isRunning ? Color.white : Color.red;
        }

        private void OnTimerTick(int seconds)
        {
            _Timer.SetText($"{seconds / 60}:{seconds % 60:00}");
        }

        private void OnCloseButtonClicked()
        {
            _gameplayManager.EndGameplay();
        }
    }
}