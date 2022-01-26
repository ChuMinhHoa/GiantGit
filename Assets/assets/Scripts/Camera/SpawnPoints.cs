using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public static SpawnPoints instance;

    public Transform[] spawnPoints;
    public GameObject[] giantObj;
    public Transform followGiant;

    [SerializeField]
    List<GameObject> giantsSpawned;

    public void SetInstance() {
        instance = this;
    }

    public void SpawnGiantNow(int giantCount) {
        for (int i = 0; i < giantCount; i++)
        {
            GameObject giant = Instantiate(giantObj[i], spawnPoints[i].position, Quaternion.identity);
            giantsSpawned.Add(giant);
        }

        int giantIndex = Random.Range(0, giantCount);
        followGiant = giantsSpawned[giantIndex].transform;
        GetComponentInParent<CameraSpawnPoint>().followPoint = followGiant;
    }
}
