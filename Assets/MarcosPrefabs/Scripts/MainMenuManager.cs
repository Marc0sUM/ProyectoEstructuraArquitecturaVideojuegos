using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   [Header("Panel del menú")]
    public GameObject menuPanel;

    private bool isPaused = false;

    void Start()
    {
        menuPanel.SetActive(false); // Oculta el menú al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
        menuPanel.SetActive(true);
        Time.timeScale = 0f; // Pausa el tiempo del juego
        isPaused = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Game Resumed");
        menuPanel.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo
        isPaused = false;
    }

    public void PlayGame()
    {
        // Si quieres que Play haga algo diferente en un menú principal
        Debug.Log("Play Game");
    }

    public void RestartGame()
    {
        Debug.Log("Restart Game");
        Time.timeScale = 1f; // Asegúrate de reanudar el tiempo antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void ShowGameOverMenu()
    {
        Debug.Log("Game Over Menu");
        menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
