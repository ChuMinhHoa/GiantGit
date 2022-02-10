using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStoreManager : MonoBehaviour
{

    public int persentGunNow;

    public int gunID;

    public delegate void OnGunStoreChange();
    public OnGunStoreChange onGunStoreChangedCallback;

    public void SetPersentGunNow(int value) {
        persentGunNow = value;
        if (onGunStoreChangedCallback != null)
            onGunStoreChangedCallback.Invoke();
    }

}
