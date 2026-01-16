using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class GamePanel : BasePanel<GamePanel>
{
    [SerializeField]
    private MyLabel labelScore;
    [SerializeField]
    private MyLabel labelTime;
    [SerializeField]
    private MyButton buttonSetting;
    [SerializeField]
    private MyButton buttonBack;
    [SerializeField]
    private MyTexture textureHP;
    [SerializeField]
    private MyTexture textureMap;
    [HideInInspector]
    public int score = 0;

    [HideInInspector]
    public float time = 0;

    private new void Awake()
    {
        base.Awake();

        //初始化成员
        try
        {
            if (labelScore == null) { labelScore = this.transform.Find("LabelScore").GetComponent<MyLabel>(); }
            if (labelTime == null) { labelTime = this.transform.Find("LabelTime").GetComponent<MyLabel>(); }
            if (buttonSetting == null) { buttonSetting = transform.Find("ButtonSetting").GetComponent<MyButton>(); }
            if (buttonBack == null) { buttonBack = this.transform.Find("ButtonBack").GetComponent<MyButton>(); }
            if (textureHP == null) { textureHP = this.transform.Find("TextureHP").GetComponent<MyTexture>(); }
            if (textureMap == null) { textureMap = this.transform.Find("TextureMap").GetComponent<MyTexture>(); }
        }
        catch { }
        finally { }
        //初始化委托
        buttonSetting.ButtonAction += SettingAction;
        buttonBack.ButtonAction += BackAction;
    }
    private void Update()
    {
        time += Time.deltaTime;
        RefreshTime();
    }
    private void SettingAction()
    {
        //打开设置面板
        //暂停时间
        //隐藏游戏面板防止穿透？
        Time.timeScale = 0;
        SettingPanel.Instance.ShoworHide(true);
        this.ShoworHide(false);
    }
    private void BackAction()
    {
        //打开确认面板？
        //切换场景到BeginScene？
        Time.timeScale = 0;
        EnsurePanel.Instance.ShoworHide(true);
        this.ShoworHide(false);
    }
    public void AddScore(int addNum)
    {
        score += addNum;
        //更新界面显示
        labelScore.content.text = score.ToString();
    }
    public void RefreshHP(int max,int nowHP)
    {
        textureHP.position.rect.width = (float)nowHP / max * 750;
    }
    private void RefreshTime()
    {
        int minute = (int)time / 60;
        int second = (int)time % 60;
        labelTime.content.text = (minute==0?"":minute.ToString()+"分")+(second.ToString()+"秒");
    }
}

