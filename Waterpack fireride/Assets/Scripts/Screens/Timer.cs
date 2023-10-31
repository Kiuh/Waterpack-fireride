using Common;
using TMPro;
using UnityEngine;

namespace Screens
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        [InspectorReadOnly]
        private float playTime = 0f;

        private TextMeshProUGUI timerText;

        private void Start()
        {
            timerText = gameObject.GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        private void Update()
        {
            playTime += Time.deltaTime;
            UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            string minutes = ((int)(playTime / 60)).ToString();
            string seconds = ((int)(playTime - (int)(playTime / 60))).ToString();
            string milliseconds = ((int)(playTime % 1 * 100)).ToString();

            if (seconds.Length < 2)
            {
                seconds = "0" + seconds;
            }

            if (milliseconds.Length < 2)
            {
                milliseconds = "0" + milliseconds;
            }

            timerText.text = minutes + ":" + seconds + ":" + milliseconds;
        }
    }
}
