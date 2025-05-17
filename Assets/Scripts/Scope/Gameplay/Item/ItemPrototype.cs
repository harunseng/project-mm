using UnityEngine;

namespace ProjectMM.Scope.Gameplay.Item
{
    public class ItemPrototype : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private MeshFilter _MeshFilter;
        [SerializeField] private MeshCollider _MeshCollider;

        #endregion

        public PrototypeData.ItemType Type { get; private set; }

        public void UpdateItem(PrototypeData data)
        {
            _MeshFilter.sharedMesh = data.Filter;
            _MeshCollider.sharedMesh = data.Collider;
            Type = data.Type;
        }
    }
}