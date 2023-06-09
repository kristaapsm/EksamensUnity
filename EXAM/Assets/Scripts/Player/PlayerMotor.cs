using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    private bool isMoving = false;
    private float startTime;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 0.5f;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI highScoreText;

    private bool timerStarted = false;
    private float elapsedTime;
    private float highScore;

    bool crouching = false;
    float crouchTimer = 0.2f;
    bool lerpCrouch = false;
    bool sprinting = false;

    private bool isOnPlatform = false; // Flag to track if the player is on a platform

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }

        if (timerStarted && !isOnPlatform)
        {
            elapsedTime = Time.time - startTime;
            timerText.text = "Timer: " + elapsedTime.ToString("F2") + "s";
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        if (!isMoving && (moveDirection.x != 0f || moveDirection.z != 0f))
        {
            isMoving = true;
            startTime = Time.time;
            timerStarted = true;

            // Debug the current high score value
        float currentHighScore = PlayerPrefs.GetFloat("HighScore", 0f);
        Debug.Log("Current high score: " + currentHighScore.ToString("F2") + "s");

        }

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -1f;

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 6;
        else
            speed = 5;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Platform"))
        {
            isOnPlatform = true;
            StopTimer();
            SaveElapsedTime();
            ShowTimeScene();
        }
    }

private void SaveElapsedTime()
{
    float elapsedTime = Time.time - startTime;

    // Retrieve the previous high score
    float previousHighScore = PlayerPrefs.GetFloat("HighScore", 0f);

    // Check if the current time is greater than the previous high score
    if (elapsedTime < previousHighScore || previousHighScore == 0f)
    {
        // Update the high score
        PlayerPrefs.SetFloat("HighScore", elapsedTime);
        PlayerPrefs.Save();
        Debug.Log("New high score set: " + elapsedTime.ToString("F2") + "s");
    }

    // Save the current time
    PlayerPrefs.SetFloat("ElapsedTime", elapsedTime);
    PlayerPrefs.Save();

    // Debug the saved high score
    float updatedHighScore = PlayerPrefs.GetFloat("HighScore", 0f);
    Debug.Log("Updated high score: " + updatedHighScore.ToString("F2") + "s");
}






    private void ShowTimeScene()
    {
        // Load a new scene to display the time
        SceneManager.LoadScene("EndScreen");
    }

    private void StopTimer()
    {
        isMoving = false;
        timerStarted = false;
    }
}
