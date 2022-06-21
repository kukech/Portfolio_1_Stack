using Assets.Scripts.Infracructure.Data;

namespace Assets.Scripts.Infracructure.Services.PersistentProgress
{
    public interface IProgressService : IService
    {
        PlayerProgress Progress { get; set; }

        void LoadProgress();
    }
}