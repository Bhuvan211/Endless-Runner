using UnityEngine;// This script handles player movement based on user input.

public class Movement : MonoBehaviour// MonoBehaviour
{
    [SerializeField] private float speed = 5f;// Speed of the player movement
    [SerializeField] private float jumpForce = 5f;// Force applied when the player jumps
    [SerializeField] private int isGrounded = 0;// Flag to check if the player is grounded
    [SerializeField] private Rigidbody rb;// Reference to the player's Rigidbody component

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()// Update is called once per frame
    {
        MovePlayer();// Call the MovePlayer method
        if (Input.GetKeyDown(KeyCode.Space))// Check for jump input
        {
            Jump();// Call the Jump method
        }
    }

    public void MovePlayer()// Method to move the player
    {
        float horizontalInput = Input.GetAxis("Horizontal");// Get horizontal input
        float verticalInput = Input.GetAxis("Vertical");// Get vertical input
        //bool JumpInput = Input.GetKey(KeyCode.Space);// Get jump input
        

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * Time.deltaTime * speed;// Calculate movement vector
        transform.Translate(movement, Space.World);// Move the player in world space
    }

    public void Jump()// Method to make the player jump
    {
        if (isGrounded < 2)// Check if the player is grounded// for double jump
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);// Apply upward force to the Rigidbody
            isGrounded++;// Set isGrounded to false
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))// Check if the player collides with the ground
        {
            isGrounded = 0;// Set isGrounded to true
        }
    }
}