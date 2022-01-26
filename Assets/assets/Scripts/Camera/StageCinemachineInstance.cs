using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StageCinemachineInstance : MonoBehaviour
{
    public static StageCinemachineInstance instance;

    public CinemachineVirtualCamera mainVirtualCamera;


    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }
}
