using System;
using UnityEngine;

namespace Tower.Waves
{
    public class GameManager : MonoBehaviour
    {
        public bool LevelFinished { get; private set; } = false;

        public static event Action OnPlayerWin;
        public static event Action OnPlayerLost;

        private void OnEnable()
        {
            Player.PlayerLost.OnTowerDestroyed += PlayerLost;
            WaveManager.OnWavesFinished += PlayerWin;
        }

        private void OnDisable()
        {
            Player.PlayerLost.OnTowerDestroyed -= PlayerLost;
            WaveManager.OnWavesFinished -= PlayerWin;
        }

        private void PlayerWin()
        {
            SetLevelFinished(OnPlayerWin);
        }

        private void PlayerLost()
        {
            SetLevelFinished(OnPlayerLost);
        }

        private void SetLevelFinished(Action finishEvent)
        {
            if (LevelFinished) return;

            LevelFinished = true;
            finishEvent.Invoke();

            Player.PlayerLost.OnTowerDestroyed -= PlayerLost;
            WaveManager.OnWavesFinished -= PlayerWin;
        }
    }
}
