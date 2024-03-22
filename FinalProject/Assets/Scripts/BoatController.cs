using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Transform centre; // Reference to the center of the boat
    public Transform player; // Reference to the player object
    public float speed = 5f; // Speed at which the boat moves

    private bool playerOnBoat = false; // Flag to track if the player is on the boat
    private Vector3 direction; // Direction vector towards the player on the boat

    private void Update()
    {
        // If the player is on the boat, move towards the calculated direction
        if (playerOnBoat && player != null)
        {
            // Move the boat towards the player's position on the boat
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the boat's trigger collider
        if (other.CompareTag("Player"))
        {
            // Set the player as on the boat
            playerOnBoat = true;

            // Set the player's parent to the boat's centre
            player.parent = centre;

            // Calculate the initial direction towards the player's position on the boat
            direction = (player.position - centre.position).normalized;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player has exited the boat's trigger collider
        if (other.CompareTag("Player"))
        {
            // Set the player as not on the boat
            playerOnBoat = false;

            // Reset the player's parent
            player.parent = null;
        }
    }
}