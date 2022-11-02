using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public enum GameState { MainMenu, PauseMenu, PlayingUI, GameOverMenu };
    public GameState currentState;
    public GameObject mainMenuPanel, pauseMenuPanel, playUIPanel, gameOverMenuPanel;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            CheckGameState(GameState.MainMenu);
        }
        else
        {
            CheckGameState(GameState.PlayingUI);
        }
    }

    public void CheckGameState(GameState newGameState)
    {
        currentState = newGameState;

        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenuSetup();
                break;
            case GameState.PauseMenu:
            PauseMenuSetup();
                break;
            case GameState.PlayingUI:
            PlayGameUISetup();
                break;
            case GameState.GameOverMenu:
            GameOverSetup();
                break;
        }
    }

    public void MainMenuSetup()
    {
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        playUIPanel.SetActive(false);
        gameOverMenuPanel.SetActive(false);
    }

    public void PauseMenuSetup()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
        playUIPanel.SetActive(false);
        gameOverMenuPanel.SetActive(false);
    }

    public void PlayGameUISetup()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        playUIPanel.SetActive(true);
        gameOverMenuPanel.SetActive(false);
    }
    public void GameOverSetup()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        playUIPanel.SetActive(false);
        gameOverMenuPanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
