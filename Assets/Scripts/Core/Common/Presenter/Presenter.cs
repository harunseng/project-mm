using UnityEngine;

namespace ProjectMM.Core.Common.Presenter
{
    public class Presenter : MonoBehaviour, IPresenter
    {
        #region Inspector

        [SerializeField] private Canvas _Canvas;

        #endregion

        public GameObject GameObject => gameObject;

        public void SetUICamera(Camera uiCamera)
        {
            _Canvas.worldCamera = uiCamera;
        }
    }
}