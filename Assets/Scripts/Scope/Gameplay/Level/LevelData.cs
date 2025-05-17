using UnityEngine;

namespace ProjectMM.Scope.Gameplay.Level
{
    public class LevelData : ScriptableObject
    {
        #region Inspector

        [SerializeField] private LevelModel _Data;

        #endregion

        public LevelModel Data => _Data;
    }
}