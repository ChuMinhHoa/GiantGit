using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Giant : MonoBehaviour
{

    public float timeToCheck, timeToCheckSetting;
    public float rotageSpeed;

    public Color giantColor;

    public float heath;

    Animator myAnim;
    Rigidbody myBody;

    public GiantStatus giantStatus, oldGiantStatus;
    [SerializeField] bool onDoSomething, stopMoveToAttack;
    public float decisionCoolDown, decisionCoolDownSetting;


    //Giant Move
    [SerializeField] Vector3 targetPosition;
    float maxZ, minZ;
    float maxX, minX;
    public float distanceToNormal;


    //Giant kick
    public float radiusCheck;
    public LayerMask whatIsHouse;
    public Transform checkPoint;

    public Transform kickPoint;
    public float kickPointRadius, kickForce;
    [SerializeField] GameObject targetKick;

    public Collider[] colliders;

    //Giant navmesh
    NavMeshAgent myNavMesh;

    //Force
    [Range(0, 100)]
    public float force;

    public bool plsCheckRotage;


    private void Awake()
    {
        myNavMesh = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SetUpSizeMove();

        giantColor = GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color;

        TagController.instance.CreateTag(giantColor, transform);

        
    }

    void SetUpSizeMove() {

        GiantManager giManager = GiantManager.instance;

        maxZ = giManager.maxZ;
        maxX = giManager.maxX;

        minZ = giManager.minZ;
        minX = giManager.minX;

    }

    private void FixedUpdate()
    {
        
        
        if (heath > 0)
        {
            if (plsCheckRotage)
            {
                CheckRotage();
            }

            if (myNavMesh.enabled == true)
            {
                if (giantStatus != GiantStatus.DieFake)
                {
                    RandomDecision();
                    if (onDoSomething)
                        DoSomeThing();
                }
            }
                

        }
        else
        {
            Invoke("MinusGiant", 4f);
            Destroy(gameObject, 4f);
        }
    }

    void MinusGiant() {
        LevelManager.instance.GiantMinus();
    }

    void RandomDecision() {

        if (decisionCoolDown <= 0f && !onDoSomething)
        {
            int decision = Random.Range(0, 2);
            if (decision == 0)
            {
                giantStatus = GiantStatus.Kick;
                onDoSomething = true;
            }
            else if (decision == 1)
            {
                giantStatus = GiantStatus.Move;
                onDoSomething = true;
            }

            decisionCoolDown = decisionCoolDownSetting;
        }
        else if (decisionCoolDown > 0) decisionCoolDown -= Time.deltaTime;

    }

    void DoSomeThing() {
        if (giantStatus == GiantStatus.Move)
            GiantMoveRandom();
        else if (giantStatus == GiantStatus.Kick)
            GiantKick();
    }

    void GiantKick() {

        if (stopMoveToAttack)
        {
            //kick
            if (targetKick != null)
            {
                Vector3 targetPos = targetKick.transform.position;
                Vector3 myPos = transform.position;

                targetPos.y = 0;
                myPos.y = 0;

                Vector3 dir = targetPos - myPos;

                Quaternion toRotage = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotage, rotageSpeed * Time.deltaTime);

                myAnim.SetTrigger("Kick");
            }
            
        }
        else {
            //move to targetAttack
            MoveToTargetAttack();
        }

    }

    public void UnKick() {

        myAnim.ResetTrigger("Kick");

        targetKick = null;

        onDoSomething = true;
        giantStatus = GiantStatus.Move;

        stopMoveToAttack = false;
    }

    public void OnKickCheck() {

        Collider[] hit = Physics.OverlapSphere(kickPoint.position, kickPointRadius, whatIsHouse);

        for (int i = 0; i < hit.Length; i++)
        {
            Building targetBuilding = hit[i].GetComponent<Building>();
            targetBuilding.Kicked(kickPoint, kickForce);
        }

    }

    void MoveToTargetAttack() {

        if (targetKick == null)
        {
            
            Collider[] hit = Physics.OverlapSphere(checkPoint.position, radiusCheck, whatIsHouse);

            if (hit.Length > 0)
            {
                targetKick = hit[0].gameObject;
            }
            else 
                giantStatus = GiantStatus.Move;
        }
        else
        {
            if (CheckDistanceToTargetAttack())
            {
                myNavMesh.isStopped = true;
                stopMoveToAttack = true;
            }
            else
                GiantMoveDeFault(targetKick.transform.position);
        }
    
    }

    bool CheckDistanceToTargetAttack() {

        Vector3 posNow = kickPoint.position;
        Vector3 targetPos = targetKick.transform.position;

        posNow.y = 0;
        targetPos.y = 0;

        float distance = Vector3.Distance(posNow, targetPos);

        Building building = targetKick.GetComponent<Building>();
        float distanceStop = (kickPointRadius / 2) + building.width;

        if (distance <= distanceStop)
            return true;
        return false;
    }

    void GiantMoveRandom() {

        if (targetPosition == Vector3.zero)
        {
            float posX = Random.Range(minX, maxX);
            float posZ = Random.Range(minZ, maxZ);

            Vector3 target = new Vector3(posX, 0, posZ);
            targetPosition = target;
        }
        targetPosition.y = 0;

        Vector3 posNow = transform.position;
        Vector3 targetPos = targetPosition;
        posNow.y = 0;
        targetPos.y = 0;

        float distance = Vector3.Distance(targetPos, posNow);

        if (distance <= distanceToNormal)
        {
            myNavMesh.isStopped = true;
            targetPosition = Vector3.zero;
            onDoSomething = false;
        }
        else
            GiantMoveDeFault(targetPosition);
    }

    void GiantMoveDeFault(Vector3 _target){
        myNavMesh.isStopped = false;
        myNavMesh.destination = _target;
    }


    public void TakeDamage(float heathUpdate) {
        heath += heathUpdate;
        myBody.freezeRotation = false;
        myAnim.SetBool("Die", true);
        if (heath <= 0f)
        {
            myNavMesh.enabled = false;
            LevelManager.instance.MinusSignal();
        }
        else {


            myNavMesh.isStopped = true;
            myNavMesh.enabled = false;
            onDoSomething = true;

            //DieFake();

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPoint.position, radiusCheck);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(kickPoint.position, kickPointRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, targetPosition);
    }


    public void CheckRotage() {


        Quaternion rotation = transform.rotation;
        Vector2 myRotageCheck = new Vector2(rotation.x, rotation.z);


        if ( CameraAnimation.instance.cameraStatus == CameraStatusNow.Normal && myRotageCheck != Vector2.zero)
        {
            BackToRotage();
        }
    }

    void BackToRotage() {
        myBody.freezeRotation = true;
        Quaternion toRotage = new Quaternion(0, 0, 0, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotage, Time.deltaTime * rotageSpeed);
        StartCoroutine(NormalMode());
    }

    IEnumerator NormalMode() {
        yield return new WaitForSeconds(2f);
        myNavMesh.enabled = true;
        myAnim.SetBool("Die", false);
        myBody.freezeRotation = false;
        plsCheckRotage = false;
    }

}

public enum GiantStatus {Move, Kick, DieFake, BackToRotage}
