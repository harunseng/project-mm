using System.Collections.Generic;
using UnityEngine;

namespace ProjectMM.Scope.Gameplay.Item
{
    public class ItemPrototypeDataProvider : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private PrototypeData[] _ItemPrototypes;

        #endregion

        public Dictionary<PrototypeData.ItemType, PrototypeData> PrototypeData { get; } = new();
        
        private void Awake()
        {
            foreach (var itemPrototype in _ItemPrototypes)
            {
                PrototypeData.Add(itemPrototype.Type, itemPrototype);
            }
        }
    }
}