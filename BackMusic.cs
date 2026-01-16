using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMusic : MonoBehaviour
{
    private static BackMusic instance;
    public static BackMusic Instance {  get { return instance; } }
    private void Awake()
    {
        instance = this;
        audioSource = this.transform.GetComponent<AudioSource>();
        ChangeValue(GameDataManager.Instance.musicandSoundValue.MusicValue);
        ChangeOpen(!GameDataManager.Instance.musicandSoundValue.MusicOn);
    }
    public AudioSource audioSource;
    public void ChangeValue(float value)
    {
        audioSource.volume = value;
    }
    public void ChangeOpen(bool isOpen)
    {
        audioSource.mute = isOpen;
    }
}
