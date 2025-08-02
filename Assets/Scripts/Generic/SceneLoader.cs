using UnityEngine.SceneManagement;
using UnityEngine;

namespace Tower.Generic
{
    public class SceneLoader : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
