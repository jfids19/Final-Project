using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Transform centre; // Reference to the center of the boat
    public Transform player; // Reference to the player object
    public float speed = 5f; // Speed at which the boat moves

    private bool playerOnBoat = false; // Flag to track if the player is on the boat
    private bool playerAboard = false;
    private Vector3 direction; // Direction vector towards the player on the boat
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    
    private void Update()
    {
        // If the player is on the boat, move towards the calculated direction
        if (playerOnBoat && player != null && centre != null)
        {
            //Calculate the direction vector towards the player on the boat
            direction = (player.position - centre.position).normalized;

            // Move the boat towards the player's position on the boat
            transform.Translate(direction * speed * Time.deltaTime);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player has entered the boat's trigger collider
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set the player as on the boat
            playerAboard = true;

            Debug.Log("Player is on the boat!");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the player has exited the boat's trigger collider
        if (collision.gameObject.CompareTag("Player"))
        {
            // Set the player as not on the boat
            playerAboard = false;

            Debug.Log("Player is off the boat!");
        }
    }

    public bool IsPlayerOnBoat()
    {
        return playerAboard;
    }

    public void ResetBoatPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}