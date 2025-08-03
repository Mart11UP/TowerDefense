using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Player
{
    public class PlayerLost : MonoBehaviour
    {
        public static event Action OnTowerDestroyed;

        public void Lost()
        {
            OnTowerDestroyed?.Invoke();
        }
    }
}
