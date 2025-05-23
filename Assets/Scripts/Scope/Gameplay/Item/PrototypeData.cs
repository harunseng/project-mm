using UnityEngine;

namespace ProjectMM.Scope.Gameplay.Item
{
    [CreateAssetMenu(fileName = "Assets/Data/Items/PrototypeData.asset", menuName = "Project MM/Item/Prototype Data", order = 0)]
    public class PrototypeData : ScriptableObject
    {
        public enum ItemType
        {
            Backpack, Bomb, Bottle, Dynamite, FryingPan, PiggyBank, Smiley, SoccerBoot, SteeringWheel, WaterMine
        }

        #region Inspector

        [SerializeField] private ItemType _Type;
        [SerializeField] private Mesh _MeshFilter;
        [SerializeField] private Mesh _MeshCollider;
        [SerializeField] private Sprite _Sprite;

        #endregion

        public ItemType Type => _Type;
        public Mesh Filter => _MeshFilter;
        public Mesh Collider => _MeshCollider;
        public Sprite Sprite => _Sprite;
    }
}