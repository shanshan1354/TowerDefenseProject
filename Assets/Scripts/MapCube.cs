using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public bool isUpgraded = false;//炮台是否升级过了
    [HideInInspector]
    public GameObject turretGO;//炮台
    public GameObject buildEffect;
    [HideInInspector]
    public TurretDate turretDate;

    private Renderer cubeRenderer;

    private void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
    }
    //建造炮台
    public void BuildTurret(TurretDate turretDate)
    {
        this.turretDate = turretDate;
        isUpgraded = false;
        turretGO = Instantiate(turretDate.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }
    //升级炮台
    public void UpgradeTurret()
    {
        //TODO
        if (isUpgraded) return;
        Destroy(turretGO);
        isUpgraded = true;
        turretGO = Instantiate(turretDate.turretUpgradedPrefab, transform.position, Quaternion.identity);
        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }
    //拆除炮台
    public void DestroyTurret()
    {
        //TODO
        Destroy(turretGO);
        isUpgraded = false;
        turretGO = null;
        turretDate = null;
        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }
    //鼠标停留在MapCube上时显示红色
    private void OnMouseEnter()
    {
        if (turretGO == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            cubeRenderer.material.color = Color.red;
        }
    }
    //鼠标离开MapCube上时恢复为白色
    private void OnMouseExit()
    {
        cubeRenderer.material.color = Color.white;
    }
}