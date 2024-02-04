using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Hotiovip.YAFPSController
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        protected ItemData itemData;

        protected InventoryController inventoryController;
        protected PlayerController playerController;
        protected PlayerInput playerInput;

        private Vector2 lookInput;

        protected virtual void OnEnable()
        {
            playerInput.onActionTriggered += OnActionTriggered;
        }
        protected virtual void OnDisable()
        {
            playerInput.onActionTriggered -= OnActionTriggered;
        }
        protected virtual void Start()
        {
            inventoryController = GetComponentInParent<InventoryController>();
            playerController = inventoryController.GetPlayerController();
            playerInput = playerController.GetPlayerInput();
        }
        protected virtual void Update()
        {

        }


        protected virtual void StartPrimaryUse()
        {
            if (!itemData.canPrimaryUse) return;
        }
        protected virtual void StopPrimaryUse()
        {
            if (!itemData.canPrimaryUse) return;
        }
        protected virtual void SecondaryUse()
        {
            if (!itemData.canSecondaryUse) return;
        }

        private void Sway()
        {
            if (!itemData.canSway) return;

            // Calculate the sway movement based on mouse input
            //float moveX = Mathf.Clamp(lookInput.x * swayAmount, -maxSwayAmount, maxSwayAmount);
            //float moveY = Mathf.Clamp(lookInput.y * swayAmount, -maxSwayAmount, maxSwayAmount);

            // Calculate the target position with weapon sway
            //Vector3 targetSwayPosition = originalPosition + new Vector3(moveX, moveY, 0f);

            // Smoothly interpolate between current position and target position
            //transform.localPosition = Vector3.Lerp(transform.localPosition, targetSwayPosition, Time.deltaTime * smoothFactor);
        }


        #region INPUTS
        public virtual void OnActionTriggered(CallbackContext context)
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
            lookInput = callbackContext.ReadValue<Vector2>();
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
            if (callbackContext.performed) StartPrimaryUse();
            else if (callbackContext.canceled) StopPrimaryUse();
        }
        private void OnSecondaryUse(CallbackContext callbackContext)
        {
            if (callbackContext.performed) SecondaryUse();
        }
        #endregion
    }
}
