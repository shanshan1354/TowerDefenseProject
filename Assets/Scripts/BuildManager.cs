using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public Button upgradeButton;//升级按钮
    public GameObject upgradeCanvas;//升级面板
    public int money = 1000;//初始金币
    public Text moneyText;
    public TurretDate laserTurret;
    public TurretDate MissileTurret;
    public TurretDate SdandardTurret;

    private Animator upgradeCanvasAnimator;
    private MapCube selectedMapCube;//当前选择的炮台
    private TurretDate selectedTurret;//选中要建造的炮台

    private void Start()
    {
        upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //开发炮台建造
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();//得到点击的MapCube
                    if (mapCube.turretGO == null)//判断MapCube上没有炮台
                    {
                        if (selectedTurret != null)//selectedTurret不为空才能进行建造炮台
                        {
                            //可以创建
                            if (money >= selectedTurret.cost)//判断钱够不够
                            {
                                ChangeMoney(selectedTurret.cost);
                                mapCube.BuildTurret(selectedTurret);
                            }
                            else
                            {
                                //TODO钱不够
                                moneyText.gameObject.GetComponent<Animator>().SetTrigger("Flicker");
                            }
                        }
                    }
                    else//若mapCube上有炮台了，再次点击就会提示升级
                    {
                        //TODO升级处理
                        if (selectedMapCube == mapCube&& upgradeCanvas.activeInHierarchy)
                        {
                            StartCoroutine("HideUpgradeUI");
                        }
                        else
                        {
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                        }
                        selectedMapCube = mapCube;
                    }
                }
            }
        }
    }
    //显示升级面板
    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        StopCoroutine("HideUpgradeUI");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        upgradeButton.interactable = !isDisableUpgrade;
    }
    //隐藏升级面板
    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.4f);
        upgradeCanvas.SetActive(false);
    }
    //花费金币
    void ChangeMoney(int change = 0)
    {
        money -= change;
        moneyText.text = "￥" + money;
    }
    //点击升级按钮
    public void OnUpgradeButtonDawn()
    {
        if (money> selectedMapCube.turretDate.upgradedCost)
        {
            selectedMapCube.UpgradeTurret();
            ChangeMoney(selectedMapCube.turretDate.upgradedCost);
        }
        else
        {
            moneyText.gameObject.GetComponent<Animator>().SetTrigger("Flicker");
        }
        StartCoroutine("HideUpgradeUI");
    }
    //点击拆除按钮
    public void OnDestroyButtonDawn()
    {
        selectedMapCube.DestroyTurret();
        StartCoroutine("HideUpgradeUI");
    }
    //选择炮台类型
    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurret = laserTurret;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurret = MissileTurret;
        }
    }

    public void OnSdandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurret = SdandardTurret;
        }
    }


}