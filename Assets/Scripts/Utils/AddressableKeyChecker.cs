using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace ProjectMM.Utils
{
    public static class AddressableKeyChecker
    {
        public static async UniTask<bool> KeyExistsAsync(string key)
        {
            var handle = Addressables.LoadResourceLocationsAsync(key);
            try
            {
                var locations = await handle.ToUniTask();
                return locations != null && locations.Count > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                Addressables.Release(handle);
            }
        }
    }
}