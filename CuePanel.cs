using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuePanel : BasePanel
{
    public Button buttonBack;
    protected new void Awake()
    {
        base.Awake();
        if (buttonBack == null) buttonBack = this.transform.Find("ButtonBcak").GetComponent<Button>();
        buttonBack.onClick.AddListener(() => 
        {
            //µã»÷·µ»Ø°´Å¥
            UIManager.Instance.Hide<CuePanel>();
        });
    }
}
