using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int countEnemyAlive = 0;//每一波敌人存活数量
    public Transform start;//生成敌人开始位置
    public Wave[] waves;//敌人的波数

    private Coroutine coroutine;//保存协程SpawnEnemys
    private float rate = 1;//生成每一波敌人间隔时间

    void Start()
    {
        coroutine=StartCoroutine("SpawnEnemy");
    }
    //停止生成敌人
    public void StopSpawnEnemys()
    {
        StopCoroutine(coroutine);
    }
    //敌人生成器
    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                Instantiate(wave.enemyPrefab, start.position, Quaternion.identity);
                countEnemyAlive++;
                if (i != wave.count - 1) yield return new WaitForSeconds(wave.rate);
            }
            while (countEnemyAlive>0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(rate);
        }
        while (countEnemyAlive > 0)
        {
            yield return 0;
        }

        GameManager.Instance.GameOverPanel("胜  利");
    }
}
