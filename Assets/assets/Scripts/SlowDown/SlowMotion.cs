
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public static SlowMotion instance;

    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);
        else instance = this;

    }

    public float slowFactor;
    public float slowLength;

    public void BackToNormal() {

        Time.timeScale = 1f;

    }

    public void DoSlowMotion() {

        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;

    }

}
