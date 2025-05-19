using System.Collections.Generic;
using ProjectMM.Scope.Gameplay.Item;
using ProjectMM.Scope.Gameplay.Level;
using UnityEngine;
using VContainer;

namespace ProjectMM.Scope.Gameplay.Presenters
{
    public class OrderManager : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private OrderView _OrderPrefab;

        #endregion

        [Inject] private BoardTracker _boardTracker;
        [Inject] private ItemPrototypeDataProvider _prototypeDataProvider;

        private readonly Dictionary<PrototypeData.ItemType, OrderView> _orderViews = new();

        private void Awake()
        {
            var index = 0;
            foreach (var order in _boardTracker.Orders)
            {
                var orderView = Instantiate(_OrderPrefab, transform);
                _orderViews.Add(order.Key, orderView);
                var viewTransform = (RectTransform)orderView.transform;
                viewTransform.anchoredPosition = new Vector2(viewTransform.rect.width * index + (20 * index), 0);
                index++;

                if (!_prototypeDataProvider.PrototypeData.TryGetValue(order.Key, out var prototypeData))
                {
                    continue;
                }
                orderView.Initialize(order.Value, prototypeData.Sprite);
            }
        }

        private void OnEnable()
        {
            _boardTracker.OrderCountChanged += OnOrderCountChanged;
        }

        private void OnDisable()
        {
            _boardTracker.OrderCountChanged -= OnOrderCountChanged;
        }

        private void OnOrderCountChanged(PrototypeData.ItemType type, int count)
        {
            _orderViews[type].SetOrderCount(count);
        }
    }
}