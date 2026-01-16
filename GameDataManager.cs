using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameDataManager
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance { get { return instance; } }

    public AudioData audioData;
    public UnityAction OnMusicChange;
    public RankData rankData;
    public RoleData roleData;
    public int nowSelHeroIndex = 0;
    public BulletData bulletData;
    public FireData fireData;
    private GameDataManager() 
    {
        Debug.Log("实例化GameDataManager");
        //初始化音乐数据
        audioData = XMLDataManager.Instance.LoadData<AudioData>("AudioData");
        if (audioData == null) 
        {
            Debug.Log("StreamingAssets和PersistentDataPath目录下未找到AudioData.xml");
            audioData = new AudioData(); 
        }
        //初始化排行榜数据
        rankData = XMLDataManager.Instance.LoadData<RankData>("RankData");
        if (rankData == null) { rankData = new RankData(); }
        //初始化角色数据
        roleData = XMLDataManager.Instance.LoadData<RoleData>("RoleData");
        //Debug.Log(roleData.roleList[nowSelHeroIndex].resName);
        //Debug.Log(Resources.Load<GameObject>(roleData.roleList[nowSelHeroIndex].resName));
        //读取子弹数据
        bulletData = XMLDataManager.Instance.LoadData<BulletData>("BulletData");
        //Debug.Log(bulletData);
        //读取FireData
        fireData = XMLDataManager.Instance.LoadData<FireData>("FireData");
    }
    #region 音乐/音效
    //提供给外部主动保存音乐音效数据的方法
    public void SaveAudioData()
    {
        XMLDataManager.Instance.SaveData<AudioData>(audioData, "AudioData");
    }

    /*
    课程中为公共的 audioData 设置了 set方法
    感觉纯粹是脱裤子放屁

    为修改音乐大小设置set方法，
    方便背景音乐播放器对修改音乐的行为进行监听

    为修改音乐和音效都设置 set方法
    音乐不是被面板/按钮/滑动条 修改
    而是面板/按钮/滑动条 通知数据管理器修改
    更符合面向对象的思想
    降低数据耦合
     */
    public void SetMusic(float value)
    {
        audioData.music = value;
        OnMusicChange?.Invoke();
    }
    public void SetMusic(bool value)
    {
        audioData.musicOn = value;
        OnMusicChange?.Invoke();
    }
    public void SetSound(float value)
    {
        audioData.sound = value;
    }
    public void SetSound(bool value)
    {
        audioData.soundOn = value;
    }
    #endregion
    #region 排行榜
    public void SaveNewRankData(string name,float time)
    {
        rankData.list.Add(new ItemData(name,time));
        rankData.list.Sort((a,b) => { return a.time > b.time ? -1 : 1; });
        //for(int i = rankData.list.Count - 1; i > 19; i++)
        //{
        //    rankData.list.RemoveAt(i);
        //}
        if (rankData.list.Count > 20)
            rankData.list.RemoveRange(20,rankData.list.Count-20);
        XMLDataManager.Instance.SaveData<RankData>(rankData, "RankData");
    }
    #endregion
    #region 玩家数据
    public RoleInfo GetNowSelHeroInfo()
    {
        return roleData.roleList[nowSelHeroIndex];
    }
    #endregion

}
