using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thesis
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Private Variables")]
        [SerializeField] private Rigidbody player_RigidBody;
        [SerializeField] private Animator player_Animator;
        [SerializeField] private bool player_Jumping;
        [SerializeField] private float velocity;
        [SerializeField] private float inputx;
        [SerializeField] private float inputz;

        [Header("Publicc Variables")]
        public float player_MoveSpeed;
        public float player_JumpForce;

        private void Awake()
        {
            player_RigidBody = GetComponent<Rigidbody>();
            player_Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            player_Jumping = false;
        }

        private void FixedUpdate()
        {
            inputx = Input.GetAxis("Horizontal");
            inputz = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(inputx, 0, inputz) * player_MoveSpeed;
            Vector3 FinalVelocity = new Vector3(movement.x, player_RigidBody.velocity.y, movement.z);
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