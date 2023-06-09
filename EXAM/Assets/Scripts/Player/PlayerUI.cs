using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;   
     // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     public void UpdateTimerText(string timerMessage)
    {
        timerText.text = timerMessage;
    }
}
