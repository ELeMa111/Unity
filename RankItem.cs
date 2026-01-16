using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem 
{
    public string name;
    public int score;
    public float time;
    public RankItem() { }
    public RankItem(string name,int score,float time) 
    {
        this.name = name;
        this.score = score;
        this.time = time;
    }
}
public class RankList
{
    public List<RankItem> list;
}
