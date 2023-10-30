using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    [AddComponentMenu("Scripts/General.LevelManager")]
    internal class LevelManager : MonoBehaviour
    {
        [Scene]
        [SerializeField]
        private string mainScene;

        public static LevelManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void LoadMainScene()
        {
            SceneManager.LoadScene(mainScene);
        }

        public void LoadLevel(string levelSceneName)
        {
            SceneManager.LoadScene(levelSceneName);
        }

        public void RestartCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
