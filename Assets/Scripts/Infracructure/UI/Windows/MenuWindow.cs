using Assets.Scripts.Infracructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Infracructure.UI.Windows
{
    public class MenuWindow : MonoBehaviour
    {
        public TextMeshProUGUI GemsCountText;
        public TextMeshProUGUI ScoreCountText;
        public GameObject MainMenu;

        private IProgressService _progress;

        public void Construct(IProgressService progressService)
        {
            _progress = progressService;
        }
        public void RefreshGemsText()
        {
            GemsCountText.text = _progress.Progress.Gems.ToString();
        }

        public void RefreshScoreText()
        {
            ScoreCountText.text = _progress.Progress.Score.ToString();
        }

        public void HideMainMenuWindow()
        {
            MainMenu.gameObject.SetActive(false);
        }
    }
}