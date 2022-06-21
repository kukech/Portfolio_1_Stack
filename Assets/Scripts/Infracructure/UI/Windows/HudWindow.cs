using Assets.Scripts.Infracructure.Data;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Infracructure.UI.Windows
{
    public class HudWindow : MonoBehaviour
    {
        public TextMeshProUGUI GemsCountText;

        public void RefreshProgress(PlayerProgress progress)
        {
            GemsCountText.text = progress.Gems.ToString();
        }
    }
}