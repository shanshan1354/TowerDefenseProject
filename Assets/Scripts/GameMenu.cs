using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    //开始游戏
    public void StarGame()
    {
        SceneManager.LoadScene(1);
    }
    //退出游戏
    public void Exit()
    {
        Application.Quit();
    }
}