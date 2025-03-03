using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText; 
    private int score;

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         QuitGame();
    //     }
    // }

    // void QuitGame()
    // {
    //     Debug.Log("Game is closing.");
    //     Application.Quit();
    // }

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        highscoreText.text = LoadHighscore().ToString();
        gameOver.alpha = 0f;
        gameOver.interactable = false;
        
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true; //enables user inputs when new game "wsad"
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.6f;
        float from = canvasGroup.alpha;

        while ( elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;

        scoreText.text = score.ToString();

        SaveHighscore();
    }

    private void SaveHighscore()
    {
        int highscore = LoadHighscore();

        if (score > highscore) {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    private int LoadHighscore()
    {
        return PlayerPrefs.GetInt("highscore", 0);
    }
}
