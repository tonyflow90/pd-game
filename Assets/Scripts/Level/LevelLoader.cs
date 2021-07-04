using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 1f;

    public void LoadLevel(string levelName)
    {
        StartCoroutine(_LoadLevel(levelName));
    }

    public void LoadGameOverLevel()
    {
        StartCoroutine(_LoadLevel("LevelFailed"));
    }

    IEnumerator _LoadLevel(string levelName)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }
}
