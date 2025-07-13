using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public int enemiesAlive = 0;

    void Start()
    {
        Debug.Log("EnemyManager ready");
    }

    public void AddEnemy()
    {
        enemiesAlive++;
        Debug.Log("Enemy added. Total alive: " + enemiesAlive);
    }

    public void EnemyDefeated()
    {
        enemiesAlive--;
        Debug.Log("Enemy defeated! Remaining: " + enemiesAlive);

        if (enemiesAlive <= 0)
        {
            LevelComplete();
        }
    }

    void LevelComplete()
    {
        Debug.Log("VICTORY! Level Complete!");

        FindObjectOfType<MainMenuManager>().ShowGameOverMenu();
    }
}