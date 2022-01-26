using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneChangeManager : MonoBehaviour
{
    public static SceneChangeManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;

    }

    public Animator loadAnim;
    public GameObject loadPanelAnim;

    [Range(0, 2)]
    public float loadSceneTime;
    public int maxLevel;

    public bool changeCameraNow;

    public void ChangeScene(int sceneIndex) {
        loadPanelAnim.SetActive(true);
        loadAnim.SetTrigger("LoadIn");
        CameraAnimation.instance.gunCamera.SetActive(false);
        StartCoroutine(LoadScene(sceneIndex));
    }

    public int GetCurrentScene() {
        return SceneManager.GetActiveScene().buildIndex;
    }

    AsyncOperation operation;

    IEnumerator LoadScene(int sceneIndex) {
        yield return new WaitForSeconds(loadSceneTime);

        CameraAnimation.instance.ResetAll();
        if (sceneIndex <= maxLevel)
        {
            operation = SceneManager.LoadSceneAsync(sceneIndex);
            StartCoroutine(LevelChange());
        }

    }

    IEnumerator LevelChange() {
        while (!operation.isDone)
            yield return null;
        CameraSpawnPointsController.instance.GetCameraSpawnPoints();

        loadAnim.SetTrigger("LoadOut");
        changeCameraNow = true;

        UIManager.instance.ChangeScene();

        MyCameraControll.instance.status = CameraStatus.FollowGun;

        StopAllCoroutines();
        StartCoroutine(DeActiveLoadPanel());
    }

    IEnumerator DeActiveLoadPanel() {

        yield return new WaitForSeconds(1f);
        loadPanelAnim.SetActive(false);

    }

}
