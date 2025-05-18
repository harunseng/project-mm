using UnityEngine;

namespace ProjectMM.Core.Services
{
    [UnityEngine.Scripting.Preserve]
    public class PlayerRepositoryService : IPlayerRepositoryService
    {
        private const string PlayerDataKey = "PlayerData";

        private PlayerData _cachedData;
        
        public void Save(PlayerData data)
        {
            PlayerPrefs.SetString(PlayerDataKey, JsonUtility.ToJson(data));;
            _cachedData = data;
        }

        public PlayerData Load()
        {
            if (_cachedData.level != 0)
            {
                return _cachedData;
            }

            var data = PlayerPrefs.GetString(PlayerDataKey);
            _cachedData = JsonUtility.FromJson<PlayerData>(data);

            return string.IsNullOrEmpty(data) ? default : _cachedData;
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(PlayerDataKey);
        }
    }
}