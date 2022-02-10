using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    //price += level * constScale; 
    public int levelPowerBuff;
    public int levelBulletProfitBuff;

    public int pricePB;
    public int priceBPB;

    public int incrPersentPricePB = 10;
    public int incrPersentPriceBPB = 10;

    public int increPersentPB;
    public int increPersentBPB;

    public int powerBuff;
    public int profitBuff;

    public int persentPBScale;
    public int persentBPBScale;

    public int persentPricePBScale;
    public int persentPriceBPBScale;

    public static BuffManager instance;

    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    public delegate void OnBuffManagerChange();
    public OnBuffManagerChange onBuffManagerChangedCallBack;

    CoinManager coinManager;

    private void Start()
    {
        coinManager = CoinManager.instance;
    }
    public void InscreasePB() {
        if (coinManager.CheckCoins(pricePB))
        {
            levelPowerBuff += 1;

            coinManager.ChangeCoin(-pricePB);

            pricePB += ((int)(pricePB * incrPersentPricePB / 100));

            powerBuff += ((int)powerBuff * increPersentPB/100);

            incrPersentPricePB += persentPBScale;
            increPersentPB += persentPBScale;

            SaveLoadSysterm.instance.SaveBuffData();

            if (onBuffManagerChangedCallBack != null)
                onBuffManagerChangedCallBack.Invoke();
        }
    }

    public void InscreaseBPB()
    {
        if (coinManager.CheckCoins(priceBPB))
        {
            levelBulletProfitBuff += 1;

            coinManager.ChangeCoin(-priceBPB);

            priceBPB += ((int)(priceBPB * incrPersentPriceBPB/100));

            profitBuff += ((int)profitBuff * increPersentBPB / 100);

            incrPersentPriceBPB += persentPriceBPBScale;
            increPersentBPB += persentBPBScale;
            
            SaveLoadSysterm.instance.SaveBuffData();

            if (onBuffManagerChangedCallBack != null)
                onBuffManagerChangedCallBack.Invoke();
        }
    }

    public void LoadData(BuffData buffData) {

        levelPowerBuff = buffData.levelPowerBuff;
        levelBulletProfitBuff = buffData.levelBulletProfitBuff;

        pricePB = buffData.pricePB;
        priceBPB = buffData.priceBPB;

        incrPersentPricePB = buffData.incrPersentPricePB;
        incrPersentPriceBPB = buffData.incrPersentPriceBPB;

        powerBuff = buffData.powerBuff;
        profitBuff = buffData.profitBuff;

        UIManager.instance.ChangeBuff();

    }
}
