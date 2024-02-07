using UnityEngine;
using UnityEngine.InputSystem;

namespace Hotiovip.YAFPSController
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;
        [Space]
        [SerializeField]
        private Transform swayHolder;

        private PlayerInput playerInput;
        private Item[] items;

        private void Start()
        {
            playerInput = playerController.GetPlayerInput();
        }

        public PlayerController GetPlayerController() => playerController;
        public Transform GetSwayHolder() => swayHolder;
    }
}
