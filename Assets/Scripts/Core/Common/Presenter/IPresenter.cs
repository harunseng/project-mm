using UnityEngine;

namespace ProjectMM.Core.Common.Presenter
{
    public interface IPresenter
    {
        public GameObject GameObject { get; }

        public void SetUICamera(Camera uiCamera);
    }
}
