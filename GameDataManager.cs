using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance { get { return instance; } }

    public MusicandSoundValue musicandSoundValue;
    public RankList rankList;

    private GameDataManager()
    {
        //初始化游戏数据
        musicandSoundValue = PlayerPrefsDataManager.Instance.LoadData(typeof(MusicandSoundValue), "MusicandSoundValue") as MusicandSoundValue;
        //如果第一次进入游戏没有数据，返回值会是默认值0，0f，false
        if (!musicandSoundValue.notFirstTimeLoad)
        {
            musicandSoundValue.notFirstTimeLoad = true;
            musicandSoundValue.MusicOn = true;
            musicandSoundValue.SoundOn = true;
            musicandSoundValue.MusicValue = 1f;
            musicandSoundValue.SoundValue = 1f;
            PlayerPrefsDataManager.Instance.SaveData(musicandSoundValue, "MusicandSoundValue");
        }

        //初始化排行耪数据
        rankList = PlayerPrefsDataManager.Instance.LoadData(typeof(RankList), "RankList") as RankList;
    }

    public void SaveMusicandSoundValue()
    {
        PlayerPrefsDataManager.Instance.SaveData(musicandSoundValue, "MusicandSoundValue");
    }
    public void AddRankInfo(string name,int score,float time)
    {
        RankItem rankItem = new RankItem(name,score,time);
        rankList.list.Add(rankItem);
        //排序
        //Sort排序可以用Lambda表达式+三目运算符
        rankList.list.Sort((a,b) => 
        { return a.time < b.time ? -1 : 1; });
        for(int i = rankList.list.Count - 1; i >= 10; i--)
        {
            rankList.list.RemoveAt(i);
        }
        PlayerPrefsDataManager.Instance.SaveData(rankList, "RankList");
    }
}
