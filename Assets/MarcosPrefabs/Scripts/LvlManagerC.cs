using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlManagerC : MonoBehaviour
{
   [Header("Panel del menú")]
    public GameObject menuPanel;

    void Start()
    {
        menuPanel.SetActive(false); // Oculta el menú al inicio
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

    public void ShowGameLevelOverMenu()
    {
        Debug.Log("Game Over Menu");
        menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
