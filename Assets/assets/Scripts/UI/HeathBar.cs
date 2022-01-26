using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    CinemachineVirtualCamera[] myVirtualCamera;
    public Slider myHeath;

    public Giant myGiant;

    private void Start()
    {
        myHeath.maxValue = myGiant.heath;
    }
    private void FixedUpdate()
    {
        
        ChangeHeathSlide();
    }

    public void ChangeHeathSlide() {
        myHeath.value = myGiant.heath;
        if (myGiant.heath <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void LateUpdate()
    {
        myVirtualCamera = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
        FollowCamera();
    }

    void FollowCamera(){
        if (myVirtualCamera.Length > 1)
        {
            transform.LookAt(transform.position + myVirtualCamera[1].transform.forward);
        }
        else { transform.LookAt(transform.position + myVirtualCamera[0].transform.forward); }
    }

}
