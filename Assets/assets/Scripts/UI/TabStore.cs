using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabStore : MonoBehaviour
{
    public List<ScriptAbleObject_Bullet> listBullet;
    //public List<Gun>
    public bool gun;

    

    public GameObject slotObj;
    public Transform slotParents;

    private void Start()
    {
        if (!gun)
        {
            for (int i = 0; i < listBullet.Count; i++)
            {
                Slot slot = slotObj.GetComponent<Slot>();
                slot.bullet = listBullet[i];
                Instantiate(slotObj, slotParents.position, Quaternion.identity, slotParents);
            }
        }
    }

    

    
}
