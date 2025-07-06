using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI fruitCounter;
    public TextMeshProUGUI fruitTotal;
    public TextMeshProUGUI healthText;
    public GameObject startPanel, gamePanel, deathPanel, pausePanel, winPanel;
    public enum GameState {Start, InGame, Death, Pause, Win }
    public GameState state = GameState.Start;
    public PlayerMovement player;

    private void Awake()
    {
        UpdateHealth();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(state == GameState.InGame)
            {
                state = GameState.Pause;
            }
            else if(state == GameState.Pause)
                state = GameState.InGame;

        }

        switch (state)
        {
            case GameState.Start:
                {
                    startPanel.SetActive(true);
                    gamePanel.SetActive(false);
                    deathPanel.SetActive(false);
                    pausePanel.SetActive(false);
                    winPanel.SetActive(false);  
                    break;
                }
            case GameState.InGame:
                {
                    startPanel.SetActive(false);
                    gamePanel.SetActive(true);
                    deathPanel.SetActive(false);
                    pausePanel.SetActive(false);
                    winPanel.SetActive(false);
                    break;
                }
            case GameState.Death: 
                {
                    startPanel.SetActive(false);
                    gamePanel.SetActive(false);
                    deathPanel.SetActive(true);
                    pausePanel.SetActive(false);
                    winPanel.SetActive(false);
                    break;
                }
            case GameState.Pause: 
                {
                    startPanel.SetActive(false);
                    gamePanel.SetActive(false);
                    deathPanel.SetActive(false);
                    pausePanel.SetActive(true);
                    winPanel.SetActive(false);
                    break;
                }
            case GameState.Win: 
                {
                    startPanel.SetActive(false);
                    gamePanel.SetActive(false);
                    deathPanel.SetActive(false);
                    pausePanel.SetActive(false);
                    winPanel.SetActive(false);
                    winPanel.SetActive(true);

                    fruitTotal.text = "You Collected " + player.fruitCollected + " Flies!";
                    break;
                }
        }   
    }
    public void UpdateCounter(int amount)
    {
        fruitCounter.text = "Flies Collected: " + amount;
    }

    public void UpdateHealth()
    {
        healthText.text = "Health: " + player.playerHealth.ToString();
    }

    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void StartButton()
    {
        state = GameState.InGame;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
