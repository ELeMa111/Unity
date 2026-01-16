using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanel : BasePanel<EndPanel>
{
    public UILabel result;
    public UIInput inputName;
    public UIButton buttonExit;
    private int endTime = 0;
    public override void Initialize()
    {
        buttonExit.onClick.Add(new EventDelegate(() =>
        {
            //µã»÷ÍË³ö°´Å¥
            GameDataManager.Instance.SaveNewRankData(inputName.value,endTime);
            Hide();
            SceneManager.LoadScene("BeginScene");
        }));
        Hide();
    }
    public override void Show()
    {
        endTime = (int)GamePanel.Instance.nowTime;
        result.text = GamePanel.Instance.labelTime.text;
        base.Show();
        Time.timeScale = 0f;
    }
    public override void Hide()
    {
        Time.timeScale = 1f;
        base .Hide();
    }
}
