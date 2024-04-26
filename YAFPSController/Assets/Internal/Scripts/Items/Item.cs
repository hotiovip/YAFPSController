using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

using Hotiovip.YAFPSController.Utils;

namespace Hotiovip.YAFPSController.Items
{
    /// <summary>
    /// Base class for creating items. Can be used as a superclass for custom items (e.g., weapons, melee weapons).
    /// </summary>
    public class Item : MonoBehaviour
    {
        #region VARIABLES
        [Tooltip("Item's data.")]
        [SerializeField]
        protected ItemData itemData;
        [Tooltip("Item's animator. Used for playing animations.")]
        [SerializeField]
        protected Animator animator;

        /// <summary>
        /// InventoryController reference. Used to get playerController
        /// </summary>
        protected Inventory inventoryController;
        /// <summary>
        /// PlayerController reference. Used to get playerInput
        /// </summary>
        protected Player playerController;
        /// <summary>
        /// PlayerInput reference. Used for listening to player inputs
        /// </summary>
        protected PlayerInput playerInput;

        /// <summary>
        /// Vector2 used to store mouse delta
        /// </summary>
        protected Vector2 lookInput;

        protected Transform weaponBoneIKTransform;
        protected Transform finalTransform;
        protected TransformInterpolator lookSwayInterpolator;
        protected TransformInterpolator movementSwayInterpolator;

        // STATES
        protected bool isUsingPrimary;
        protected bool isUsingSecondary;
        protected bool isPerformingAction;
        #endregion

        protected virtual void Awake()
        {
            // Get important references
            inventoryController = GetComponentInParent<Inventory>();
            playerController = inventoryController.GetPlayerController();
            playerInput = playerController.GetPlayerInput();

            // Get the interpolators on wich the various movements will be applied
            weaponBoneIKTransform = inventoryController.GetWeaponBoneIKTransform();
            finalTransform = inventoryController.GetFinalTransform();
            lookSwayInterpolator = inventoryController.GetLookSwayInterpolator();
            movementSwayInterpolator = inventoryController.GetMovementSwayInterpolator();
        }
        protected virtual void OnEnable()
        {
            // Listen for inputs
            playerInput.onActionTriggered += OnActionTriggered;

            // Set positionHolder's position and rotation
            //positionHolder.localPosition = itemData.itemPosition;
            //positionHolder.localRotation = itemData.itemRotation;
            //positionHolder.localPosition = itemData.posRotData.defaultPosition;
            //positionHolder.localRotation = itemData.posRotData.defaultRotation;

        }
        protected virtual void OnDisable()
        {
            // Unbind from all events
            playerInput.onActionTriggered -= OnActionTriggered;
        }
        protected virtual void Start()
        {
            
        }
        protected virtual void Update()
        {
            UpdateLookSway();

            weaponBoneIKTransform.SetPositionAndRotation(finalTransform.position, finalTransform.rotation);
        }

        #region PRIMARY USE
        /// <summary>
        /// Controls the decision logic for triggering the primary action of the item.
        /// This method determines whether to execute the PrimaryUse() action based on predefined conditions.
        /// Custom logic can be implemented by overriding this method.
        /// </summary>
        protected virtual void UpdatePrimaryUse()
        {

        }
        /// <summary>
        /// Starts the logic for primary use. It can also not be used. It is needed only if we need looping primary use,
        /// like firing a gun. Can be overriden to add custom logic.
        /// </summary>
        protected virtual void StartPrimaryUse()
        {
            
        }
        /// <summary>
        /// Primary use logic. Can be overriden to add custom logic.
        /// </summary>
        protected virtual void PrimaryUse()
        {

        }
        /// <summary>
        /// Stops the logic for primary use. It can also not be used. It is needed only if we need looping primary use,
        /// like firing a gun. Can be overriden to add custom logic.
        /// </summary>
        protected virtual void StopPrimaryUse()
        {
            
        }
        #endregion

