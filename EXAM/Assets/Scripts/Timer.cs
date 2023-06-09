using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool isMoving = false;

    private void Start()
    {
        // Set the timer text to an initial value
        timerText.text = "Timer: 0.00s";
    }

    private void Update()
    {
        // Check if the player is moving
        if (!isMoving && (Keyboard.current.wKey.isPressed || Keyboard.current.aKey.isPressed ||
            Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed))
        {
            isMoving = true;
            startTime = Time.time;
        }

        // Check if the player has stopped moving
        if (isMoving && !(Keyboard.current.wKey.isPressed || Keyboard.current.aKey.isPressed ||
            Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed))
        {
            isMoving = false;
            float elapsedTime = Time.time - startTime;
            Debug.Log("Player moved for: " + elapsedTime + " seconds");

            // Update the timer text
            timerText.text = "Timer: " + elapsedTime.ToString("F2") + "s";
        }
    }
}
