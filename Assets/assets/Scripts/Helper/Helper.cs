using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Helper : MonoBehaviour
{

    public float distanceToMakeNextDecision;

    public float maxX, maxZ, minX, minZ;

    public bool makeNewDecistionNow;

    public float distanceNow;

    [SerializeField]
    Vector3 targetPos;

    NavMeshAgent myNavMesh;

    private void Awake()
    {
        myNavMesh = GetComponent<NavMeshAgent>();   
    }

    private void Start()
    {
        SetLimitPos();
        distanceToMakeNextDecision = myNavMesh.stoppingDistance;
        makeNewDecistionNow = true;
    }

    void SetLimitPos() {

        maxX = GiantManager.instance.maxX;
        maxZ = GiantManager.instance.maxZ;

        minX = GiantManager.instance.minX;
        minZ = GiantManager.instance.minZ;

    }

    private void FixedUpdate()
    {
        if (makeNewDecistionNow)
        {
            MakeDecision();
        }

        if (CheckDistange())
        {
            makeNewDecistionNow = true;
        }

        myNavMesh.destination = targetPos;
    }

    bool CheckDistange() {

        Vector3 myPosNow = transform.position;
        myPosNow.y = 0;

        float distance = Vector3.Distance(targetPos, myPosNow);
        distanceNow = distance;

        if (distance <= distanceToMakeNextDecision)
            return true;

        return false;

    }

    void MakeDecision() {
        makeNewDecistionNow = false;
        targetPos = RandomPos();
        
    }


    Vector3 RandomPos(){
        float posX = Random.Range(minX, maxX);
        float posZ = Random.Range(minZ, maxZ);

        Vector3 newPos = new Vector3(posX, 0, posZ);
        return newPos;
    }

}
