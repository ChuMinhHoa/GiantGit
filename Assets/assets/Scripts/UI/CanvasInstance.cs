using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInstance : MonoBehaviour
{
    public static CanvasInstance instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    public GameObject scope;
    public Joystick js;
}
