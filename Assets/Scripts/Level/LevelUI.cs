using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;
 
public class LevelUI : MonoBehaviour
{
    public GameManager gameManager;

    public TMP_Text pizzaText;
    public TMP_Text beverageText;

    public TMP_Text timerText;
    public bool pause;

    public GameObject panelPause;

    public Button button;

    private float time;

    void start()
    {
        if (pause)
        {
            button.GetComponentInChildren<TMP_Text>().text = "Play";
        }
        else
        {
            button.GetComponentInChildren<TMP_Text>().text = "Pause";
        }
        
        pizzaText.text = gameManager.GetPizzaCount().ToString();
        beverageText.text = gameManager.GetBeverageCount().ToString();
        timerText.text = FormatTime(gameManager.maxTime - gameManager.GetTime());
    }

    private string FormatTime(float time) 
    {
        var minutes = time / 60;
        var seconds = time % 60;
        var timeString = string.Format("{0:00}:{1:00}", (int)minutes, (int)seconds);
        return timeString;
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
        pizzaText.text = gameManager.GetPizzaCount().ToString();
        beverageText.text = gameManager.GetBeverageCount().ToString();
        timerText.text = FormatTime(gameManager.maxTime - gameManager.GetTime());
    }
}