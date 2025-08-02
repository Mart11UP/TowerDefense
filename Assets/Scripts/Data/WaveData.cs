using UnityEngine;
using Tower.Generic;

namespace Tower.Data
{
    [CreateAssetMenu(fileName = "WavesData", menuName = "Tower/Waves Data")]
    public class WaveData : ScriptableObject
    {
        [System.Serializable]
        public struct SingleWaveData
        {
            public string title;
            public float StartDelay;
            public float Duration;
            [Space]
            [Header("Enemy Spawn Wait Time Range")]
            public float minWaitTime;
            public float maxWaitTime;
            [Space]
            public RandomSpawner.SpawnObjectData[] enemySpawnData;
        }

        public SingleWaveData[] wavesData;
    }
}
