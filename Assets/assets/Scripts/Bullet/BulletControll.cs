using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BulletControll : MonoBehaviour
{

    CinemachineVirtualCamera virtualCam;
    [SerializeField]
    float speed;
    public float speedRotage;
    public Vector3 target;
    public LayerMask whatTarget;
    float angle;
    public float force;
    public float damage, timeLive;

    public Vector3 checkPoint;

    Rigidbody myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        virtualCam = MyCameraControll.instance.followGunCameraTransform.GetComponent<CinemachineVirtualCamera>();
        virtualCam.Follow = transform;
        myBody.AddForce((target - transform.position) * speed, ForceMode.Impulse);
        BuffManager buffManager = BuffManager.instance;
        damage = buffManager.powerBuff;
    }

    private void FixedUpdate()
    {
        angle += (Time.deltaTime * speedRotage);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void OnTriggerEnter(Collider other)
    {

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);

        AimAndShoot aS = GameObject.FindObjectOfType<AimAndShoot>();

        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            CameraAnimation.instance.HitEnemy();

            MyCameraControll.instance.headHitCamera.m_Follow = other.transform;

            if (other.gameObject.layer == 7)
            {

                Giant otherGiant = other.GetComponent<Giant>();
                Rigidbody otherBody = otherGiant.GetComponent<Rigidbody>();
                HitToEnemy(-damage, otherGiant, otherBody, other.transform);

            }

            if (other.gameObject.layer == 8)
            {
                
                Giant otherGiant = other.GetComponentInParent<Giant>();
                Rigidbody otherBody = otherGiant.GetComponentInParent<Rigidbody>();
                HitToEnemy(-damage * 2, otherGiant, otherBody, other.transform.parent);
                LevelManager.instance.ChangeReward(100);

            }

            SlowMotion.instance.BackToNormal();
            Destroy(gameObject, timeLive);
        }
        else
        {
            CameraAnimation.instance.HitOther();
            SlowMotion.instance.BackToNormal();
            Destroy(gameObject);
        }
    }

    void HitToEnemy(float _damage, Giant otherGiant, Rigidbody otherBody, Transform otherTransform) {

        otherGiant.TakeDamage(_damage);

        Vector3 vectorForce = (otherTransform.position - transform.position).normalized;
        vectorForce.y = 2f;

        otherBody.AddForce(vectorForce * -force, ForceMode.Impulse);
        otherGiant.plsCheckRotage = true;

    }

}
