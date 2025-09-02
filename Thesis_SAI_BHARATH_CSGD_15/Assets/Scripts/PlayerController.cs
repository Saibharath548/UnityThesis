using UnityEngine;

namespace Thesis
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Private Variables")]
        [SerializeField] private GameObject main_Camera;
        [SerializeField] private Rigidbody player_RigidBody;
        [SerializeField] private Animator player_Animator;
        [SerializeField] private Quaternion player_CurrentRotation;
        [SerializeField] private Vector3 moveDirection;
        [SerializeField] private float velocity;
        [SerializeField] private float xAxis;
        [SerializeField] private float yAxis;


        [Header("Public Variables")]
        public float mouse_Sensitivity = 2f;
        public float player_MoveSpeed = 5f;
        public float player_JumpForce = 5f;
        public float rotateSpeed = 10f;

        private void Awake()
        {
            player_RigidBody = GetComponent<Rigidbody>();
            player_Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            player_CurrentRotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            // Camera rotation input
            xAxis = Input.GetAxis("Mouse X") * mouse_Sensitivity;
            yAxis -= Input.GetAxis("Mouse Y") * mouse_Sensitivity;
            yAxis = Mathf.Clamp(yAxis, -20, 20);

            // Rotate camera horizontally with player
            player_CurrentRotation *= Quaternion.Euler(0, xAxis, 0);
            main_Camera.transform.rotation = Quaternion.Euler(yAxis, player_CurrentRotation.eulerAngles.y, 0);

            // Get movement input relative to camera
            Vector3 camForward = main_Camera.transform.forward;
            Vector3 camRight = main_Camera.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");

            moveDirection = (camForward * inputZ + camRight * inputX).normalized;

            // Apply movement
            Vector3 FinalVelocity = moveDirection * player_MoveSpeed;
            FinalVelocity.y = player_RigidBody.velocity.y;
            player_RigidBody.velocity = FinalVelocity;

            // Rotate player towards movement direction (if moving)
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            }

            // Animation velocity
            velocity = new Vector2(inputX, inputZ).magnitude;
            if (player_Animator)
            {
                player_Animator.SetFloat("xVelocity", velocity); // optional if you want
            }
        }

    }

}