using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerPanel : BasePanel
{
    public InputField inputField;
    public Button buttonChoose;
    public Button buttonEnter;

    protected new void Awake()
    {
        base.Awake();
        inputField.text = "";
        RefreshLabel();
        buttonChoose.onClick.AddListener(() =>
        {
            //点击换区按钮
            UIManager.Instance.Show<ChoosePanel>();
            UIManager.Instance.Hide<ServerPanel>();
        });
        buttonEnter.onClick.AddListener(() =>
        {
            //点击进入按钮
            UIManager.Instance.Hide<ServerPanel>();
            SceneManager.LoadScene("GameScene");
        });
    }
    private void RefreshLabel()
    {
        int i = GameDataManager.Instance.thisUser.lastServer;
        if (i == 0) i = 1;
        string name = GameDataManager.Instance.serverData.serverList[i - 1].name;
        inputField.text = i.ToString() + " 区 " + name;
    }
}
