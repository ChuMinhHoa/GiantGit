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

    public int currentGunIndex;
    public int currentBulletIndex;

    public delegate void OnGABchange();
    public OnGABchange onGABchangedCallback;

    private void Start()
    {
        expManager = ExpManager.instance;
        expManager.onExpChangedCallBack += NextGun;
    }

    public void NextGun() {
        if (expManager == null)
            expManager = ExpManager.instance;

        if (expManager.expGunCurrent >= listGun[indexGun].exp)
        {
            listGun[indexGun].enable = true;
            if (indexGun < listGun.Count - 1) indexGun += 1;
            if (onGABchangedCallback != null)
                onGABchangedCallback.Invoke();
        }
    }

    void NextBullet() { 
        
    }

    public void ChangeGun() {
        Transform playerTransform = GameObject.FindObjectOfType<AimAndShoot>().transform;
        GameObject gun = Instantiate(listGun[currentGunIndex].objChange,
            playerTransform.position,
            Quaternion.identity,
            playerTransform);
        gun.transform.localPosition = listGun[currentGunIndex].objChange.transform.localPosition;
        gun.name = "Gun";

        playerTransform.gameObject.SetActive(false);
        playerTransform.gameObject.SetActive(true);
    }

    public void ChangeBullet() {
        AimAndShoot _player = GameObject.FindObjectOfType<AimAndShoot>();
        _player.bulletObj = listBullet[currentBulletIndex].objChange;
    }



}
