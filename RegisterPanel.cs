using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public InputField inputAccount;
    public InputField inputPassword;
    public Button buttonBack;
    public Button buttonEnsure;

    private string account;
    private string password;

    protected new void Awake()
    {
        base.Awake();
        if(inputAccount==null)inputAccount = this.transform.Find("InputAccount").GetComponent<InputField>();
        if (inputPassword == null) inputPassword = this.transform.Find("InputPassword").GetComponent<InputField>();
        if (buttonBack == null) buttonBack = this.transform.Find("ButtonBack").GetComponent<Button>();
        if (buttonEnsure == null) buttonEnsure = this.transform.Find("ButtonEnsure").GetComponent<Button>();

        inputAccount.onEndEdit.AddListener((text) => 
        {
            //结束输入 账号
            account = text;
        });
        inputPassword.onEndEdit.AddListener((text) => 
        {
            //结束输入 密码
            password = text;
        });
        buttonBack.onClick.AddListener(() =>
        {
            //点击返回按钮
            UIManager.Instance/*.loginPanel*/.Show<LoginPanel>();
            //Hide();
            UIManager.Instance.Hide<RegisterPanel>();
        });
        buttonEnsure.onClick.AddListener(() => 
        {
        //点击确定按钮
            if (account == null || password == null)
            {
                UIManager.Instance.Show<CuePanel>();
            }
            else
            {
                UserItem item = new UserItem();
                item.account = account;
                item.password = password;
                if (!GameDataManager.Instance.AddUserData(item))
                {
                    Debug.Log("你非法了");
                }
                else
                    Debug.Log("succeed");
                UIManager.Instance.Show<LoginPanel>();
                UIManager.Instance.Hide<RegisterPanel>();
            }
        });
    }
}
