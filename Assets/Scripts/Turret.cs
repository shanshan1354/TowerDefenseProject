using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public bool useLaser = false;//是否使用激光
    public float attackRateTime = 1;//多长时间攻击一次
    public float damageRate = 70;//激光伤害
    public GameObject bulletPrefab;//子弹
    public GameObject laserEffect;//激光特效
    public LineRenderer laserRenderer;//画线组件
    public Transform head;//炮头
    public Transform firePosition;//开火位置

    private  List<GameObject> enemys = new List<GameObject>();
    private float timer = 0;

    private void Start()
    {
        timer = attackRateTime;
    }
    private void Update()
    {
        //炮台根据目标进行扭头视角跟随
        if (enemys.Count>0&&enemys[0]!=null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }

        if (useLaser)
        {
            //使用激光攻击模式
                LaserAttack();
        }
        else
        {
            //使用子弹攻击模式
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRateTime)
            {
                Attack();
                timer = 0;
            }
        }
    }
    //激光攻击
    void LaserAttack()
    {
        if (enemys.Count > 0)
        {
            if (enemys[0] == null)//更新enemys，去掉集合里面空的子集
            {
                UpdateEnemys();
            }
            if (enemys.Count > 0)//更新enemys数组后再次判断集合里敌人数量是否大于0
            {
                if (laserRenderer.enabled == false) laserRenderer.enabled = true;
                laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
                laserEffect.transform.position = enemys[0].transform.position;//设置激光特效播放位置
                laserEffect.SetActive(true);
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }
        }
        else//若无敌人
        {
            laserEffect.SetActive(false);
            laserRenderer.enabled = false;
        }
    }
    //子弹攻击
    void Attack()
    {
        if (enemys[0]==null)
        {
            UpdateEnemys();
        }

        if(enemys.Count>0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attackRateTime;
        }
    }
    //更新enemys数组
    void UpdateEnemys()
    {
        List<int> emptyList = new List<int>();
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i]==null)
            {
                emptyList.Add(i);
            }
        }
        for (int i = 0; i < emptyList.Count; i++)
        {
            enemys.RemoveAt(emptyList[i] - i);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }

}
