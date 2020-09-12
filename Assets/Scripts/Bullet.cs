using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 40;
    public float distance = 1.3f;
    public float damage = 50;
    public GameObject explosionEffectPrefab;
    private Transform target;
    void Update()
    {
        //如果敌人被在子弹打到之前被销毁，那么子弹也销毁
        if (target==null)
        {
            Die();
            return;
        }
        //子弹射向敌人
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //检测子弹与敌人之间的距离，当子弹碰到时产生爆炸特效，并销毁子弹
        Vector3 dir = target.position - transform.position;
        if (dir.magnitude<distance)
        {
            target.gameObject.GetComponent<Enemy>().TakeDamage(damage);//子弹造成的伤害
            Die();
        }
    }
    //子弹销毁
    void Die()
    {
        GameObject effect = Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }
    //设置子弹目标
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}