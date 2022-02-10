using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowView : MonoBehaviour
{

    public Image icon;
    public List<Slot> slots;

    public GameObject objCreateNow;

    public void AddSlot(Slot _slot) {
        slots.Add(_slot);
    }

    public void ChangeView(GameObject objShow) {
        
        float width = icon.GetComponent<RectTransform>().rect.width;
        float height = icon.GetComponent<RectTransform>().rect.height;

        if (objCreateNow != null)
            Destroy(objCreateNow.gameObject);

        GameObject objShowCreated =
            Instantiate(objShow,
            icon.transform.position,
            objShow.transform.rotation,
            icon.transform);

        objCreateNow = objShowCreated;
        Vector3 newScale = new Vector3(width / 5, width / 5, height / 5);
        objShowCreated.transform.localScale = newScale;

        
    }
}
