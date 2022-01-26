using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSysterm : MonoBehaviour
{
    public static SaveLoadSysterm instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    public SaveData data;
    public BuffData buffData;
    public CoinData coinData;

    public void GetData() {
        data = SaveLoadDataManager.LoadData();
    }

    public void SaveData() {
        SaveLoadDataManager.SaveData();
    }

    public void LoadData() {
        if (data != null)
        {
            LevelManager levelManager = LevelManager.instance;
            levelManager.levelNow = data.level;
            levelManager.groupNow = data.groupNow;
            levelManager.totalGroup = levelManager.levels[int.Parse(data.level) - 1].groupOfLevel.Length;

            if (levelManager.OnLevelChangedCallback != null)
                levelManager.OnLevelChangedCallback.Invoke();


        }

    }

    public void SaveBuffData() {
        SaveLoadDataManager.SaveBuffData();
    }

    public void GetBuffData() {
        buffData = SaveLoadDataManager.LoadBuffData();
    }

    public void LoadBuffData() {
        BuffManager.instance.LoadData(buffData.levelPowerBuff,
            buffData.levelBulletProfitBuff);
    }

    public void SaveCoinData() {
        SaveLoadDataManager.SaveCoinData();
    }

    public void LoadCoinData() {
        CoinManager coinManager = CoinManager.instance;
        coinManager.coins = coinData.coins;
        UIManager.instance.ChangeCoinUI();
    }

    public void GetCoinData() {
        coinData = SaveLoadDataManager.LoadCoinData();
    }
}
