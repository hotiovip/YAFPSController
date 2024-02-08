using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hotiovip.YAFPSController
{
    /// <summary>
    /// Controls the inventory system, like: weapon swapping, usables, etc...
    /// </summary>
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        [Required]
        private PlayerController playerController;
        [Space]
        [SerializeField]
        [Required]
        private Transform swayHolder;
        [SerializeField]
        [Required]
        private Transform positionHolder;

        private PlayerInput playerInput;
        private Item[] items;
        private Transform bulletsHolder;

        private void Start()
        {
            playerInput = playerController.GetPlayerInput();

            // Create an empty game object that does not move and holds all the bullets shot for a less messy workspace
            bulletsHolder = new GameObject("BulletsHolder").transform;
        }

        public PlayerController GetPlayerController() => playerController;
        public Transform GetBulletsHolder() => bulletsHolder;
        public Transform GetPositionHolder() => positionHolder;
        public Transform GetSwayHolder() => swayHolder;
    }
}
