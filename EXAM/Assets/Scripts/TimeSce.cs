using UnityEngine;
using TMPro;

public class TimeSce : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void Awake()
    {
        // Retrieve the saved time from PlayerPrefs
        float elapsedTime = PlayerPrefs.GetFloat("ElapsedTime", 0f);

        // Display the time in the UI Text component
        timeText.text = "Time: " + elapsedTime.ToString("F2") + "s";
    }
}
