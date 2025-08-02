using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Player
{
    public class PlayerLost : MonoBehaviour
    {
        public static event Action OnPlayerLost;

        public void Lost()
        {
            OnPlayerLost.Invoke();
        }
    }
}
