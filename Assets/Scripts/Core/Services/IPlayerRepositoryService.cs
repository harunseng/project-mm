namespace ProjectMM.Core.Services
{
    public interface IPlayerRepositoryService
    {
        public void Save(PlayerData data);
        public PlayerData Load();
        public void Clear();
    }
}
