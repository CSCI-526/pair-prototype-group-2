using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


// Controls player movement and rotation.
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed.
    public int ballCount = 5; //Starting with 5 ballls.
    public TMP_Text ballCountText; // Reference to the Text UI for curr
    
    public TMP_Text gameOverText;
    private float gameOverY = -3.0f; // when player z value is smaller than -1 ----> game over

    public TMP_Text MovementText;
    public TMP_Text clickText;

    private Rigidbody rb; // Reference to player's Rigidbody.

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
        UpdateBallCountUI();
        gameOverText.gameObject.SetActive(false);
        clickText.gameObject.SetActive(false);
    }

    private void Update() 
    {
        // game over
        float playerY = transform.position.y;
        if(playerY < gameOverY || ballCount == 0) {
            ShowGameOverWithDelay();
        }

        // restart game
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        // movement notification disappear
        if(transform.position.x < -8f) {
            MovementText.gameObject.SetActive(false);
            clickText.gameObject.SetActive(true);
        }

        if(transform.position.x < -20f) {
            clickText.gameObject.SetActive(false);
        }
    }

    // Add to the ball count.
    public void AddBallCount(int amount)
    {
        ballCount += amount;
        Debug.Log("Ball count increased! Total balls: " + ballCount);
        UpdateBallCountUI(); // Update the ball count UI 
    }

    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
        // Player always move forward
        // float moveVertical = Input.GetAxis("Vertical");
        float moveVertical = 1.0f;
        Vector3 movement = transform.forward * moveVertical * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

    }

    private void UpdateBallCountUI()
    {
        if (ballCountText != null)
        {
            ballCountText.text = "Total Balls: " + ballCount;
        }
        else
        {
            Debug.LogError("Ball Count UI Text not assigned!");
        }
    }


    public void ShowGameOver() {
        gameOverText.gameObject.SetActive(true);
        gameOverText.alignment = TextAlignmentOptions.Center; // make the text apper in the center of the camera
        Time.timeScale = 0f;
        Debug.Log("Player is below -1, Game Over!");
    }
    private void ShowGameOverWithDelay()
    {
        StartCoroutine(ShowGameOverCoroutine()); // 启动 Coroutine
    }
    private IEnumerator ShowGameOverCoroutine() {
        yield return new WaitForSeconds(1f);

        gameOverText.gameObject.SetActive(true);
        gameOverText.alignment = TextAlignmentOptions.Center; // make the text apper in the center of the camera
        Time.timeScale = 0f;
        Debug.Log("Player is below -1, Game Over!");
    }

    public void RestartGame()
    {
        transform.position = Vector3.zero;
        gameOverText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}