using UnityEngine;
using UnityEngine.InputSystem;

namespace Hotiovip.YAFPSController
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;

        private PlayerInput playerInput;
        private Item[] items;

        private void Start()
        {
            playerInput = playerController.GetPlayerInput();
        }

        public PlayerController GetPlayerController() => GetPlayerController();
    }
}
