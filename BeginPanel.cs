using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BeginPanel : BasePanel<BeginPanel>
{
    [SerializeField]
    private MyButton buttonStart;
    [SerializeField]
    private MyButton buttonSetting;
    [SerializeField] 
    private MyButton buttonExit;
    [SerializeField] 
    private MyButton buttonRank;

    private void Start()
    {
        #region 初始化成员
        try
        {
            if (buttonStart == null) { buttonStart = this.transform.Find("ButtonStart").GetComponent<MyButton>(); }
            if (buttonSetting == null) { buttonSetting = this.transform.Find("ButtonSetting").GetComponent<MyButton>(); }
            if (buttonExit == null) { buttonExit = this.transform.Find("ButtonExit").GetComponent<MyButton>(); }
            if (buttonRank == null) { buttonRank = this.transform.Find("ButtonRank").GetComponent<MyButton>(); }
        }
        catch { }
        #endregion
        #region 初始化按钮点击事件
        buttonStart.ButtonAction += StartAction;
        buttonSetting.ButtonAction += SettingAction;
        buttonExit.ButtonAction += ExitAction;
        buttonRank.ButtonAction += RankAction;
        #endregion
    }
    private void StartAction()
    {
        //开始按钮点击事件
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("GameScene");
    }
    private void SettingAction() 
    {
        //设置按钮点击事件
        SettingPanel.Instance.ShoworHide(true);
        this.ShoworHide(false);
    }
    private void ExitAction() 
    { 
        //退出按钮点击事件
        Application.Quit();
    }
    private void RankAction()
    {
        //排行榜按钮点击事件
        RankPanel.Instance.ShoworHide(true);
        this.ShoworHide(false);
    }
}
