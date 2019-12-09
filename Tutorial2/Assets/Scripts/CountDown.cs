using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 120f;

    public Text endText;
    public GameObject player;

    public Text countdownText;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        endText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString ("Time Remaining: 00");

        if (currentTime <= 0)
        {
            currentTime = 0;
            endText.text = "You lose! Game created by Emily Rogers.";
            Destroy(player);

        }
    }
}
