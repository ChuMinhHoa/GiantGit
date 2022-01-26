using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagController : MonoBehaviour
{
    public static TagController instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    public GameObject myTag;
    public GameObject canvasParrent;

    public void CreateTag(Color colorChange, Transform _lookAt) {
        GameObject myTagCreated = Instantiate(myTag, transform.position, Quaternion.identity, canvasParrent.transform);
        FloatingTag targetTag = myTagCreated.GetComponent<FloatingTag>();
        targetTag.lookAt = _lookAt;
        targetTag.myColor = colorChange;
    }
}