        #region SECONDARY USE
        /// <summary>
        /// Controls the decision logic for triggering the secondary action of the item.
        /// This method determines whether to execute the SecondaryUse() action based on predefined conditions.
        /// Custom logic can be implemented by overriding this method.
        /// </summary>
        protected virtual void UpdateSecondaryUse()
        {

        }
        /// <summary>
        /// Starts the logic for secondary use. It can also not be used. It is needed only if we need looping secondary use,
        /// like aiming a gun. Can be overriden to add custom logic.
        /// </summary>
        protected virtual void StartSecondaryUse()
        {
            
        }
        /// <summary>
        /// Primary use logic. Can be overriden to add custom logic.
        /// </summary>
        protected virtual void SecondaryUse()
        {

        }
        /// <summary>
        /// Stops the logic for secondary use. It can also not be used. It is needed only if we need looping secondary use,
        /// like aiming a gun. Can be overriden to add custom logic.
        /// </summary>
        protected virtual void StopSecondaryUse()
        {
            
        }
        #endregion

        /// <summary>
        /// Action logic. As default this action is bound to the "R" key and it is like a reload.
        /// But can be overriden to be any action.
        /// </summary>
        protected virtual void Action()
        {

        }
        /// <summary>
        /// Action logic that gets triggered at the end of an animation "reload animation".
        /// On a weapon it would add the ammo, so that they appear at the end of the animation.
        /// </summary>
        protected virtual void ActionEnded()
        {

        }

        /// <summary>
        /// Calculates and applies look sway, caused by moving the mouse (looking around).
        /// </summary>
        protected virtual void UpdateLookSway()
        {
            if (!itemData.canLookSway) return;

            // Calculate the sway movement based on mouse input
            // left-right
            float moveX = Mathf.Clamp(lookInput.x * itemData.lookSwayVectorDirection.x * itemData.lookSwayVector.x, itemData.minLookSwayVector.x, itemData.maxLookSwayVector.x);
            // up-down
            float moveY = Mathf.Clamp(lookInput.y * itemData.lookSwayVectorDirection.y * itemData.lookSwayVector.y, itemData.minLookSwayVector.y, itemData.maxLookSwayVector.y);
            // on it self
            float moveZ = Mathf.Clamp(lookInput.x * itemData.lookSwayVectorDirection.z * itemData.lookSwayVector.z, itemData.minLookSwayVector.z, itemData.maxLookSwayVector.z);

            // Transform the rotations from Vector3s to Quaternions
            Quaternion swayRotationX = Quaternion.AngleAxis(moveX, Vector3.up);
            Quaternion swayRotationY = Quaternion.AngleAxis(moveY, Vector3.right);
            Quaternion swayRotationZ = Quaternion.AngleAxis(moveZ, Vector3.forward);

            // Summ all the rotations together
            Quaternion targetRotation = swayRotationX * swayRotationY * swayRotationZ;

            // Apply rotation
            lookSwayInterpolator.RotationSmoothDamp(targetRotation, itemData.lookSwaySmoothTime, InterpolationSpace.Local);
        }

        /// <summary>
        /// Calculates and applies the movement sway, caused by moving around.
        /// </summary>
        protected virtual void UpdateMovementSway()
        {

        }

        #region GETTERS
        public virtual bool CanPrimaryUse() => true;
        public virtual bool CanSecondaryUse() => true;
        public virtual bool CanPerformAction() => true;
        #endregion

        #region ANIMATION CALLBACKS
        /// <summary>
        /// Can be called from the animation callback redirector. After the "action" animation has finished playing.
        /// To trigger custom logic.
        /// </summary>
        public virtual void OnActionEndedCallback()
        {
            
        }
        #endregion

        #region INPUTS
        /// <summary>
        /// Method that gets called from the player input component.
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnActionTriggered(CallbackContext context)
        {
            // With a switch we check if the action name is the one we want and then call the respective method.
            // It is not the best way. But offers flexibility and customization.

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
        
        /// Different methods that handle player input
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
