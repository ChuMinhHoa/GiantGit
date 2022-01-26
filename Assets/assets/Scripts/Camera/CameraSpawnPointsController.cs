using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawnPointsController : MonoBehaviour
{
    public static CameraSpawnPointsController instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }
    [SerializeField]
    CameraSpawnPoint[] cameraSpawnPoints;
    
    public void GetCameraSpawnPoints() {

        cameraSpawnPoints = GameObject.FindObjectsOfType<CameraSpawnPoint>();

        for (int i = 0; i < cameraSpawnPoints.Length; i++)
            cameraSpawnPoints[i].gameObject.SetActive(false);

        int index = LevelManager.instance.groupNow - 1;

        if (cameraSpawnPoints[index])
        {
            cameraSpawnPoints[index].gameObject.SetActive(true);
            cameraSpawnPoints[index].GetComponentInChildren<SpawnPoints>().SetInstance();
        }
        
    }
}
