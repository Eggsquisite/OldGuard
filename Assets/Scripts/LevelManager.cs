using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadNextLevel()
    {
        StartCoroutine(AsyncLoadNextLevel());
    }

    IEnumerator AsyncLoadNextLevel()
    {
        AsyncOperation loadNextLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!loadNextLevel.isDone)
        {
            yield return null;
        }
    }

    public void RestartLevel()
    {
        PlayerManager.death = false;
        PlayerManager.paused = false;
        StartCoroutine(AsyncRestartLevel());
    }

    IEnumerator AsyncRestartLevel()
    {
        AsyncOperation restartLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        while (!restartLevel.isDone)
        {
            yield return null;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
