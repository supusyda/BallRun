using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Timmer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI timerText;
    public float remainingTime = 6;
    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        { remainingTime = remainingTime - Time.deltaTime; }

        else if (remainingTime <= 0.01)
        {
            remainingTime = 0;
            timerText.color = Color.red;

        }

        int minute = Mathf.FloorToInt(remainingTime / 60);
        int seconcd = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minute, seconcd);
    }
}
