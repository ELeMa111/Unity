using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 使用UIManager主要是想要使用这个类对面板对象进行统一管理
/// 而不是对每个面板对象进行单独的访问
/// 同时实现可以销毁面板对象而不丢失引用
/// 但是课程中的字典实在是脱裤子放屁
/// 用面板名做键名，用面板对象做值
/// 访问时还需要Dic[key] 才能访问
/// 不知道抽的什么风
/// 和直接声明私有成员感觉没什么区别，
/// 反正也不会被置空
/// </summary>
public class UIManager 
{
    private static UIManager instance = new UIManager();    
    public static UIManager Instance{ get { return instance; } }
    private UIManager() 
    {
        ///*CuePanel*/ cuePanel = Resources.Load<GameObject>("CuePanel").GetComponent<CuePanel>();
        ///*LoginPanel*/ loginPanel = Resources.Load<GameObject>("LoginPanel").GetComponent<LoginPanel>();
        ///*RegisterPanel*/ registerPanel = Resources.Load<GameObject>("RegisterPanel").GetComponent<RegisterPanel>();
        ///*ServerPanel*/ serverPanel = Resources.Load<GameObject>("ServerPanel").GetComponent<ServerPanel>();
        ///*ChoosePanel*/ choosePanel = Resources.Load<GameObject>("ChoosePanel").GetComponent<ChoosePanel>();

        canvas = GameObject.Find("Canvas").transform;
        GameObject.DontDestroyOnLoad(canvas);
        BasePanel[] panel = canvas.GetComponentsInChildren<BasePanel>();
        for(int i = 0; i < panel.Length; i++)
        {
            string key = panel[i].name;
            if (!Dic.ContainsKey(key))
            {
                Dic.Add(key, panel[i]);
            }
        }
        //Dic.Add("CuePanel", cuePanel);
        //Dic.Add("LoginPanel", loginPanel);
        //Dic.Add("RegisterPanel",registerPanel);
        //Dic.Add("ServerPanel",serverPanel);
        //Dic.Add("ChoosePanel",choosePanel);
    }
    ////提示面板
    //public CuePanel cuePanel;
    ////登录面板
    //public LoginPanel loginPanel;
    ////注册面板
    //public RegisterPanel registerPanel;
    ////服务器面板
    //public ServerPanel serverPanel;
    ////服务器选择面板
    //public ChoosePanel choosePanel;

    public Dictionary<string ,BasePanel> Dic = new Dictionary<string ,BasePanel>();
    public Transform canvas;   

    //public T Show<T>(string key)where T : BasePanel 
    //{
    //    /*
    //    GameObject thisPanel = GameObject.Instantiate(Dic[key] as T, canvas, false).gameObject;
    //    if (Dic[key] != null && Dic[key] is T)
    //    {

    //        T panel = thisPanel.GetComponent<T>();

    //        //Dic.Add(typeof(T).Name, panel);
    //        Dic[key] = panel;
    //    }
    //    else
    //    {
    //        Debug.Log("Fail to Instantiate T object");
    //    }
    //    */
    //    Dic[key].Show();
    //    return Dic[key] as T;
    //}
    //public void Hide(BasePanel panel)
    //{
    //    panel.Hide();
    //}

    public T Show<T>()where T: BasePanel
    {
        string key = typeof(T).Name;
        Debug.Log(key);
        if (Dic.ContainsKey(key)) return Dic[key] as T;
        T value = GameObject.Instantiate
        (
            Resources.Load<T>(key),canvas,false        
        );
        Dic.Add(key, value );
        return value;
    }
    public void Hide<T>()
    {
        string key = typeof(T).Name;
        if (!Dic.ContainsKey(key)) return;
        Dic[key].Hide();
        Dic.Remove(key);
    }
}
