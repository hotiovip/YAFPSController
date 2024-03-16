using Hotiovip.YAFPSController.Attributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Hotiovip.YAFPSController
{
    /// <summary>
    /// Controls the inventory system, like: weapon swapping, usables, etc...
    /// </summary>
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;
        [Title("Holders")]
        [SerializeField]
        private Transform swayHolder;
        [SerializeField]
        private Transform positionHolder;

        private PlayerInput playerInput;
        private List<Item> items;
        private Item currentItem;
        private Transform bulletsHolder;

        private void Awake()
        {
            playerInput = playerController.GetPlayerInput();
        }
        private void OnEnable()
        {
            // Listen for inputs
            playerInput.onActionTriggered += OnActionTriggered;

        }
        private void OnDisable()
        {
            playerInput.onActionTriggered -= OnActionTriggered;
        }
        private void Start()
        {
            // Create an empty game object that does not move and holds all the bullets shot for a less messy workspace
            bulletsHolder = new GameObject("BulletsHolder").transform;

            // Get all the items that should be added to the inventory
            items = new List<Item>();
            foreach (Item item in transform.GetComponentsInChildren(typeof(Item), true))
            {
                items.Add(item);
                item.gameObject.SetActive(false);
            }

            Equip(0);
        }

        private void Equip(int itemIndex)
        {
            Unequip();

            // Set item as current Item
            currentItem = items[itemIndex];

            // Activate item
            currentItem.gameObject.SetActive(true);
        }
        private void EquipPrimary()
        {
            Equip(0);
        }
        private void EquipSecondary()
        {
            Equip(1);
        }
        private void Unequip()
        {
            if (currentItem == null) return;

            currentItem.gameObject.SetActive(false);
            currentItem = null;
        }

        public PlayerController GetPlayerController() => playerController;
        public Transform GetBulletsHolder() => bulletsHolder;
        public Transform GetPositionHolder() => positionHolder;
        public Transform GetSwayHolder() => swayHolder;

        #region INPUTS
        public void OnActionTriggered(CallbackContext context)
        {
            // Check wich action to call
            switch (context.action.name)
            {
                case "Equip Primary":
                    OnEquipPrimary(context);
                    break;
                case "Equip Secondary":
                    OnEquipSecondary(context); 
                    break;

            }
        }

        private void OnEquipPrimary(CallbackContext context)
        {
            if (context.performed) EquipPrimary();
        }
        private void OnEquipSecondary(CallbackContext context)
        {
            if (context.performed) EquipSecondary();
        }
        #endregion
    }
}
