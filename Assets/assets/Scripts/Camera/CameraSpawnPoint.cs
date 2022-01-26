using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawnPoint : MonoBehaviour
{
    CameraAnimation cameraAnimation;

    public Transform startPoint, followPoint;

    private void Start()
    {
        cameraAnimation = CameraAnimation.instance;
        cameraAnimation.StreetOff(followPoint, startPoint);
    }
    private void Update()
    {
        if (SceneChangeManager.instance.changeCameraNow)
        {
            StartCoroutine(StartWithCamera());
        }

        if (UIManager.instance.followGiantNow)
        {
            StartCoroutine(StartGameNow());
        }
    }

    IEnumerator StartWithCamera() {
        yield return new WaitForSeconds(.1f);
        SceneChangeManager.instance.changeCameraNow = false;
        UIManager.instance.followGiantNow = false;
        cameraAnimation.StreetOnWait(followPoint, startPoint);
    }

    IEnumerator StartGameNow() {
        yield return new WaitForSeconds(.1f);
        SceneChangeManager.instance.changeCameraNow = false;
        UIManager.instance.followGiantNow = false;
        cameraAnimation.StreetOn(followPoint, startPoint);
    }
}
