using System;

namespace Assets.Scripts.Infracructure.Data
{
    public class PlayerProgress
    {
        public int Gems { get; private set; }
        public int Score;

        public Action ScoreChanged;

        public PlayerProgress(int gems)
        {
            Gems = gems;
            Score = 0;
        }

        public void ScoreToGems()
        {

        }

        public void AddScore(int count)
        {
            Score += count;
            ScoreChanged?.Invoke();
        }
    }
}
