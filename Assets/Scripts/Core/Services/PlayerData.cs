using System;

namespace ProjectMM.Core.Services
{
    [Serializable]
    public struct PlayerData
    {
        public int gold;
        public int level;
        public int metaGameIndex;
        public int metaGameProgress;
        public int latestCompletedLevel;
    }
}