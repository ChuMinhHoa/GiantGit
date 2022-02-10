using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }
    public delegate void OnCoinManagerChange();
    public OnCoinManagerChange onCoinManagerChangedCallBack;

    public int coins;

    public int GetCoins() { return coins; }
    public void SetCoins(int value) { coins = value; }

    public void ChangeCoin(int value) {
        coins += value;
        SaveLoadSysterm.instance.SaveCoinData();
        if (onCoinManagerChangedCallBack != null)
            onCoinManagerChangedCallBack.Invoke();
    }

    public bool CheckCoins(int value) {
        if (value <= coins)
            return true;
        else return false;
    }
}
