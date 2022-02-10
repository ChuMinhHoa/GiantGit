using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    public GameObject MenuUI;
    public bool canShoot;
    public bool followGiantNow;

    BuffManager buffManager;
    LevelManager levelManager;
    CoinManager coinManager;
    SceneChangeManager sceneChangeManager;
    ExpManager expManager;

    public TextMeshProUGUI levelBP;
    public TextMeshProUGUI levelPBP;
    public TextMeshProUGUI priceBP;
    public TextMeshProUGUI pricePBP;


    public TextMeshProUGUI coinText;

    //Confirm panel
    public GameObject confirmPanel;
    public Slider levelConfirmUI;
    public TextMeshProUGUI textLevelCf;
    public TextMeshProUGUI coinReward;
    public TextMeshProUGUI coinCf;



    public bool nextLevel;

    void Start() {
        buffManager = BuffManager.instance;
        buffManager.onBuffManagerChangedCallBack += ChangeBuff;

        levelManager = LevelManager.instance;
        
        sceneChangeManager = SceneChangeManager.instance;

        expManager = ExpManager.instance;

        coinManager = CoinManager.instance;
        coinManager.onCoinManagerChangedCallBack += ChangeCoinUI;

    }
    public void HideMenuUI() {
        MenuUI.SetActive(false);
        canShoot = true;
    }

    public void ShowMenuUI()
    {
        MenuUI.SetActive(true);
        canShoot = false;
    }

    public void PlayButton() {
        HideMenuUI();
        LevelManager.instance.LevelCreate();
        followGiantNow = true;
    }


    public void InCreasePB() {
        buffManager.InscreasePB();
    }

    public void InCreaseBPB()
    {
        buffManager.InscreaseBPB();
    }

    public void ChangeBuff() {
        if (buffManager==null)
            buffManager = BuffManager.instance;
        
        levelBP.text = "LV." + buffManager.levelPowerBuff;
        levelPBP.text = "LV." + buffManager.levelBulletProfitBuff;

        priceBP.text = buffManager.pricePB.ToString();
        pricePBP.text = buffManager.priceBPB.ToString();

    }

    public void ConfirmPanelOn(bool _nextLevel)
    {
        confirmPanel.SetActive(true);
        nextLevel = _nextLevel;
        levelManager.GetRewards();

        SaveLoadSysterm saveLoadSysterm = SaveLoadSysterm.instance;
        saveLoadSysterm.SaveData();
        saveLoadSysterm.SaveCoinData();
        saveLoadSysterm.SaveExpData();
        levelConfirmUI.maxValue = levelManager.groupNow;
        levelConfirmUI.value = 0;
        StartCoroutine(ChangeUI());
    }
    IEnumerator ChangeUI()
    {
        while (levelConfirmUI.value < levelManager.groupNow - 1)
        {
            float value = levelConfirmUI.value + 0.01f;
            levelConfirmUI.value = value;
            textLevelCf.text = (int)value + "/" + levelManager.totalGroup;
            yield return new WaitForSeconds(.01f);
        }

    }

    public void ChangeToNextShoot() {
        
        if (nextLevel)
            sceneChangeManager.ChangeScene(sceneChangeManager.GetCurrentScene() + 1);
        else sceneChangeManager.ChangeScene(sceneChangeManager.GetCurrentScene());

    }

    public void ChangeCoinUI() {
        if (coinManager == null)
            coinManager = CoinManager.instance;
        coinText.text = coinManager.GetCoins().ToString();
        coinCf.text = coinManager.GetCoins().ToString();
    }

    public void ChangeCoinReward(int coins) {
        coinReward.text = coins.ToString();
    }
}
