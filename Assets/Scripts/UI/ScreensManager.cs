using UnityEngine;
using Tower.Player;
using Tower.Waves;

namespace Tower.UI
{
    public class ScreensManager : MonoBehaviour
    {
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject lostScreen;

        private void OnEnable()
        {
            PlayerLost.OnPlayerLost += DisplayLostScreen;
            WaveManager.OnLevelFinished += DisplayWinScreen;
        }

        private void OnDisable()
        {
            PlayerLost.OnPlayerLost -= DisplayLostScreen;
            WaveManager.OnLevelFinished -= DisplayWinScreen;
        }

        private void DisplayLostScreen()
        {
            lostScreen.SetActive(true);
        }

        private void DisplayWinScreen()
        {
            winScreen.SetActive(true);
        }
    }
}
