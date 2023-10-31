using UnityEngine;

namespace Player
{
    [AddComponentMenu("Scripts/Player.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private PlayerImpl player;
    }
}
