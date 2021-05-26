using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;
 
public class LevelUI : MonoBehaviour
{
    public TMP_Text timerText;
    public bool pause;

    public GameObject panelPause;

    public Button button;

    private float time;

    void start()
    {
        // panelPause.SetActive(true);

        if (pause)
        {
            button.GetComponentInChildren<TMP_Text>().text = "Play";
        }
        else
        {
            button.GetComponentInChildren<TMP_Text>().text = "Pause";
        }
    }

    public void TogglePlayPause()
    {
        pause = !pause;
        panelPause.SetActive(pause);
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (pause)
        {
            button.GetComponentInChildren<TMP_Text>().text = "Play";
        }
        else
        {
            button.GetComponentInChildren<TMP_Text>().text = "Pause";
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;

        var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = time % 60;//Use the euclidean division for the seconds.
        var fraction = (time * 100) % 100;

        //update the label value
        //  timerLabel.text = string.Format ("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}