using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffData 
{

    public int levelPowerBuff;
    public int levelBulletProfitBuff;

    public int pricePB;
    public int priceBPB;

    public int incrPersentPricePB;
    public int incrPersentPriceBPB;

    public int powerBuff;
    public int profitBuff;


    public BuffData(BuffManager buffManager) 
    {
        levelPowerBuff = buffManager.levelPowerBuff;
        levelBulletProfitBuff = buffManager.levelBulletProfitBuff;

        pricePB = buffManager.pricePB;
        priceBPB = buffManager.priceBPB;

        incrPersentPricePB = buffManager.incrPersentPricePB;
        incrPersentPriceBPB = buffManager.incrPersentPriceBPB;

        profitBuff = buffManager.profitBuff;
        powerBuff = buffManager.powerBuff;
    }

}
