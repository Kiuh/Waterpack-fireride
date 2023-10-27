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

        [SerializeField]
        private string baseLevelSceneName;

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

        public void LoadLevel(uint levelNumber)
        {
            SceneManager.LoadScene(baseLevelSceneName + levelNumber);
        }
    }
}
