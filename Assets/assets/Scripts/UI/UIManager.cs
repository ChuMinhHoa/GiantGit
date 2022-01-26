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

    public TextMeshProUGUI levelBP;
    public TextMeshProUGUI levelPBP;
    public TextMeshProUGUI coinText;

    //Confirm panel
    public GameObject confirmPanel;

    public bool nextLevel;

    void Start() {
        buffManager = BuffManager.instance;
        buffManager.onBuffManagerChangedCallBack += ChangeBuff;

        levelManager = LevelManager.instance;
        
        sceneChangeManager = SceneChangeManager.instance;

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

    public void ChangeScene() {
        ShowMenuUI();
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
    }

    public void ConfirmPanelOn(bool _nextLevel)
    {
        confirmPanel.SetActive(true);
        nextLevel = _nextLevel;
        levelManager.GetRewards();
        ChangeUI();
    }
    IEnumerator ChangeUI()
    {
        yield return new WaitForSeconds(0.5f);
    }

    void ChangeToNextShoot() {
        if (nextLevel)
            sceneChangeManager.ChangeScene(sceneChangeManager.GetCurrentScene() + 1);
        else sceneChangeManager.ChangeScene(sceneChangeManager.GetCurrentScene());
    }

    public void ChangeCoinUI() {
        if (coinManager == null)
            coinManager = CoinManager.instance;
        coinText.text = coinManager.coins.ToString();
    }
}
