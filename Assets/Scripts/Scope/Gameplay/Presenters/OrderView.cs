using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectMM.Scope.Gameplay.Presenters
{
    public class OrderView : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private TextMeshProUGUI _OrderText;
        [SerializeField] private Image _OrderImage;

        #endregion

        public void Initialize(int count, Sprite sprite)
        {
            _OrderImage.sprite = sprite;
            _OrderText.SetText(count.ToString());
        }

        public void SetOrderCount(int order)
        {
            _OrderText.SetText(order.ToString());

            if (order == 0)
            {
                _OrderText.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}