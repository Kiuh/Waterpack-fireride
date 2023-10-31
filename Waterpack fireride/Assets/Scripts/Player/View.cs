using UnityEngine;

namespace Player
{
    [AddComponentMenu("Player.View")]
    internal class View : MonoBehaviour
    {
        public void Flip()
        {
            transform.rotation = Quaternion.Euler(
                transform.eulerAngles.y == 0 ? Vector3.up * 180 : Vector3.zero
            );
        }
    }
}
