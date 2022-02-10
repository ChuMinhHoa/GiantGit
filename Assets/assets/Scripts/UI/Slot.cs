using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;
    public ScriptAbleObject_Bullet bullet;
    public GameObject objShow;

    ShowView showViewNow;

    private void Start()
    {
        StartCoroutine(WaitToCreate());
    }
    IEnumerator WaitToCreate() {
        yield return new WaitForSeconds(0.1f);
        CreateNow();
    }
    void CreateNow() {
        objShow = bullet.objShow;
        showViewNow = GameObject.FindObjectOfType<ShowView>();

        if (objShow != null)
        {
            float width = icon.GetComponent<RectTransform>().rect.width;
            float height = icon.GetComponent<RectTransform>().rect.height;
            GameObject objShowCreated =
                Instantiate(objShow,
                icon.transform.position,
                objShow.transform.rotation,
                icon.transform);

            Vector3 newScale = new Vector3(width / 5, width / 5, height / 5);
            objShowCreated.transform.localScale = newScale;

        }

        if (showViewNow.slots.Count == 0)
            ChangeView();
        showViewNow.AddSlot(this);
    }

    public void ChangeView() {
        showViewNow = GameObject.FindObjectOfType<ShowView>();
        showViewNow.ChangeView(objShow);
    }
}
