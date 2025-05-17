using System;
using System.Collections.Generic;
using ProjectMM.Scope.Gameplay.Item;

namespace ProjectMM.Scope.Gameplay.Level
{
    [Serializable]
    public struct LevelModel
    {
        [Serializable]
        public struct Orders
        {
            public PrototypeData.ItemType type;
            public int count;
        }

        [Serializable]
        public struct Layout
        {
            public PrototypeData.ItemType type;
            public float minVolume;
            public float maxVolume;
            public int count;
        }

        public List<Orders> itemOrders;
        public List<Layout> itemLayouts;
    }
}