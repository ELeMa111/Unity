using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    public UIButton buttonBack;
    public UILabel labelTime;
    public List<GameObject> HPObjs;
    public float nowTime = 0; 

    public override void Initialize()
    {
        buttonBack.onClick.Add(new EventDelegate(() =>
        {
            //点击退出按钮
            EnsurePanel.Instance.Show();
        }));
        ChangeHP(2);
    }
    private void Update()
    {
        nowTime += Time.deltaTime;
        //更新时间显示
        int newTime = Convert.ToInt32(nowTime);
        string timeStr = (newTime / 60 > 0 ? (newTime / 60).ToString() + "分" : "") +
                         (newTime % 60).ToString() + "秒";
        labelTime.text = timeStr;
    }
    public void ChangeHP( int HP)
    {
        for(int i = 0; i < 10; i++)
        {
            //if (i < HP)
            //{
            //    HPObjs[i].SetActive(true);
            //}
            //else
            //{
            //    HPObjs[i].SetActive(false);
            //}
            HPObjs[i].SetActive(i < HP ? true : false);
        }
    }
}
