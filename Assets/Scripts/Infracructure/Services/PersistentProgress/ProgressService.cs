using Assets.Scripts.Infracructure.Data;
using UnityEngine;

namespace Assets.Scripts.Infracructure.Services.PersistentProgress
{
    public class ProgressService : IProgressService
    {
        private const string GemsKey = "Gems";

        public PlayerProgress Progress { get; set; }

        public void AddScore() =>
            Progress.Score++;

        public void LoadProgress()
        {
            int gems = PlayerPrefs.HasKey(GemsKey) ? PlayerPrefs.GetInt(GemsKey) : 0;
            Progress = new PlayerProgress(gems);
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetInt(GemsKey, Progress.Gems);
        }
    }
}
