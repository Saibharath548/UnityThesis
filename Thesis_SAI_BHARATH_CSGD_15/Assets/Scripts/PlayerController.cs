using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thesis
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Private Variables")]
        [SerializeField] private Camera main_Camera;
        [SerializeField] private Rigidbody player_RigidBody;
        [SerializeField] private Animator player_Animator;
        [SerializeField] private Quaternion player_CurrentRotation;
        [SerializeField] private Vector3 inputx;
        [SerializeField] private Vector3 inputz;
        [SerializeField] private float velocity;
        [SerializeField] private float xAxis;
        [SerializeField] private float yAxis;
        [SerializeField] private bool player_Jumping;


        [Header("Public  Variables")]
        public float mouse_Sensitivity;
        public float player_MoveSpeed;
        public float player_JumpForce;

        private void Awake()
        {
            main_Camera = Camera.main;
            player_RigidBody = GetComponent<Rigidbody>();
            player_Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            player_CurrentRotation = gameObject.transform.rotation;
            player_Jumping = false;
        }
        private void FixedUpdate()
        {
            inputx = Input.GetAxis("Horizontal") * transform.right;
            inputz = Input.GetAxis("Vertical") * transform.forward;
            xAxis = Input.GetAxisRaw("Mouse X") * mouse_Sensitivity;
            yAxis -= Input.GetAxisRaw("Mouse Y") * mouse_Sensitivity;

            player_CurrentRotation.y += xAxis;
            yAxis = Mathf.Clamp(yAxis, -20, 20);

            Vector3 movement = (inputx+inputz) * player_MoveSpeed;
            Vector3 FinalVelocity = new Vector3(movement.x, player_RigidBody.velocity.y, movement.z);
            transform.rotation = Quaternion.Euler(0, player_CurrentRotation.y, 0);
            main_Camera.transform.localEulerAngles = new Vector3(yAxis, 0, 0);
            player_RigidBody.velocity = FinalVelocity;


            velocity = movement.magnitude;


            if (Input.GetKeyDown(KeyCode.Space) && isGrounded() && !player_Jumping)
            {
                player_RigidBody.AddForce(Vector3.up * player_JumpForce, ForceMode.Impulse);
                player_Jumping = true;
            }
            if (isGrounded())
            {
                player_Jumping = false;
            }
            player_Animator.SetFloat("xVelocity", velocity);
        }



        private bool isGrounded()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
            {
                return true;
            }
            return false;
        }
    }
}