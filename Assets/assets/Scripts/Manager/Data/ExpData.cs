using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExpData
{
    public int currentExp;
    public int indexNextGun;
    public ExpData(ExpManager expManager, GunAndBulletManager gunAndBulletManager) {
        
        currentExp = expManager.expGunCurrent;
        indexNextGun = gunAndBulletManager.indexGun;

    }
}
