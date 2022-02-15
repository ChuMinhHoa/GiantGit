using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabStore : MonoBehaviour
{


    public List<ScriptAbleObject_Bullet> listBullet;
    public List<ScriptAbleObject_Gun> listGun;
    public bool gun;

    public GameObject slotObj;
    public Transform slotParents;

    public GunAndBulletManager gunAndBulletManager;

    public int indexPick;

    private void Start()
    {
        int maxCount = 0;
        int indexItemCatch = 0;
        indexPick = 0;
        if (gun)
        {
            maxCount = listGun.Count;
            indexItemCatch = gunAndBulletManager.indexGun;
        }
        else { 
            maxCount = listBullet.Count;
            indexItemCatch = gunAndBulletManager.indexBullet;
        }

        for (int i = 0; i < maxCount; i++)
        {
            Slot slot = slotObj.GetComponent<Slot>();
            if (!gun)
                slot.bullet = listBullet[i];
            else slot.gun = listGun[i];

            if (i <= indexItemCatch)
                slot.blockPanel.SetActive(false);
            else slot.blockPanel.SetActive(true);

            slot.gunBool = gun;
            Instantiate(slotObj, slotParents.position, Quaternion.identity, slotParents);
        }
    }

    public void ChangeButton()
    {

        //ChangeBullet or ChangeGun
        if (gun)
        {
            //change Gun
            gunAndBulletManager.currentGunIndex = indexPick;
            gunAndBulletManager.ChangeGun();
        }
        else {
            //change Bullet
            gunAndBulletManager.currentBulletIndex = indexPick;
            gunAndBulletManager.ChangeBullet();
        }

    }

    
}
