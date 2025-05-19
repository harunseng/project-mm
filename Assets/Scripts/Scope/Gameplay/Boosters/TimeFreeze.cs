using System.Threading;
using Cysharp.Threading.Tasks;
using ProjectMM.Scope.Gameplay.Level;

namespace ProjectMM.Scope.Gameplay.Boosters
{
    public class TimeFreeze : IBooster
    {
        private const int Duration = 15000;
        
        private readonly LevelTimer _levelTimer;

        public string Name { get; }

        public TimeFreeze(LevelTimer timer)
        {
            Name = "TimeFreeze";

            _levelTimer = timer;
        }

        public async UniTask Execute(CancellationToken token)
        {
            _levelTimer.IsRunning = false;

            await UniTask.Delay(Duration, cancellationToken: token).SuppressCancellationThrow();

            _levelTimer.IsRunning = true;
        }

        public void Complete()
        {
        }
    }
}