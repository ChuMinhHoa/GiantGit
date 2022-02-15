using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowView : MonoBehaviour
{
    public List<Slot> slots;

    public Camera gunUICamera;
    public Camera bulletUICamera;

    public Joystick js;
    public Vector2 rotageX;
    public float rotageSpeed;
    public float angle = 90;

    public GameObject objView;
    public GameObject[] listObjView;

    GunAndBulletManager gABManager;

    public void AddSlot(Slot _slot) {
        slots.Add(_slot);
    }

    public void ChangeView(Slot slot) {
        int index = slots.IndexOf(slot);
        if (slot.gunBool)
        {
            Vector3 cameraGunPosition = gunUICamera.transform.localPosition;
            cameraGunPosition.x = 10 * index;
            gunUICamera.transform.localPosition = cameraGunPosition;
            objView = listObjView[index];
        }
        else {
            //ChangeBulletCamera
            Vector3 cameraBulletPosition = bulletUICamera.transform.localPosition;
            cameraBulletPosition.x = 10 * index;
            bulletUICamera.transform.localPosition = cameraBulletPosition;
            GunAndBulletManager.instance.currentBulletIndex = index;
            objView = listObjView[index];
        }
    }

    void Start() {
        objView = listObjView[0];
        gABManager = GunAndBulletManager.instance;
        gABManager.onGABchangedCallback += UnLock;
    }

    void FixedUpdate() {
        JoyStickChange();
        
        if (rotageX != Vector2.zero)
        {
            angle += rotageX.x * rotageSpeed;
            objView.transform.eulerAngles = new Vector3(-45, angle, 0);
        }
    }

    void JoyStickChange() {
        rotageX.x = js.Horizontal;
        rotageX.y = 0f;
    }

    void UnLock() {
        int maxIndexUnlock = gABManager.indexGun;
        for (int i = 0; i < maxIndexUnlock; i++)
        {
            slots[i].blockPanel.SetActive(false);
        }        
    }

    public int GetIndexSlot(Slot _slot) {
        return slots.IndexOf(_slot);
    }
}
