using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel<SettingPanel>
{
    public UIButton buttonBack;
    public UISlider sliderMusic;
    public UISlider sliderSound;
    public UIToggle toggleMusic;
    public UIToggle toggleSound;

    public override void Initialize()
    {
        buttonBack.onClick.Add(new EventDelegate(BackAction));
        sliderMusic.onChange.Add(new EventDelegate(MusicSliderAction));
        sliderSound.onChange.Add(new EventDelegate(SoundSliderAction));
        toggleMusic.onChange.Add(new EventDelegate(MusicToggleAction));
        toggleSound.onChange.Add(new EventDelegate(SoundToggleAction));

        //RefreshAudioData();

        Hide();
    }
    private void BackAction()
    {
        //点击返回按钮
        Hide();
    }
    private void MusicSliderAction()
    {
        //滑动音乐滑动条
        GameDataManager.Instance.SetMusic(sliderMusic.value);
    }
    private void SoundSliderAction()
    {
        //滑动音效滑动条
        GameDataManager.Instance.SetSound(sliderSound.value);
    }
    private void MusicToggleAction()
    {
        //点击音乐多选框
        GameDataManager.Instance.SetMusic(toggleMusic.value);
    }
    private void SoundToggleAction()
    {
        //点击音效多选框
        GameDataManager.Instance.SetSound(toggleSound.value);
    }
    //更新数据
    private void RefreshAudioData()
    {
        sliderMusic.value = GameDataManager.Instance.audioData.music;
        sliderSound.value = GameDataManager.Instance.audioData.sound;
        toggleMusic.value = GameDataManager.Instance.audioData.musicOn;
        toggleSound.value = GameDataManager.Instance.audioData.soundOn;
    }
    //重写的激活方法，更新数据
    public override void Show()
    {
        base.Show();
        RefreshAudioData();
    }
    //重写的失活方法，保存数据
    public override void Hide()
    {
        GameDataManager.Instance.SaveAudioData();
        base.Hide();
    }
}
