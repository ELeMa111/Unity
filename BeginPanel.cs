using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel<BeginPanel>
{
    public UIButton buttonStart;
    public UIButton buttonRank;
    public UIButton buttonSetting;
    public UIButton buttonExit;
    public override void Initialize()
    {
        buttonStart.onClick.Add(new EventDelegate(StartAction));
        buttonRank.onClick.Add(new EventDelegate(RankAction));
        buttonSetting.onClick.Add(new EventDelegate(SettingAction));
        buttonExit.onClick.Add(new EventDelegate(ExitAction));
    }
    private void StartAction()
    {
        //点击开始按钮
        Debug.Log("点击开始按钮");
        ChoosePanel.Instance.Show();
        Hide();
    }
    private void RankAction()
    {
        Debug.Log("点击排行榜按钮");
        //点击排行榜按钮
        RankPanel.Instance.Show();
    }
    private void SettingAction()
    {
        Debug.Log("点击设置按钮");
        SettingPanel.Instance.Show();
        //点击设置按钮
    }
    private void ExitAction()
    {
        Debug.Log("点击退出按钮");
        //点击退出按钮
        Application.Quit();
    }
}
