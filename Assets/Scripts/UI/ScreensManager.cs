using UnityEngine;
using Tower.Waves;

namespace Tower.UI
{
    public class ScreensManager : MonoBehaviour
    {
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject lostScreen;

        private void OnEnable()
        {
            GameManager.OnPlayerLost += DisplayLostScreen;
            GameManager.OnPlayerWin += DisplayWinScreen;
        }

        private void OnDisable()
        {
            GameManager.OnPlayerLost -= DisplayLostScreen;
            GameManager.OnPlayerWin -= DisplayWinScreen;
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
