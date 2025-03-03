using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadGameWithDelay(1.5f));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadGameWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
