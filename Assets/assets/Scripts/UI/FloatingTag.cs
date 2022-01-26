using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FloatingTag : MonoBehaviour
{
    public Transform lookAt;
    [SerializeField] Vector3 offset, pos;
    Camera cam;

    [SerializeField] Vector2 maxPos, minPos;

    bool outSite;

    public Image imageTriangle;
    public Color myColor;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        RectTransform myRect = GetComponent<RectTransform>();

        maxPos.x = Screen.width - (myRect.rect.width / 2);
        maxPos.y = Screen.height - (myRect.rect.height / 2);
        minPos.x = 0 + (myRect.rect.width / 2);
        minPos.y = 0 + (myRect.rect.height / 2);

        imageTriangle.color = myColor;
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (lookAt != null)
        {
            Vector3 target = lookAt.position + offset;
            Vector3 dir = target - transform.position;
            float angle = Vector3.Angle(dir,transform.forward);


            pos = cam.WorldToScreenPoint(target);
            CheckOutSite();
            transform.position = pos;

        }
        else Destroy(gameObject);

        if (!outSite)
        {
            imageTriangle.color = new Color(imageTriangle.color.r, imageTriangle.color.g, imageTriangle.color.b, 0);
        }
        else {
            imageTriangle.color = new Color(imageTriangle.color.r, imageTriangle.color.g, imageTriangle.color.b, 255);
        }

    }

    void CheckOutSite()
    {

        if (pos.x <= maxPos.x && pos.y <= maxPos.y && pos.x >= minPos.x && pos.y >= minPos.y)
        {
            outSite = false;
        }

        if (pos.x > maxPos.x)
        {
            pos.x = maxPos.x;
            outSite = true;
        }

        if (pos.y > maxPos.y)
        {
            pos.y = maxPos.y;
            outSite = true;
        }


        if (pos.y < minPos.y)
        {
            pos.y = minPos.y;
            outSite = true;
        }

        if (pos.x < minPos.x)
        {
            pos.x = minPos.x;
            outSite = true;
        }
    }
}
