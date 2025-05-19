using Cysharp.Threading.Tasks;
using ProjectMM.Scope.Gameplay.Boosters;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectMM.Scope.Gameplay.Presenters
{
    public class BoosterView : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Button _BoosterButton;

        #endregion

        private IBooster _booster;

        private void OnEnable()
        {
            _BoosterButton.onClick.AddListener(OnBoosterButtonClicked);
        }

        private void OnDisable()
        {
            _BoosterButton.onClick.RemoveListener(OnBoosterButtonClicked);
        }

        public void Initialize(IBooster booster)
        {
            _booster = booster;
        }

        private void OnBoosterButtonClicked()
        {
            _booster?.Execute(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }
}