using Hotiovip.YAFPSController.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Hotiovip.YAFPSController
{
    /// <summary>
    /// Controls the player. Move, jump, run, etc...
    /// Can be used as base clase to make custom player controllers.
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region VARIABLES
        [Title("References")]
        [SerializeField]
        [DisplayName("Rigidbody")]
        protected Rigidbody rb;
        [SerializeField]
        protected CapsuleCollider capsuleCollider;
        [SerializeField]
        protected PlayerInput playerInput;
        [SerializeField]
        protected Transform playerCamera;

        [Title("Movement Settings")]
        [SerializeField]
        protected LayerMask whatIsGround;
        [SerializeField]
        protected float groundDrag;
        [Space]
        [SerializeField]
        protected float walkSpeed = 3f;
        [SerializeField]
        protected float runSpeed = 4.5f;
        [SerializeField]
        protected float jumpForce = 10f;

        [Title("Input Settings")]
        [SerializeField]
        protected float lookXSensitivity = 1f;
        [SerializeField]
        protected float lookYSensitivity = 1f;

        protected float currentHeight;
        protected float currentSpeed;

        protected Vector2 moveInput;
        protected Vector2 lookInput;
        protected float lookXRotation;
        protected float lookYRotation;

        // States
        protected bool isRunning = false;
        #endregion

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
            // Set default current speed to walk speed
            currentSpeed = walkSpeed;
            currentHeight = capsuleCollider.height;

            // Set the cursor to invisible and locked at the center
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        protected virtual void Update()
        {

        }
        protected virtual void FixedUpdate()
        {
            Look();
            Move();
        }

        protected virtual void Move()
        {
            Vector3 moveVector = (transform.forward * moveInput.y) + (transform.right * moveInput.x);

            if (IsGrounded)
            {
                rb.drag = groundDrag;
            }
            else
            {
                rb.drag = 0f;
            }

            rb.AddForce(moveVector.normalized * currentSpeed * 10f, ForceMode.Force);

            // Clamp movement speed
            Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if (flatVelocity.magnitude > currentSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * currentSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }
        }
        protected virtual void Look()
        {
            float lookInputX = lookInput.x * lookXSensitivity * Time.deltaTime;
            float lookInputY = lookInput.y * lookYSensitivity * Time.deltaTime;

            lookYRotation += lookInputX;
            lookXRotation -= lookInputY;
            lookXRotation = Mathf.Clamp(lookXRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(0f, lookYRotation, 0f);
            playerCamera.rotation = Quaternion.Euler(lookXRotation, lookYRotation, 0f);
        }
        protected virtual void Jump()
        {
            if (!IsGrounded) return;

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        protected virtual void StartRun()
        {
            isRunning = true;
            currentSpeed = runSpeed;
        }
        protected virtual void StopRun()
        {
            isRunning = false;
            currentSpeed = walkSpeed;
        }
        protected virtual void Crouch()
        {

        }

        public PlayerInput GetPlayerInput() => playerInput;
        public bool IsGrounded => Physics.Raycast(transform.position, Vector3.down, currentHeight* 0.5f + 0.2f, whatIsGround);
        public bool IsRunning => isRunning;

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
                case "Crouch":
                    OnCrouch(context);
                    break;
            }
        }

        private void OnMove(CallbackContext callbackContext)
        {
            // Update vector
            moveInput = callbackContext.ReadValue<Vector2>();
        }
        private void OnLook(CallbackContext callbackContext)
        {
            // Update vector
            lookInput = callbackContext.ReadValue<Vector2>();
        }
        private void OnJump(CallbackContext callbackContext)
        {
            if (callbackContext.performed) Jump();
        }
        private void OnRun(CallbackContext callbackContext)
        {
            if (callbackContext.performed) StartRun();
            else if (callbackContext.canceled) StopRun();
        }
        private void OnCrouch(CallbackContext callbackContext)
        {
            if (callbackContext.performed) StartRun();
            else if (callbackContext.canceled) StopRun();
        }
        #endregion
    }
}
