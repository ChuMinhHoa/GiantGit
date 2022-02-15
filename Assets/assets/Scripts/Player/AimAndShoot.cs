using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class AimAndShoot : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public Camera fpsCamera;

    public float mouseSensitivity;
    public Animator myAnim;


    private float xRotage = 0f, mouseX;
    private float yRotage = 0f, mouseY;
    public float range;

    public float fieldOfViewSetting, defaultFielOfView;

    public GameObject scope;
    public GameObject gunCamera;

    public Joystick js;

    public GameObject bulletObj;
    public Transform shootPoint;

    public LayerMask whatIsEnemy;

    Touch touch;

    public GunStatus status;
    public Vector3 checkPoint;

    public bool headNow;

    [SerializeField]
    Vector3 targetChecked;

    //Bullet Check Distance;
    GameObject bulletNeedCheck;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        mainCamera = StageCinemachineInstance.instance.mainVirtualCamera;
        fpsCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gunCamera = CameraAnimation.instance.gunCamera;
        scope = CanvasInstance.instance.scope;
        js = CanvasInstance.instance.js;
    }


    public void StartNow() {
        myAnim.SetTrigger("StartNow");
        status = GunStatus.reload;
        CameraAnimation.instance.mainCamera.m_Follow = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.instance.canShoot && LevelManager.instance.giantCountSignal > 0)
        {
            if (Input.touchCount > 0)
            {

                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended && status == GunStatus.scope)
                {
                    Shoot();
                }
            }

            if (Input.GetMouseButtonUp(0) && status == GunStatus.scope)
            {
                Shoot();
            }

            MouseLook();
        }
    }

    private void FixedUpdate()
    {
        Cast();
        BulletDistance();
    }

    void BulletDistance() {

        if (bulletNeedCheck != null)
        {
            float distance = Vector3.Distance(transform.position, checkPoint);
            float distanceBullet = Vector3.Distance(transform.position, bulletNeedCheck.transform.position);
            if ((distance + 2f) < distanceBullet)
            {

                SlowMotion.instance.BackToNormal();
                CameraAnimation.instance.HitOther();
                Destroy(bulletNeedCheck);

            }
        }

    }

    void Cast() {
        if (status == GunStatus.scope)
        {
            Vector3 hitPoint = Vector3.zero;
            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                hitPoint = hit.point;
            else
                hitPoint = ray.GetPoint(75);

            checkPoint = hitPoint;
            checkPoint = checkPoint + new Vector3(0, 0, 0.5f);
        }
       
    }

    void MouseLook() {

        mouseX = js.Horizontal * mouseSensitivity * Time.deltaTime;
        mouseY = js.Vertical * mouseSensitivity * Time.deltaTime;

        xRotage -= mouseY;
        xRotage = Mathf.Clamp(xRotage, -90f, 90f);

        yRotage += mouseX;
        yRotage = Mathf.Clamp(yRotage, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotage, yRotage, transform.localRotation.z);

    }

    void Shoot() {
        Vector3 hitPoint;
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, whatIsEnemy))
        {
            hitPoint = hit.point;
            targetChecked = hit.point;
        }
        else { 
            hitPoint = ray.GetPoint(75);
            targetChecked = transform.position;
        }
        
        GameObject bullet = Instantiate(bulletObj, shootPoint.position, Quaternion.identity);
        bulletNeedCheck = bullet;
        status = GunStatus.shoot;
        
        bullet.GetComponent<BulletControll>().target = hitPoint;

        myAnim.SetBool("Shoot", true);
        if (headNow)
        {
            SlowMotion.instance.DoSlowMotion();
            gunCamera.SetActive(false);
            CameraAnimation.instance.FollowBullet();
        }
        else
            CameraAnimation.instance.HitOther();
        
    }

    public void Reload() {
        myAnim.SetBool("ReloadBullet", true);
        myAnim.SetBool("Shoot", false);

    }

    public void UnReload() {
        myAnim.SetBool("ReloadBullet", false);  
    }

    public void UnShoot() { myAnim.SetBool("Shoot",false); }

}
public enum GunStatus { shoot, reload, scope }
