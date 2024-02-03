using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

using Sirenix.OdinInspector;

namespace Hotiovip.YAFPSController
{
    public class PlayerController : MonoBehaviour
    {
        #region VARIABLES
        [Title("References")]
        [LabelText("Rigidbody")]
        [SerializeField]
        [Required]
        private Rigidbody rb;
        [SerializeField]
        [Required]
        private CapsuleCollider capsuleCollider;
        [SerializeField]
        [Required]
        private PlayerInput playerInput;
        [SerializeField]
        [Required]
        private Transform playerCamera;

        [Title("Movement Settings")]
        [SerializeField]
        private LayerMask whatIsGround;
        [SerializeField]
        private float groundDrag;
        [Space]
        [SerializeField]
        private float walkSpeed = 3f;
        [SerializeField]
        private float runSpeed = 4.5f;
        [SerializeField]
        private float jumpForce = 10f;

        [Title("Input Settings")]
        [SerializeField]
        private float mouseXSensitivity = 1f;
        [SerializeField]
        private float mouseYSensitivity = 1f;

        private float currentHeight;
        private float currentSpeed;

        private Vector2 moveInput;
        private Vector2 lookInput;
        private float lookXRotation;
        private float lookYRotation;

        // States
        private bool isRunning = false;
        #endregion

        private void OnEnable()
        {
            playerInput.onActionTriggered += OnActionTriggered;
        }
        private void OnDisable()
        {
            playerInput.onActionTriggered -= OnActionTriggered;
        }
        void Start()
        {
            // Set default current speed to walk speed
            currentSpeed = walkSpeed;
            currentHeight = capsuleCollider.height;

            // Set the cursor to invisible and locked at the center
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        void Update()
        {

        }
        private void FixedUpdate()
        {
            Look();
            Move();
        }

        private void Move()
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
        private void Look()
        {
            float lookInputX = lookInput.x * mouseXSensitivity * Time.deltaTime;
            float lookInputY = lookInput.y * mouseYSensitivity * Time.deltaTime;

            lookYRotation += lookInputX;
            lookXRotation -= lookInputY;
            lookXRotation = Mathf.Clamp(lookXRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(0f, lookYRotation, 0f);
            playerCamera.rotation = Quaternion.Euler(lookXRotation, lookYRotation, 0f);
        }
        private void Jump()
        {
            if (!IsGrounded) return;

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        private void StartRun()
        {
            isRunning = true;
            currentSpeed = runSpeed;
        }
        private void StopRun()
        {
            isRunning = false;
            currentSpeed = walkSpeed;
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
        #endregion
    }
}
