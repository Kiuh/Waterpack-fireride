using UnityEngine;

namespace Common
{
    [AddComponentMenu("Common.CameraController")]
    internal class CameraController : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;

        [SerializeField]
        private float lerpSpeed;

        private void LateUpdate()
        {
            Vector3 targetPosition = target.transform.position;
            targetPosition.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed);
        }
    }
}
