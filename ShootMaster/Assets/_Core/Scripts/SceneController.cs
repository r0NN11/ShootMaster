using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Core.Scripts
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController _instance;

        public static SceneController Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject container = new GameObject("SceneController");
                    _instance = container.AddComponent<SceneController>();
                }

                return _instance;
            }
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}