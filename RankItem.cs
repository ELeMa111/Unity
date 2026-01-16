using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    public UILabel labelRank;
    public UILabel labelName;
    public UILabel labelTime;

    public void Initialize(int rank,string name,float time)
    {
        labelRank.text = rank.ToString();
        labelName.text = name;
        int newTime = Convert.ToInt32(time);
        string timeStr = (newTime / 60 > 0 ? (newTime / 60).ToString() + "·Ö" : "") +
                         (newTime % 60).ToString() + "Ãë";
        labelTime.text = timeStr;
    }
}
