using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class WinPanel : BasePanel<WinPanel>
{
    public MyTextField textfield;
    public MyButton buttonEnsure;

    private new void Awake()
    {
        base.Awake();
        if (textfield == null) { textfield = this.transform.Find("MyWindow/TextField").GetComponent<MyTextField>(); }
        if (buttonEnsure == null) { buttonEnsure = this.transform.Find("MyWindow/ButtonEnsure").GetComponent<MyButton>(); }

        buttonEnsure.ButtonAction += EnsureAction;
        this.ShoworHide(false);
    }
    private void EnsureAction()
    {
        //添加排行榜数据
        string name = textfield.content.text;
        int score = GamePanel.Instance.score;
        float time = GamePanel.Instance.time;
        GameDataManager.Instance.AddRankInfo(name, score, time);
        //游戏时间继续
        Time.timeScale = 1f;
        //返回开始界面
        SceneManager.LoadScene("BeginScene");
    }
}
