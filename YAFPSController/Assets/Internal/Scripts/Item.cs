using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Hotiovip.YAFPSController
{
    public class Item : MonoBehaviour
    {
        protected InventoryController inventoryController;
        protected PlayerController playerController;
        protected PlayerInput playerInput;

        protected void OnEnable()
        {
            playerInput.onActionTriggered += OnActionTriggered;
        }
        protected void OnDisable()
        {
            playerInput.onActionTriggered -= OnActionTriggered;
        }
        protected void Start()
        {
            inventoryController = GetComponentInParent<InventoryController>();
            playerController = inventoryController.GetPlayerController();
            playerInput = playerController.GetPlayerInput();
        }

        protected void PrimaryUse()
        {

        }
        protected void SecondaryUse()
        {

        }
        #region INPUTS
        public void OnActionTriggered(CallbackContext context)
        {
            // Check wich action to call
            switch (context.action.name)
            {
                case "Move":
                    OnMove(context);
                    break;
                case "Look":
                    OnLook(context);
                    break;
                case "Jump":
                    OnJump(context);
                    break;
                case "Run":
                    OnRun(context);
                    break;

                case "Primary Use":
                    OnPrimaryUse(context); 
                    break;
                case "Secondary Use":
                    OnSecondaryUse(context);
                    break;
            }
        }

        private void OnMove(CallbackContext callbackContext)
        {
            // Update vector
            //moveInput = callbackContext.ReadValue<Vector2>();
        }
        private void OnLook(CallbackContext callbackContext)
        {
            // Update vector
            //lookInput = callbackContext.ReadValue<Vector2>();
        }
        private void OnJump(CallbackContext callbackContext)
        {
            //if (callbackContext.performed) Jump();
        }
        private void OnRun(CallbackContext callbackContext)
        {
            //if (callbackContext.performed) StartRun();
            //else if (callbackContext.canceled) StopRun();
        }
        private void OnPrimaryUse(CallbackContext callbackContext)
        {

        }
        private void OnSecondaryUse(CallbackContext callbackContext)
        {

        }
        #endregion
    }
}
