using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;
    public ScriptAbleObject_Bullet bullet;
    public ScriptAbleObject_Gun gun;

    public bool gunBool;
    public GameObject blockPanel;

    ShowView showViewNow;
    TabStore tabStore;

    private void Start()
    {
        StartCoroutine(WaitToCreate());
    }
    IEnumerator WaitToCreate() {
        yield return new WaitForSeconds(0.1f);
        CreateNow();
    }

    void CreateNow() {
        showViewNow = GameObject.FindObjectOfType<ShowView>();
        showViewNow.AddSlot(this);
        if (showViewNow.slots.Count == 0)
            ChangeView();
    }

    public void ChangeView() {
        showViewNow = GameObject.FindObjectOfType<ShowView>();
        showViewNow.ChangeView(this);

        tabStore = GameObject.FindObjectOfType<TabStore>();
        tabStore.indexPick = showViewNow.GetIndexSlot(this);
        
    }
}
