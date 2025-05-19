using UnityEngine;

namespace ProjectMM.Utils
{
    public class FitGroundToScreen : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Camera _Camera;
        [SerializeField] private float _ReferenceWidth;
        [SerializeField] private float _ReferenceHeight;

        #endregion

        private void Start()
        {
            FitGround();
        }

        private void FitGround()
        {
            var targetAspect = (float)Screen.width / Screen.height;
            var referenceAspect = _ReferenceWidth / _ReferenceHeight;

            var camHeight = _Camera.orthographicSize * 2f;
            var camWidth = camHeight * targetAspect;

            var scale = transform.localScale;

            if (targetAspect >= referenceAspect)
            {
                scale.x = camWidth;
                scale.y = camWidth * (_ReferenceHeight / _ReferenceWidth);
            }
            else
            {
                scale.y = camHeight;
                scale.x = camHeight * (_ReferenceWidth / _ReferenceHeight);
            }

            transform.localScale = scale;
        }
    }
}