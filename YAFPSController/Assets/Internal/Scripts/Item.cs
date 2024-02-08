using Hotiovip.YAFPSController.Utils;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Hotiovip.YAFPSController
{
    /// <summary>
    /// Used to make items. Can be used as a super class to make new specific items.
    /// </summary>
    public class Item : MonoBehaviour
    {
        #region VARIABLES
        /// <summary>
        /// Item's data.
        /// </summary>
        [SerializeField]
        protected ItemData itemData;

        /// <summary>
        /// InventoryController reference. Used to get playerController
        /// </summary>
        protected InventoryController inventoryController;
        /// <summary>
        /// PlayerController reference. Used to get playerInput
        /// </summary>
        protected PlayerController playerController;
        /// <summary>
        /// PlayerInput reference. Used for listening to player inputs
        /// </summary>
        protected PlayerInput playerInput;

        /// <summary>
        /// Vector2 used to store mouse delta
        /// </summary>
        private Vector2 lookInput;

        private Transform positionHolder;

        /// <summary>
        /// Transform to wich the sway is applied
        /// </summary>
        private Transform swayHolder;
        /// <summary>
        /// the velocity parameter used for the sway's smooth damp
        /// </summary>
        private Quaternion swayVelocity;

        // STATES
        protected bool isPrimaryUsing;
        protected bool isSecondaryUsing;
        #endregion

        protected virtual void OnEnable()
        {
            // Get important references
            inventoryController = GetComponentInParent<InventoryController>();
            playerController = inventoryController.GetPlayerController();
            playerInput = playerController.GetPlayerInput();

            // Listen for inputs
            playerInput.onActionTriggered += OnActionTriggered;

            // Set positionHolder's position and rotation
            positionHolder = inventoryController.GetPositionHolder();
            positionHolder.localPosition = itemData.itemPosition;
            positionHolder.localRotation = itemData.itemRotation;

        }
        protected virtual void OnDisable()
        {
            playerInput.onActionTriggered -= OnActionTriggered;
        }
        protected virtual void Start()
        {
            swayHolder = inventoryController.GetSwayHolder();
        }
        protected virtual void Update()
        {
            UpdatePrimaryUse();

            if (isSecondaryUsing) SecondaryUse();

            UpdateSway();
        }

        /// <summary>
        /// Handles the logic to decide if call PrimaryUse() or not.
        /// Can be overridden for custom logic.
        /// </summary>
        protected virtual void UpdatePrimaryUse()
        {
            if (isPrimaryUsing) PrimaryUse();
        }
        protected virtual void StartPrimaryUse()
        {
            if (!itemData.canPrimaryUse) return;

            isPrimaryUsing = true;

            PrimaryUse();
        }
        protected virtual void PrimaryUse()
        {

        }
        protected virtual void StopPrimaryUse()
        {
            if (!itemData.canPrimaryUse) return;

            isPrimaryUsing = false;
        }
        protected virtual void StartSecondaryUse()
        {
            if (!itemData.canSecondaryUse) return;
        }
        protected virtual void SecondaryUse()
        {

        }
        protected virtual void StopSecondaryUse()
        {
            
        }

        private void UpdateSway()
        {
            if (!itemData.canSway) return;

            // Calculate the sway movement based on mouse input
            float moveX = Mathf.Clamp(lookInput.x * itemData.swayVector.y, itemData.minSwayVector.y, itemData.maxSwayVector.y);
            float moveY = Mathf.Clamp(-lookInput.y * itemData.swayVector.x, itemData.minSwayVector.x, itemData.maxSwayVector.x);
            float moveZ = Mathf.Clamp(lookInput.x * itemData.swayVector.z, itemData.minSwayVector.z, itemData.maxSwayVector.z);

            Quaternion swayRotationX = Quaternion.AngleAxis(moveX, Vector3.up);
            Quaternion swayRotationY = Quaternion.AngleAxis(moveY, Vector3.right);
            Quaternion swayRotationZ = Quaternion.AngleAxis(moveZ, Vector3.forward);

            Quaternion targetRotation = swayRotationX * swayRotationY * swayRotationZ;

            swayHolder.localRotation = QuaternionUtil.SmoothDamp(swayHolder.localRotation, targetRotation, ref swayVelocity, itemData.swaySmoothTime * Time.deltaTime);
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
            if (callbackContext.performed) StartSecondaryUse();
            else if (callbackContext.canceled) StopSecondaryUse();
        }
        #endregion
    }
}
