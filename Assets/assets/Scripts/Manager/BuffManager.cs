using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public int levelPowerBuff;
    public int levelBulletProfitBuff;

    [Range(0,5)]
    public int powerBuff;
    [Range(10, 50)]
    public int profitBuff;

    public static BuffManager instance;

    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    public delegate void OnBuffManagerChange();
    public OnBuffManagerChange onBuffManagerChangedCallBack;

    public void InscreasePB() {

        levelPowerBuff += 1;

        //SaveBuffData

        SaveLoadSysterm.instance.SaveBuffData();

        if (onBuffManagerChangedCallBack != null)
            onBuffManagerChangedCallBack.Invoke();
        
    }

    public void InscreaseBPB()
    {

        levelBulletProfitBuff += 1;

        //SaveBuffData

        SaveLoadSysterm.instance.SaveBuffData();

        if (onBuffManagerChangedCallBack != null)
            onBuffManagerChangedCallBack.Invoke();

    }

    public void LoadData(int levelPB, int levelBPB) {

        levelPowerBuff = levelPB;
        levelBulletProfitBuff = levelBPB;

        UIManager.instance.ChangeBuff();

    }
}
