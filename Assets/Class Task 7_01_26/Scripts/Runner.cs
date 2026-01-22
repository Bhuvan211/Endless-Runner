using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Runner : MonoBehaviour
{
    [SerializeField] private float speed = 5f;// Speed of the player movement
    [SerializeField] private float jumpForce = 5f;// Force applied when the player jumps
    [SerializeField] private int isGrounded = 0;// Flag to check if the player is grounded
    [SerializeField] private Rigidbody rb;// Reference to the player's Rigidbody component
    [SerializeField] private Animator animator;// Reference to the Animator component
    [SerializeField] private AudioManager audioManager;
    public bool startGame = false;

    private void Awake()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }


    private void Update()// Update is called once per frame
    {
        ContinewMovement();// Call the ContinueMovement method
        MovePlayer();// Call the MovePlayer method
        if (Input.GetKeyDown(KeyCode.Space))// Check for jump input
        {
            Jump();// Call the Jump method
        }
    }

    public void MovePlayer()// Method to move the player
    {
        float horizontalInput = Input.GetAxis("Horizontal");// Get horizontal input
       


        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * Time.deltaTime * speed;// Calculate movement vector
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

    private void OnCollisionEnter(Collision collision)// Handle collision events
    {

        if (collision.gameObject.CompareTag("Ground"))// Check if the player collides with the ground
        {
            isGrounded = 0;// Set isGrounded to true
        }
        if (collision.gameObject.CompareTag("Obstacle"))// Check if the player collides with an obstacle
        {
            audioManager.PlayCrash();
            animator.SetBool("Death",true);
            StartCoroutine(RestartGame());// Restart the game after a delay
            // Handle obstacle collision (e.g., end game, reduce health, etc.)
            Debug.Log("Collided with an obstacle!");
        }
    }
    private void ContinewMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);// Move the player forward continuously
    }
    
    IEnumerator RestartGame()
    {
        //animator.SetBool("Death", true);
        Console.WriteLine("Game Over! Restarting...");

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Endless Runner");// Reload the current scene
    }



}
