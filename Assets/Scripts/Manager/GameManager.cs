using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public LevelLoader levelLoader;

    private int pizzaCount = 0;
    public int maxPizzaCount = 3;
    private int beverageCount = 0;
    public int maxBeverageCount = 3;
    private float time;
    public float maxTime = 120;

    void start()
    {

    }
    void FixedUpdate()
    {
        time += Time.deltaTime;

        // if(CheckCompleteGame()) {
        //     // Debug.Log(CalculateScore());
        //     // LevelComplete();
        // }

        if (time > maxTime)
        {
            Debug.Log(CalculateScore());
            GameEnd();
        }

    }

    public void GameEnd()
    {
        Debug.Log("Ende");
        levelLoader.LoadLevel("LevelFailed");
    }

    public void LevelComplete() { 
        Debug.Log("LevelComplete");

        var timeString = FormatTime((int)maxTime - (int)time);
        var totalScore = CalculateScore();

        PlayerPrefs.SetString("pizza", pizzaCount.ToString());
        PlayerPrefs.SetString("beverage", beverageCount.ToString());
        PlayerPrefs.SetString("time", timeString);
        PlayerPrefs.SetString("score", totalScore.ToString());
        
        levelLoader.LoadLevel("LevelComplete");
    }
    private string FormatTime(float time) 
    {
        var minutes = time / 60;
        var seconds = time % 60;
        var timeString = string.Format("{0:00}:{1:00}", (int)minutes, (int)seconds);
        return timeString;
    }
    private int CalculateScore()
    {
        var timeScore = (maxTime * 10) - ((time % 60) * 10);
        var beverageScore = beverageCount * 100;
        var pizzaScore = pizzaCount * 200;

        return (int)timeScore + beverageScore + pizzaScore;
    }

    public bool CheckCompleteGame()
    {
        if(pizzaCount == maxPizzaCount && beverageCount == maxBeverageCount && time < maxTime) {
            return true;
        }
        return false;
    }
    public void IncreaseBeverageCount()
    {
        beverageCount++;
    }
    public void IncreasePizzaCount()
    {
        pizzaCount++;
    }

    public int GetPizzaCount()
    {
        return pizzaCount;
    }

    public int GetBeverageCount()
    {
        return beverageCount;
    }

    public float GetTime()
    {
        return time;
    }
}