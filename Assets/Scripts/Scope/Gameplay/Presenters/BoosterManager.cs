using ProjectMM.Scope.Gameplay.Boosters;
using UnityEngine;
using VContainer;

namespace ProjectMM.Scope.Gameplay.Presenters
{
    public class BoosterManager : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private BoosterView _BoosterPrefab;

        #endregion

        [Inject] private BoosterController _boosterController;

        private void Awake()
        {
            var index = 0;
            foreach (var booster in _boosterController.GetAvailableBoosters())
            {
                var boosterView = Instantiate(_BoosterPrefab, transform);
                var viewTransform = (RectTransform)boosterView.transform;
                viewTransform.anchoredPosition = new Vector2(viewTransform.rect.width * index + (80 * index), 0);
                index++;

                boosterView.Initialize(booster);
            }
        }
    }
}