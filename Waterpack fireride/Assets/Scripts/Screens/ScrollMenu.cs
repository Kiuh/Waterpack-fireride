using Configs;
using General;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Screens
{
    internal struct LevelUIElement
    {
        public Level Level;
        public GameObject GameObject;

        public LevelUIElement(Level level, GameObject gameObject)
        {
            Level = level;
            GameObject = gameObject;
        }
    }

    public class ScrollMenu : MonoBehaviour
    {
        [SerializeField]
        private LevelsConfig levelsConfig;

        [SerializeField]
        private LevelManager levelManager;

        [SerializeField]
        private Transform contentTransform;

        [SerializeField]
        private GameObject levelUIElementPrefab;

        [SerializeField]
        private float snapSpeed;

        [SerializeField]
        private TextMeshProUGUI levelText;

        private List<LevelUIElement> levelsUIElements;

        private RectTransform rectTransform;

        private float targetToSnapX;

        private int selectedLevelIndex;
        private bool isSnapEnabled = false;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            levelsUIElements = new List<LevelUIElement>();
            InstantiateLevelsUIElements();
        }

        private void Start()
        {
            int currentLevelIndex = levelsConfig.GetCurrentLevelIndex();
            FindTargetSnapX(levelsUIElements[currentLevelIndex].GameObject);
            contentTransform.position = new Vector2(targetToSnapX, contentTransform.position.y);
            UpdateLevelText(currentLevelIndex);
        }

        private void InstantiateLevelsUIElements()
        {
            int counter = 1;
            foreach (Level level in levelsConfig.Levels)
            {
                GameObject levelUIGameObject = Instantiate(
                    levelUIElementPrefab,
                    contentTransform,
                    false
                );
                levelUIGameObject.GetComponentInChildren<TextMeshProUGUI>().text =
                    counter.ToString();
                levelsUIElements.Add(new LevelUIElement(level, levelUIGameObject));
                counter++;
            }
        }

        private void Update()
        {
            if (isSnapEnabled)
            {
                float contentSnapPositionX = Mathf.SmoothStep(
                    contentTransform.position.x,
                    targetToSnapX,
                    snapSpeed * Time.deltaTime
                );
                contentTransform.position = new Vector2(
                    contentSnapPositionX,
                    contentTransform.position.y
                );
            }
        }

        private void FindNearestLevelUIElement()
        {
            float nearestPos = float.MaxValue;
            for (int i = 0; i < levelsUIElements.Count; i++)
            {
                float distance = Mathf.Abs(
                    rectTransform.anchoredPosition.x
                        - levelsUIElements[i].GameObject.transform.position.x
                );
                if (distance < nearestPos)
                {
                    nearestPos = distance;
                    selectedLevelIndex = i;
                }
            }
            FindTargetSnapX(levelsUIElements[selectedLevelIndex].GameObject);
        }

        public void ScrollBeginDrag()
        {
            isSnapEnabled = false;
        }

        public void ScrollEndDrag()
        {
            FindNearestLevelUIElement();
            UpdateLevelText(selectedLevelIndex);
            isSnapEnabled = true;
        }

        private void FindTargetSnapX(GameObject targetLevelGO)
        {
            targetToSnapX =
                rectTransform.anchoredPosition.x
                - targetLevelGO.transform.position.x
                + contentTransform.position.x;
        }

        private void UpdateLevelText(int index)
        {
            levelText.text =
                (index + 1).ToString() + ". " + levelsUIElements[index].Level.LevelName;
        }

        public void PlaySelectedPress()
        {
            levelManager.LoadLevel(levelsUIElements[selectedLevelIndex].Level);
        }
    }
}
