using System;
using UnityEngine;
using VContainer.Unity;

namespace ProjectMM.Scope.Gameplay.Level
{
    [UnityEngine.Scripting.Preserve]
    public class LevelTimer : ITickable
    {
        public event Action TimerEnd;
        public event Action<int> TimerTick;

        private float _seconds;
        private int _lastWholeSeconds;

        public bool IsRunning { get; set; }
        public int RemainingSeconds => _lastWholeSeconds;

        public void SetTimer(float seconds)
        {
            _seconds = seconds;
            _lastWholeSeconds = Mathf.CeilToInt(_seconds);
        }

        public void Tick()
        {
            if (_seconds <= 0 || !IsRunning)
            {
                return;
            }

            _seconds -= Time.deltaTime;

            var currentWholeSeconds = Mathf.CeilToInt(_seconds);
            if (currentWholeSeconds < _lastWholeSeconds)
            {
                _lastWholeSeconds = currentWholeSeconds;
                TimerTick?.Invoke(currentWholeSeconds);
            }

            if (_seconds <= 0)
            {
                IsRunning = false;
                _seconds = 0;
                TimerEnd?.Invoke();
            }
        }
    }
}