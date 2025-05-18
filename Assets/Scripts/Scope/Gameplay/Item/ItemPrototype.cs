using System;
using DG.Tweening;
using UnityEngine;

namespace ProjectMM.Scope.Gameplay.Item
{
    public class ItemPrototype : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private MeshFilter _MeshFilter;
        [SerializeField] private MeshCollider _MeshCollider;
        [SerializeField] private Rigidbody _Rigidbody;

        #endregion

        public PrototypeData.ItemType Type { get; private set; }

        private void OnDisable()
        {
            _MeshCollider.enabled = true;
            _Rigidbody.isKinematic = false;
            transform.position = Vector3.zero; 
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(3.33f, 3.33f, 3.33f);;
        }

        public void UpdateItem(PrototypeData data)
        {
            _MeshFilter.sharedMesh = data.Filter;
            _MeshCollider.sharedMesh = data.Collider;
            Type = data.Type;
        }

        public void SetPhysicsActive(bool active)
        {
            _MeshCollider.enabled = active;
            _Rigidbody.isKinematic = !active;
        }

        public void MoveToSlot(Vector3 position, Action onCompleted = null)
        {
            const float duration = 0.6f;
            const float halfDuration = duration / 2;

            _MeshCollider.enabled = false;
            _Rigidbody.isKinematic = true;

            var seq = DOTween.Sequence();
            seq.SetId(transform);
            seq.Join(transform.DOMoveX(position.x + 0.45f, duration).SetEase(Ease.OutQuad));
            seq.Join(transform.DOMoveY(3f, duration).SetEase(Ease.OutQuad));
            seq.Join(transform.DOMoveZ(position.z, duration).SetEase(Ease.InQuad));
            seq.Join(transform.DOScale(Vector3.one * 6, duration).SetEase(Ease.OutQuad));
            seq.Join(transform.DORotate(Vector3.zero, duration).SetEase(Ease.InBack));
            seq.Insert(halfDuration,transform.DOScale(Vector3.one, halfDuration).SetEase(Ease.OutQuad));
            seq.Insert(halfDuration,transform.DOMoveY(0.1f, halfDuration).SetEase(Ease.OutQuad));
            seq.OnComplete(() => onCompleted?.Invoke());
            seq.Play();
        }

        public void ShiftNextSlot(Vector3 position)
        {
            DOTween.Kill(transform, true);

            const float offset = 0.45f;
            const float duration = 0.15f;
            var halfDistance = (position.x + offset - transform.position.x) / 2;

            var seq = DOTween.Sequence();
            seq.Join(transform.DOMove(new Vector3 (transform.position.x + halfDistance, transform.position.y, transform.position.z + halfDistance), duration).SetEase(Ease.OutQuad));
            seq.Append(transform.DOMove(new Vector3 (transform.position.x + halfDistance * 2, transform.position.y, transform.position.z), duration).SetEase(Ease.OutQuad));
            seq.Play();
        }

        public void ShitPreviousSlot(Vector3 position, int step)
        {
            DOTween.Kill(transform, true);

            const float offset = 0.45f;
            const float duration = 0.075f;

            var halfDistance = (transform.position.x - (position.x + offset)) / (2 * step);
            var positionX = transform.position.x;
            var seq = DOTween.Sequence();
            for (var i = 0; i < step; i++)
            {
                seq.Append(transform.DOMove(new Vector3 (positionX - halfDistance, transform.position.y, transform.position.z + halfDistance), duration).SetEase(Ease.OutQuad));
                seq.Append(transform.DOMove(new Vector3 (positionX - halfDistance * 2, transform.position.y, transform.position.z), duration).SetEase(Ease.OutQuad));
                positionX = transform.position.x - (halfDistance * 2) * (i + 1);
            }
            seq.Play();
        }

        public void MoveToMergePoint(Vector3 position, Action onCompleted = null)
        {
            DOTween.Kill(transform, true);

            const float duration = 0.4f;

            var seq = DOTween.Sequence();
            seq.Join(transform.DOMoveX(position.x, duration).SetEase(Ease.InCubic));
            seq.Join(transform.DOMoveZ(position.z, duration).SetEase(Ease.OutCubic));
            seq.OnComplete(() => onCompleted?.Invoke());
        }
    }
}