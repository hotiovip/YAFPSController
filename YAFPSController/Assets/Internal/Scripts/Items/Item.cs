using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using Hotiovip.YAFPSController.Utils;
using Quaternion = UnityEngine.Quaternion;

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

        // Look Sway
        /// <summary>
        /// Vector2 used to store mouse delta
        /// </summary>
        protected Vector2 lookInput;
        protected float lookX;
        protected float lookY;
        protected float lookZ;

        // Movement Sway
        /// <summary>
        /// Vector2 used to store movement vector.
        /// </summary>
        protected Vector3 moveInput;
        protected float moveX;
        protected float moveY;
        protected float moveZ;

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
            UpdateProceduralAnimations();  
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
        
        protected virtual void UpdateProceduralAnimations()
        {
            lookSwayInterpolator.RotationSmoothDamp(CalculateLookSway(), itemData.swayConfig.lookSwaySmoothTime, InterpolationSpace.Local);
            movementSwayInterpolator.RotationSmoothDamp(CalculateMovementSway(), itemData.swayConfig.movementSwaySmoothTime, InterpolationSpace.Local);

            weaponBoneIKTransform.SetPositionAndRotation(finalTransform.position, finalTransform.rotation);
        }
        /// <summary>
        /// Calculates and applies look sway, caused by moving the mouse (looking around).
        /// </summary>
        protected virtual Quaternion CalculateLookSway()
        {
            if (!itemData.swayConfig.canLookSway) return Quaternion.identity;

            // Calculate the sway movement based on mouse input
            // left-right
            lookX += lookInput.x * itemData.swayConfig.lookSwayDirection.x * itemData.swayConfig.lookSwayForce.x;
            // up-down
            lookY += lookInput.y * itemData.swayConfig.lookSwayDirection.y * itemData.swayConfig.lookSwayForce.y;
            // on it self
            lookZ += lookInput.x * itemData.swayConfig.lookSwayDirection.z * itemData.swayConfig.lookSwayForce.z;

            // Clamp the results between min and max
            lookX = Mathf.Clamp(lookX, itemData.swayConfig.minLookSwayVector.x, itemData.swayConfig.maxLookSwayVector.x);
            lookY = Mathf.Clamp(lookY, itemData.swayConfig.minLookSwayVector.y, itemData.swayConfig.maxLookSwayVector.y);
            lookZ = Mathf.Clamp(lookZ, itemData.swayConfig.minLookSwayVector.z, itemData.swayConfig.maxLookSwayVector.z);

            // Reset the forces if the move axis is 0
            if (lookInput.x == 0)
            {
                lookX = 0;
                lookZ = 0;
            }
            if (lookInput.y == 0)
            {
                lookY = 0;
            }

            // Transform the rotations from Vector3s to Quaternions
            Quaternion swayRotationX = Quaternion.AngleAxis(lookX, Vector3.up);
            Quaternion swayRotationY = Quaternion.AngleAxis(lookY, Vector3.right);
            Quaternion swayRotationZ = Quaternion.AngleAxis(lookZ, Vector3.forward);

            // Summ all the rotations together
            return swayRotationX * swayRotationY * swayRotationZ;

            // Apply rotation
            //lookSwayInterpolator.RotationSmoothDamp(targetRotation, itemData.swayConfig.lookSwaySmoothTime, InterpolationSpace.Local);
        }
        /// <summary>
        /// Calculates and applies the movement sway, caused by moving around.
        /// </summary>
        protected virtual Quaternion CalculateMovementSway()
        {
            if (!itemData.swayConfig.canMovementSway) return Quaternion.identity;

            // Calculate the sway movement based on mouse input
            // left-right
            moveX += moveInput.x * itemData.swayConfig.movementSwayDirection.x * itemData.swayConfig.movementSwayForce.x;
            // up-down
            moveY += moveInput.y * itemData.swayConfig.movementSwayDirection.y * itemData.swayConfig.movementSwayForce.y;
            // on it self
            moveZ += moveInput.x * itemData.swayConfig.movementSwayDirection.z * itemData.swayConfig.movementSwayForce.z;

            // Clamp the results between min and max
            moveX = Mathf.Clamp(moveX, itemData.swayConfig.minMovementSwayVector.x, itemData.swayConfig.maxMovementSwayVector.x);
            moveY = Mathf.Clamp(moveY, itemData.swayConfig.minMovementSwayVector.y, itemData.swayConfig.maxMovementSwayVector.y);
            moveZ = Mathf.Clamp(moveZ, itemData.swayConfig.minMovementSwayVector.z, itemData.swayConfig.maxMovementSwayVector.z);
            
            // Reset the forces if the move axis is 0
            if (moveInput.x == 0)
            {
                moveX = 0;
                moveZ = 0;
            }
            if (moveInput.y == 0)
            {
                moveY = 0;
            }

            // Transform the rotations from Vector3s to Quaternions
            Quaternion swayRotationX = Quaternion.AngleAxis(moveX, Vector3.up);
            Quaternion swayRotationY = Quaternion.AngleAxis(moveY, Vector3.right);
            Quaternion swayRotationZ = Quaternion.AngleAxis(moveZ, Vector3.forward);

            // Summ all the rotations together
            return swayRotationX * swayRotationY * swayRotationZ;

            // Apply rotation
            //movementSwayInterpolator.RotationSmoothDamp(targetRotation, itemData.swayConfig.movementSwaySmoothTime, InterpolationSpace.Local);
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
            moveInput = callbackContext.ReadValue<Vector2>();
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
