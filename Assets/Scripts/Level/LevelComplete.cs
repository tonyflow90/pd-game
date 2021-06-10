using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

public class LevelComplete : MonoBehaviour
{
    public TMP_Text pizzaText;
    public TMP_Text beverageText;
    public TMP_Text timerText;
    public TMP_Text totalScore;

    void Update()
    {
    }

    void FixedUpdate()
    {
        pizzaText.text = PlayerPrefs.GetString("pizza");
        beverageText.text = PlayerPrefs.GetString("beverage");
        timerText.text = PlayerPrefs.GetString("time");
        totalScore.text = PlayerPrefs.GetString("score");
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}