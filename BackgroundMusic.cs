using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;
    public static BackgroundMusic Instance {  get { return instance; } }

    private AudioClip backgroundMusic;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        
        audioSource = GetComponent<AudioSource>();
        //监听背景音乐数据更新，更新时重设背景音乐大小/开关
        GameDataManager.Instance.OnMusicChange += RefreshMusicValue;
        //开启协程加载数据
        StartCoroutine(LoadMusic());
        DontDestroyOnLoad(this.gameObject);
    }
    private IEnumerator LoadMusic()
    {
        ResourceRequest rq = Resources.LoadAsync<AudioClip>("Audio/BKMusic1");
        yield return rq;
        backgroundMusic = rq.asset as AudioClip;
        if (backgroundMusic != null) { Debug.Log("BKMusic加载完成"); }
        if (audioSource != null) 
        {
            audioSource.clip = backgroundMusic;
            RefreshMusicValue();
            audioSource.Play();
        }
    }
    //更新背景音乐数据
    private void RefreshMusicValue()
    {
        if (audioSource != null)
        {
            audioSource.volume = GameDataManager.Instance.audioData.music;
            audioSource.mute = !GameDataManager.Instance.audioData.musicOn;
        }
    }
}
