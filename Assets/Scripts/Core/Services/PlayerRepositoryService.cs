using UnityEngine;

namespace ProjectMM.Core.Services
{
    [UnityEngine.Scripting.Preserve]
    public class PlayerRepositoryService : IPlayerRepositoryService
    {
        private const string PlayerDataKey = "PlayerData";
        
        public void Save(PlayerData data)
        {
            PlayerPrefs.SetString(PlayerDataKey, JsonUtility.ToJson(data));;
        }

        public PlayerData Load()
        {
            var data = PlayerPrefs.GetString(PlayerDataKey);
            return string.IsNullOrEmpty(data) ? default : JsonUtility.FromJson<PlayerData>(data);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(PlayerDataKey);
        }
    }
}
