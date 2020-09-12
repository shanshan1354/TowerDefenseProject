using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    public GameObject endPanel;
    public Text messageText;

    private EnemySpawner enemySpawner;

    private void Awake()
    {
        _instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }

    //重新开始
    public void ResumeGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //返回菜单
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    //失败或胜利界面
    public void GameOverPanel(string message)
    {
        enemySpawner.StopSpawnEnemys();
        endPanel.SetActive(true);
        messageText.text = message;
    }
}