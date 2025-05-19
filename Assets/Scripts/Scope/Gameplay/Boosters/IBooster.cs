using System.Threading;
using Cysharp.Threading.Tasks;

namespace ProjectMM.Scope.Gameplay.Boosters
{
    public interface IBooster
    {
        public string Name { get; }

        public UniTask Execute(CancellationToken token);
        public void Complete();
    }
}
