using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    #region 成员组件
    public InputField inputAccount;
    public InputField inputPassword;
    public Toggle toggleP;
    public Toggle toggleL;
    public Button buttonRegister;
    public Button buttonLogin;
    #endregion
    #region InputData
    private string account;
    private string password;
    private bool togglep;
    private bool togglel;
    #endregion
    protected new void Awake()
    {
        base.Awake();
        if (inputAccount == null) inputAccount = this.transform.Find("InputAccount").GetComponent<InputField>();
        if(inputPassword==null) inputPassword = this.transform.Find("inputPassword").GetComponent<InputField>();
        if (toggleP == null) toggleP = this.transform.Find("ToggleP").GetComponent<Toggle>();
        if (toggleL == null) toggleL = this.transform.Find("ToggleL").GetComponent<Toggle>();
        if (buttonRegister == null) buttonRegister = this.transform.Find("ButtonRegister").GetComponent<Button>();
        if (buttonLogin == null) buttonLogin = this.transform.Find("ButtonLogin").GetComponent<Button>();

        RefreshData();

        inputAccount.onEndEdit.AddListener((text) => 
        {
            //账号输入框 结束输入
            account = text;
        });
        inputPassword.onEndEdit.AddListener((text) => 
        {
            //密码输入框 结束输入
            password = text;
        });
        toggleP.onValueChanged.AddListener((value) =>
        {
            //点击 记住密码
            togglep = value;
        });
        toggleL.onValueChanged.AddListener((value) => 
        {
            //点击 自动登录
            togglel = value;
        });
        buttonRegister.onClick.AddListener(() => 
        {
            //点击 注册按钮
            UIManager.Instance.Show<RegisterPanel>();
            //Hide();
            UIManager.Instance.Hide<LoginPanel>();
        });
        buttonLogin.onClick.AddListener(() => 
        {
            //点击 登录按钮
            if (GameDataManager.Instance.TryLogin(account, password,togglep,togglel))
            {
                UIManager.Instance.Show<ServerPanel>();
                UIManager.Instance.Hide<LoginPanel>();
            }
            else
            {
                Debug.Log("你非法了");
            }
        });
    }
    private void RefreshData()
    {
        if (GameDataManager.Instance.thisUser != null)
        {
            togglep = toggleP.isOn = GameDataManager.Instance.thisUser.remanber;
            togglel = toggleL.isOn = GameDataManager.Instance.thisUser.login;
            if (GameDataManager.Instance.thisUser.remanber)
            {
                account = inputAccount.text = GameDataManager.Instance.thisUser.account;
                password = inputPassword.text = GameDataManager.Instance.thisUser.password;
            }
            if (GameDataManager.Instance.thisUser.login)
            {
                UIManager.Instance.Show<ServerPanel>();
                UIManager.Instance.Hide<LoginPanel>();
            }
        }
    }
}
