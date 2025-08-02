using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tower.Data;
using Tower.Generic;
using System;

namespace Tower.Waves
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private WaveData waveData;
        [SerializeField] private float waitTimeBetweenWaves = 10;
        public static event Action<WaveData.SingleWaveData> OnWaveStarted;
        public static event Action<WaveData.SingleWaveData> OnWaveFinished;
        public static event Action OnLevelFinished;
        private RandomSpawner enemySpawner;

        // Start is called before the first frame update
        void Start()
        {
            enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<RandomSpawner>();
            StartCoroutine(WavesRoutine());
        }

        private IEnumerator WavesRoutine()
        {
            foreach(WaveData.SingleWaveData wave in waveData.wavesData)
            {
                OnWaveStarted?.Invoke(wave);

                yield return new WaitForSeconds(wave.StartDelay);

                enemySpawner.StartSpawner(wave.enemySpawnData, wave.minWaitTime, wave.maxWaitTime);

                yield return new WaitForSeconds(wave.Duration);

                enemySpawner.StopSpawner();

                yield return new WaitForSeconds(waitTimeBetweenWaves);

                OnWaveFinished?.Invoke(wave);
            }

            OnLevelFinished?.Invoke();
        }
    }
}
