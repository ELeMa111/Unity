using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : BasePanel<SettingPanel>
{
    [SerializeField]
    private MyButton buttonBack;
    [SerializeField]
    private MyToggle toggleMusic;
    [SerializeField]
    private MyToggle toggleSound;
    [SerializeField]
    private MySlider sliderMusic;
    [SerializeField]
    private MySlider sliderSound;
    #region 生命周期函数
    private void Start()
    {
        try
        {
            if (buttonBack == null) { buttonBack = this.transform.Find("MyWindow/ButtonBack").GetComponent<MyButton>(); }
            if (toggleMusic == null) { toggleMusic = this.transform.Find("MyWindow/ToggleMusic").GetComponent<MyToggle>(); }
            if (toggleSound == null) { toggleSound = this.transform.Find("MyWindow/ToggleSound").GetComponent<MyToggle>(); }
            if (sliderMusic == null) { sliderMusic = this.transform.Find("MyWindow/SliderMusic").GetComponent<MySlider>(); }
            if (sliderSound == null) { sliderSound = this.transform.Find("MyWindow/SliderSound").GetComponent<MySlider>(); }
        }
        catch { }
        finally { }
        buttonBack.ButtonAction += BackAction;
        sliderMusic.SliderAction += SliderMusicAction;
        sliderSound.SliderAction += SliderSoundAction;
        toggleMusic.ToggleAction += ToggleMusicAction;
        toggleSound.ToggleAction += ToggleSoundAction;
        this.ShoworHide(false);
    }
    private void OnEnable()
    {
        RefreshMusicandSoundData();
    }
    #endregion
    private void BackAction()
    {
        //返回按钮的点击响应事件

        Time.timeScale = 1.0f;
        #region 更新数据管理器的音乐数据
        //数据管理器中的数据应该在数据改变时就被修改，而不是在关闭面板时才被修改
        //因为还没有定义声音响应，在这里修改
        /*
        GameDataManager.Instance.musicandSoundValue.MusicOn = toggleMusic.newValue;
        GameDataManager.Instance.musicandSoundValue.SoundOn = toggleSound.newValue;
        GameDataManager.Instance.musicandSoundValue.MusicValue = sliderMusic.newValue;
        GameDataManager.Instance.musicandSoundValue.SoundValue = sliderSound.newValue;
        */
        #endregion

        //保存数据至硬盘的方法
        GameDataManager.Instance.SaveMusicandSoundValue();
        if(SceneManager.GetActiveScene().name == "BeginScene")
            BeginPanel.Instance.ShoworHide(true);
        else if(SceneManager.GetActiveScene().name=="GameScene")
            GamePanel.Instance.ShoworHide(true);
        this.ShoworHide(false);

        
    }
    private void SliderMusicAction(float newValue)
    {
        //音乐滑动条数值改变时的响应事件
        BackMusic.Instance.ChangeValue(newValue);
        //更新数据管理器的数据
        GameDataManager.Instance.musicandSoundValue.MusicValue = sliderMusic.newValue;
    }
    private void SliderSoundAction(float newValue)
    {
        //音效滑动条数值改变时的响应事件

        //更新数据管理器的数据
        GameDataManager.Instance.musicandSoundValue.SoundValue = sliderSound.newValue;
    }
    private void ToggleMusicAction(bool newValue)
    {
        //音乐多选框被点击的响应事件
        BackMusic.Instance.ChangeOpen(!newValue);
        //更新数据管理器的数据
        GameDataManager.Instance.musicandSoundValue.MusicOn = toggleMusic.newValue;
    }
    private void ToggleSoundAction(bool newValue)
    {
        //音效多选框被点击的响应事件

        //更新数据管理器的数据
        GameDataManager.Instance.musicandSoundValue.SoundOn = toggleSound.newValue;
    }
    //根据数据管理器数据更新面板显示的方法
    private void RefreshMusicandSoundData()
    {
        try
        {
            MusicandSoundValue data = GameDataManager.Instance.musicandSoundValue;
            toggleMusic.newValue = data.MusicOn;
            toggleSound.newValue = data.SoundOn;
            sliderMusic.newValue = data.MusicValue;
            sliderSound.newValue = data.SoundValue;
        }
        catch { }
        finally { }
    }
}
