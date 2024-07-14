using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bubbleCountText;
    public Button pauseButton;
    public Button restartButton;
    public GameObject gameOverPanel;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pauseButton.onClick.AddListener(PauseGame);
        restartButton.onClick.AddListener(RestartGame);
        UpdateUI();
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + gameManager.score;
    }

    void PauseGame()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}