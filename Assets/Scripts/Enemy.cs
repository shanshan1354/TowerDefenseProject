using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1;//移动速度
    public GameObject explosionEffect;//爆炸特效
    public float hp = 150;//敌人血量
    private float totalHP;//保存敌人满血总值
    private int index = 0;
    private Transform[] movePosition;
    private Slider hpSlider;

    private void Start()
    {
        totalHP = hp;
        hpSlider = GetComponentInChildren<Slider>();
        movePosition = WayPoints.wayPoints;
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate((movePosition[index].position - transform.position).normalized * Time.deltaTime * moveSpeed);
        if (Vector3.Distance(movePosition[index].position, transform.position)<0.3f)
        {
            index++;
        }

        if (index> movePosition.Length - 1)
        {
            ReachDestination();
            return;
        }
    }
    //敌人到达目的地销毁自身
    private void ReachDestination()
    {
        Destroy(gameObject);
        GameManager.Instance.GameOverPanel("失  败");//游戏失败
    }
    //敌人销毁后，countEnemyAlive减一
    private void OnDestroy()
    {
        EnemySpawner.countEnemyAlive--;
    }

    //敌人受伤扣血
    public void TakeDamage(float damage)
    {
        hp -= damage;
        hpSlider.value = hp / totalHP;
        if (hp<=0)
        {
            Destroy(gameObject);
            GameObject effect= Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(effect, 1.5f);
            return;
        }
    }
}
