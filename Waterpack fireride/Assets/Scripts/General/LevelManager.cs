using Common;
using Configs;
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

        [SerializeField]
        [InspectorReadOnly]
        private Level currentLevel;
        public Level CurrentLevel
        {
            get => currentLevel;
            set => currentLevel = value;
        }

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

        public void LoadLevel(Level level)
        {
            currentLevel = level;
            SceneManager.LoadScene(level.SceneName);
        }

        public void RestartCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
