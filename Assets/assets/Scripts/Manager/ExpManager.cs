using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public static ExpManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    public delegate void OnExpChange();
    public OnExpChange onExpChangedCallBack;

    GunAndBulletManager gunAndBulletManager;

    private void Start()
    {
        gunAndBulletManager = GunAndBulletManager.instance;
    }

    public int expGunCurrent;
    public int nextGunExp;

    public int GetGunExp() {
        return expGunCurrent;
    }

    public void SetGunExp(int Value) {
        expGunCurrent = Value;
        if (onExpChangedCallBack != null)
            onExpChangedCallBack.Invoke();
    }

    public int GetNextGunExp()
    {
        return nextGunExp;
    }

    public void SetNextGunExp(int Value)
    {
        nextGunExp = Value;
        if (onExpChangedCallBack != null)
            onExpChangedCallBack.Invoke();
    }

    public void LoadData(ExpData expData) {
        SetGunExp(expData.currentExp + expGunCurrent);
        if (gunAndBulletManager == null)
            gunAndBulletManager = GunAndBulletManager.instance;
        int nextExp = gunAndBulletManager.listGun[expData.indexNextGun].exp;
        SetNextGunExp(nextExp);

        ExpUIManager.instance.ChangeExp();
    }
}
