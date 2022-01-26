using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MyCameraControll : MonoBehaviour
{
    public static MyCameraControll instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    public Joystick js;
    public AimAndShoot aim;

    private float xRotage = 0f, yRotage = 0f;
    public CameraStatus status;

    public float angle, rotageSpeed;

    public Transform mainCameraTransform;
    public Transform followGunCameraTransform;
    public Transform gunCameraTransform;
    public CinemachineVirtualCamera headHitCamera;
    public CinemachineVirtualCamera followBulletCamera;

    private void Start()
    {
        status = CameraStatus.FollowGun;
    }
    // Update is called once per frame
    void Update()
    {

        if (aim == null)
            aim = GameObject.FindObjectOfType<AimAndShoot>();

        if (status == CameraStatus.FollowGun && LevelManager.instance.giantCountSignal > 0)
        {
            FollowGun();
        }
        
    }

    void FollowGun() {
        float mouseX = js.Horizontal * aim.mouseSensitivity * Time.deltaTime;
        float mouseY = js.Vertical * aim.mouseSensitivity * Time.deltaTime;

        xRotage -= mouseY;
        xRotage = Mathf.Clamp(xRotage, -90f, 90f);

        yRotage += mouseX;
        yRotage = Mathf.Clamp(yRotage, -90f, 90f);

        mainCameraTransform.localRotation = Quaternion.Euler(xRotage, yRotage, transform.localRotation.z);

        gunCameraTransform.localRotation = Quaternion.Euler(xRotage, yRotage, transform.localRotation.z);

        gunCameraTransform.position = mainCameraTransform.position;
    }


    public void ResetAll() {

        status = CameraStatus.ResetNow;
        mainCameraTransform.rotation = new Quaternion(0, 0, 0, 0);
        xRotage = 0;
        yRotage = 0;

    }
}
public enum CameraStatus { FollowGun, FollowBullet, ResetNow }
