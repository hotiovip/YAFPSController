using Hotiovip.YAFPSController.Utils;
using Sirenix.Utilities.Editor;
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
        protected bool isUsingPrimary;
        protected bool isUsingSecondary;
        protected bool isPerformingAction;
        #endregion

        protected virtual void Awake()
        {
            // Get important references
            inventoryController = GetComponentInParent<InventoryController>();
            playerController = inventoryController.GetPlayerController();
            playerInput = playerController.GetPlayerInput();
        }
        protected virtual void OnEnable()
        {
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

            if (isUsingSecondary) SecondaryUse();

            UpdateSway();
        }

        /// <summary>
        /// Handles the logic to decide if call PrimaryUse() or not.
        /// Can be overridden for custom logic.
        /// </summary>
        protected virtual void UpdatePrimaryUse()
        {
            if (!CanPrimaryUse()) return;

            if (isUsingPrimary) PrimaryUse();
        }
        protected virtual void StartPrimaryUse()
        {
            isUsingPrimary = true;

            PrimaryUse();
        }
        protected virtual void PrimaryUse()
        {

        }
        protected virtual void StopPrimaryUse()
        {
            isUsingPrimary = false;
        }

        protected virtual void UpdateSecondaryUse()
        {
            if (isUsingSecondary) SecondaryUse();
        }
        protected virtual void StartSecondaryUse()
        {

        }
        protected virtual void SecondaryUse()
        {

        }
        protected virtual void StopSecondaryUse()
        {
            
        }

        protected virtual void Action()
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


        public virtual bool CanPrimaryUse() => true;
        public virtual bool CanSecondaryUse() => true;
        public virtual bool CanPerformAction() => true;
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
                case "Action":
                    OnAction(context);
                    break;
            }
        }

        protected virtual void OnMove(CallbackContext callbackContext)
        {
            // Update vector
            //moveInput = callbackContext.ReadValue<Vector2>();
        }
        protected virtual void OnLook(CallbackContext callbackContext)
        {
            // Update vector
            lookInput = callbackContext.ReadValue<Vector2>();
        }
        protected virtual void OnJump(CallbackContext callbackContext)
        {
            //if (callbackContext.performed) Jump();
        }
        protected virtual void OnRun(CallbackContext callbackContext)
        {
            //if (callbackContext.performed) StartRun();
            //else if (callbackContext.canceled) StopRun();
        }
        protected virtual void OnPrimaryUse(CallbackContext callbackContext)
        {
            if (callbackContext.performed) StartPrimaryUse();
            else if (callbackContext.canceled) StopPrimaryUse();
        }
        protected virtual void OnSecondaryUse(CallbackContext callbackContext)
        {
            if (callbackContext.performed) StartSecondaryUse();
            else if (callbackContext.canceled) StopSecondaryUse();
        }
        protected virtual void OnAction(CallbackContext context)
        {
            if (context.performed) Action();
        }
        #endregion
    }
}
