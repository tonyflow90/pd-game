using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
   public bool running = true;

    private float time;

    void Update()
    {
    }

    void FixedUpdate()
    {
        if(running) {
            time += Time.deltaTime;

            var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
            var seconds = time % 60;//Use the euclidean division for the seconds.
            var fraction = (time * 100) % 100;

            //update the label value
            //  timerLabel.text = string.Format ("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
            timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }
    }
}