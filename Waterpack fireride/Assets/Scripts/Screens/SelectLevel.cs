using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Screens
{
    [AddComponentMenu("Scripts/Screens.SelectLevel")]
    internal class SelectLevel : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private RectTransform selectLevelRectTransform;

        [SerializeField]
        private CanvasScaler canvasScaler;

        [SerializeField]
        private RectTransform settingsPanel;

        [SerializeField]
        private float minPosition,
            maxPosition,
            slideTime,
            slideDownPercent;

        [SerializeField]
        private float settingPanelMinPosition,
            settingPanelMaxPosition,
            settingPanelSlideTime;

        private float startPositionY,
            startAnchoredPositionY,
            scaleFactor;

        private void Awake()
        {
            scaleFactor = Screen.height / canvasScaler.referenceResolution.y;
        }

        public void ShowSelectLevelPanel()
        {
            _ = LeanTween.moveX(settingsPanel, settingPanelMinPosition, settingPanelSlideTime);
            _ = StartCoroutine(
                HandleMenuSlide(slideTime, selectLevelRectTransform.anchoredPosition.y, maxPosition)
            );
        }

        public void OnDrag(PointerEventData eventData)
        {
            selectLevelRectTransform.anchoredPosition = new Vector2(
                0,
                Mathf.Clamp(
                    startAnchoredPositionY
                        - (startPositionY - (eventData.position.y / scaleFactor)),
                    minPosition,
                    maxPosition
                )
            );
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StopAllCoroutines();
            startPositionY = eventData.position.y / scaleFactor;
            startAnchoredPositionY = selectLevelRectTransform.anchoredPosition.y;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (IsAfterSlideDownPoint())
            {
                _ = LeanTween.moveX(settingsPanel, settingPanelMaxPosition, settingPanelSlideTime);
                _ = StartCoroutine(
                    HandleMenuSlide(
                        slideTime,
                        selectLevelRectTransform.anchoredPosition.y,
                        minPosition
                    )
                );
            }
            else
            {
                _ = StartCoroutine(
                    HandleMenuSlide(
                        slideTime,
                        selectLevelRectTransform.anchoredPosition.y,
                        maxPosition
                    )
                );
            }
        }

        private bool IsAfterSlideDownPoint()
        {
            return selectLevelRectTransform.anchoredPosition.y
                < minPosition * (1 - slideDownPercent);
        }

        private IEnumerator HandleMenuSlide(float slideTime, float startingX, float targetX)
        {
            for (float i = 0; i <= slideTime * 1.05f; i += slideTime / 10)
            {
                selectLevelRectTransform.anchoredPosition = new Vector2(
                    0,
                    Mathf.Lerp(startingX, targetX, i / slideTime)
                );

                yield return new WaitForSecondsRealtime(slideTime / 10);
            }
        }
    }
}
