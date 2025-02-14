using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController to access the ball count.

    private void Start()
    {
        // Ensure the PlayerController reference is set at the start.
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference not assigned in Obstacle script.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the obstacle is the ball.
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Destroy the obstacle.
            Destroy(gameObject);

            // Increase the ball count by 3 via PlayerController.
            if (playerController != null)
            {
                playerController.AddBallCount(3);
            }
        }
    }
}
