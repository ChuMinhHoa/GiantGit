using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAndBulletManager : MonoBehaviour
{
    public static GunAndBulletManager instance;
    ExpManager expManager;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    public List<ScriptAbleObject_Bullet> listBullet;
    public List<ScriptAbleObject_Gun> listGun;

    public int indexGun;
    public int indexBullet;

    private void Start()
    {
        expManager = ExpManager.instance;
        expManager.onExpChangedCallBack += NextGun;
    }

    void NextGun() {

        if (expManager.expGunCurrent >= listGun[indexGun].exp)
        {
            listGun[indexGun].enable = true;
            if (indexGun < listGun.Count - 1) indexGun += 1;
        }
    }

    void NextBullet() { 
        
    }

    public ScriptAbleObject_Gun GetGun() {
        return listGun[indexGun];
    }

    public ScriptAbleObject_Bullet GetBullet() {
        return listBullet[indexBullet];
    }



}
