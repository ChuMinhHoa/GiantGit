using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAnimation : MonoBehaviour
{
    public static CameraAnimation instance;

    public AimAndShoot ais;
    Animator myAnim;

    [Range(0, 5)]
    public float hitTime;
    [Range(0, 5)]
    public float scopeTime;
    [Range(0, 5)]
    public float scopeFirstTime;
    [Range(0, 5)]
    public float hitOtherTime;

    public CinemachineVirtualCamera followCamera;
    public CinemachineVirtualCamera afterCamera;
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera streetCamera; // follow street or giant.

    public CinemachineStateDrivenCamera myDriven;

    //ScopeOn
    public float fiewOfViewDefault, fiewOfViewScopeOn;
    public GameObject gunCamera;
    public GameObject scopeUI;

    public CameraStatusNow cameraStatus;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        fiewOfViewDefault = mainCamera.m_Lens.FieldOfView;
    }

    private void Update()
    {
        if (ais == null)
            ais = GameObject.FindObjectOfType<AimAndShoot>();
        if (gunCamera == null)
            gunCamera = GameObject.FindGameObjectWithTag("GunCam");
        if (myDriven.Follow == null && ais != null)
            myDriven.Follow = ais.gameObject.transform;
    }

    public void FollowBullet() {
        //ScopeOff
        ScopeControll(false, false);
        myAnim.SetBool("BulletFollow", true);
        ais.myAnim.SetBool("OnScope", false);
        mainCamera.m_Lens.FieldOfView = fiewOfViewDefault;
        cameraStatus = CameraStatusNow.FollowBullet;

    }

    void ScopeControll(bool _activeGunCam, bool _activeScopeUI) {
        gunCamera.SetActive(_activeGunCam);
        scopeUI.SetActive(_activeScopeUI);
    }

    public void HitEnemy() {

        followCamera.gameObject.SetActive(false);

        myAnim.SetBool("BulletFollow", false);
        myAnim.SetBool("HitEnemy", true);

        cameraStatus = CameraStatusNow.FollowEnemy;

        gunCamera.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(BackToMainCam(hitTime));
    }

    IEnumerator BackToMainCam(float _time) {

        yield return new WaitForSeconds(_time);
        afterCamera.gameObject.SetActive(false);

        ais.Reload();
        ais.headNow = false;

        myAnim.SetBool("HitEnemy", false);
        gunCamera.SetActive(true);

        cameraStatus = CameraStatusNow.Normal;

        myAnim.SetBool("StreetOff", false);

        StopAllCoroutines();
        StartCoroutine(UnReload());

    }

    public void MainCam() {

        myAnim.SetBool("BulletFollow", false);
        myAnim.SetBool("HitEnemy", false);
        StopAllCoroutines();
        StartCoroutine(ScopeOn(scopeTime));

    }

    IEnumerator ScopeOn(float _time) {

        yield return new WaitForSeconds(_time);
        

        BulletControll[] bullets = GameObject.FindObjectsOfType<BulletControll>();
        for (int i = 0; i < bullets.Length; i++)
            Destroy(bullets[i].gameObject);
        if (LevelManager.instance.giantCountSignal > 0)
        {
            ais.myAnim.SetBool("OnScope", true);

            ais.status = GunStatus.scope;
            ScopeControll(false, true);
            mainCamera.m_Lens.FieldOfView = fiewOfViewScopeOn;

        }
        else {
            ais.myAnim.SetBool("OnScope", false);
            ais.myAnim.SetBool("Reload", true);
            ScopeControll(true, false);
        }
    }

    IEnumerator UnReload() {

        yield return new WaitForSeconds(scopeTime);

        ais.UnReload();

        MainCam();

    }

    public void HitOther() {
        StopAllCoroutines();
        StartCoroutine(BackToMainCam(hitOtherTime));

        ScopeControll(true, false);

        myAnim.SetBool("BulletFollow", false);
        followCamera.gameObject.SetActive(false);

        gunCamera.SetActive(true);

        mainCamera.m_Lens.FieldOfView = fiewOfViewDefault;

    }

    public void NowStart() {
        StartCoroutine(StartNow());
    }

    IEnumerator StartNow() {
        yield return new WaitForSeconds(4f);
        myAnim.SetBool("StreetOn", false);
        myAnim.SetBool("StreetOff", true);
        streetCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        StartCoroutine(BackToNormal());
    }

    IEnumerator BackToNormal() {
        yield return new WaitForSeconds(2f);
        ais.StartNow();
        gunCamera.SetActive(true);
        myAnim.SetBool("StreetOff", false);
        StartCoroutine(ScopeOn(scopeFirstTime));
    }

    public void StreetOn(Transform _lookAt, Transform _myPosition) {
        streetCamera.transform.position = _myPosition.position;
        streetCamera.LookAt = _lookAt;
        gunCamera.SetActive(false);
        myAnim.SetBool("StreetOn", true);
        NowStart();
    }

    public void StreetOnWait(Transform _lookAt, Transform _myPosition) {
        streetCamera.transform.position = _myPosition.position;
        streetCamera.LookAt = _lookAt;
        gunCamera.SetActive(false);
        myAnim.SetBool("StreetOn", true);
    }

    public void StreetOff(Transform _lookAt, Transform _myPosition)
    {
        streetCamera.transform.position = _myPosition.position;
        streetCamera.LookAt = _lookAt;
        gunCamera.SetActive(false);
        myAnim.SetBool("StreetOn", true);
    }

    public void ResetAll() {

        MyCameraControll.instance.ResetAll();
        scopeUI.SetActive(false);
        gunCamera.SetActive(false);
        myAnim.SetBool("BulletFollow", false);
        myAnim.SetBool("HitEnemy", false);
        myAnim.SetBool("StreetOff", false);
        myAnim.SetBool("StreetOn", false);
        cameraStatus = CameraStatusNow.Normal;
        mainCamera.m_Lens.FieldOfView = fiewOfViewDefault;
        
    }
}
public enum CameraStatusNow { FollowBullet, FollowEnemy, Normal}
