namespace Assets.Scripts.Infracructure.Data
{
    public class PlayerProgress
    {
        public int Gems { get; private set; }
        public int Score;

        public PlayerProgress(int gems)
        {
            Gems = gems;
            Score = 0;
        }

        public void CalculateGemsFromScore()
        {
            Gems = Score / 10;
        }

        public void AddScore() =>
            Score++;
    }
}
