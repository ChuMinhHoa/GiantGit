using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffData 
{

    public int levelPowerBuff;
    public int levelBulletProfitBuff;

    public BuffData(int levelPB, int levelBPB) {
        levelPowerBuff = levelPB;
        levelBulletProfitBuff = levelBPB;
    }

}
