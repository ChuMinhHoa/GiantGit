using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpUIManager : MonoBehaviour
{
    public static ExpUIManager instance;
    public Image expSlider;
    ExpManager expManager;
    public TextMeshProUGUI persentExp;

    float fillAmountNow;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        expManager = ExpManager.instance;

        expManager.onExpChangedCallBack += ChangeExp;
    }

    public void ChangeExp() {
        if (expManager == null)
            expManager = ExpManager.instance;
        fillAmountNow = (float)expManager.GetGunExp() / expManager.GetNextGunExp();
        expSlider.fillAmount = 0;
        StartCoroutine(ChangeCFUI());
    }

    IEnumerator ChangeCFUI() {
        while (expSlider.fillAmount < fillAmountNow)
        {
            expSlider.fillAmount += .01f;
            persentExp.text = ((int)(expSlider.fillAmount * 100)).ToString() + "%";
            yield return new WaitForSeconds(0.01f);
        }
        
    }
}
