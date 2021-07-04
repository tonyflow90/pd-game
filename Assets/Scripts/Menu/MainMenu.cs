using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void Play () {
        levelLoader.LoadLevel("Level_1_light");
        // SceneManager.LoadScene(1);
    }

    public void Quit () {
        Application.Quit();
    }
}
