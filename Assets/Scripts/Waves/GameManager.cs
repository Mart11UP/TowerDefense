using System;
using System.Collections;
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
            WaveManager.OnWavesFinished += AllEnemiesDeath;
        }

        private void OnDisable()
        {
            Player.PlayerLost.OnTowerDestroyed -= PlayerLost;
            WaveManager.OnWavesFinished -= AllEnemiesDeath;
        }

        private void AllEnemiesDeath()
        {
            StartCoroutine(WaitUntilAllEnemiesDeath());
        }

        private void PlayerLost()
        {
            SetLevelFinished(OnPlayerLost);
        }

        private IEnumerator WaitUntilAllEnemiesDeath()
        {
            while (GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
                yield return new WaitForSeconds(2);

            SetLevelFinished(OnPlayerWin);
        }

        private void SetLevelFinished(Action finishEvent)
        {
            if (LevelFinished) return;

            LevelFinished = true;
            finishEvent.Invoke();

            Player.PlayerLost.OnTowerDestroyed -= PlayerLost;
            WaveManager.OnWavesFinished -= AllEnemiesDeath;
        }
    }
}
