using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    [SerializeField]
    private MyButton buttonBack;
    [SerializeField]
    private MyLabel[] labelNames;
    [SerializeField]
    private MyLabel[] labelScores;
    [SerializeField]
    private MyLabel[] labelTimes;

    private void Start()
    {

        //初始化
        try
        {
            //初始化buttonBack
            if (buttonBack == null) { buttonBack = this.transform.Find("MyWindow/ButtonBack").GetComponent<MyButton>(); }
            //初始化Label数组
            InitializeLabels();
        }
        catch { }
        finally { }
        buttonBack.ButtonAction += BackAction;

        //游戏开始时隐藏当前面板
        //主要是为了在游戏开始时实例化自己但不显示自己
        this.ShoworHide(false);
    }
    private void OnEnable()
    {
        RefreshData();
    }
    private void BackAction()
    {
        //返回按钮的响应事件
        BeginPanel.Instance.ShoworHide(true);
        this.ShoworHide(false);
    }
    //初始化Labels数组对象的方法
    private void InitializeLabels()
    {
        labelNames = new MyLabel[10];
        labelScores = new MyLabel[10];
        labelTimes = new MyLabel[10];
        int indexNames = 1;
        int indexScores = 1;
        int indexTimes = 1;
        MyLabel[] array;
        array = this.transform.GetComponentsInChildren<MyLabel>();
        for(int i = 0; i < array.Length; i++)
        {
            if (array[i].gameObject.name == "LabelName (" + indexNames + ")") 
            {
                labelNames[indexNames-1] = array[i];
                indexNames++;
            }
            if (array[i].gameObject.name == "LabelScore (" + indexScores + ")")
            {
                labelScores[indexScores-1] = array[i];
                indexScores++;
            }
            if (array[i].gameObject.name == "LabelTime (" + indexTimes + ")")
            {
                labelTimes[indexTimes-1] = array[i];
                indexTimes++;
            }
        }
    }

    private void RefreshData()
    {
        //更新Label信息
        List<RankItem> rankList = GameDataManager.Instance.rankList.list;
        int time;
        int minute;
        int second;
        for(int i = 0; i < rankList.Count; i++)
        {
            labelNames[i].content.text = rankList[i].name;
            labelScores[i].content.text = rankList[i].score.ToString();
            time = Convert.ToInt32(rankList[i].time);
            minute = time / 60;
            second = time % 60;
            labelTimes[i].content.text = minute + "分" + (second == 0 ? "0" : (second.ToString())) + "秒";
        }
    }
}
