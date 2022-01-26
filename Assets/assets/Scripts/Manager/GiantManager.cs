using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantManager : MonoBehaviour
{
    #region singleton
    public static GiantManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }
    #endregion


    public float maxZ, minZ;
    public float maxX, minX;
}
