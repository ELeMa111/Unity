using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板基类，所有面板都会继承他
/// 包含了显示/隐藏/初始化的方法
/// </summary>
public abstract class BasePanel<T> : MonoBehaviour where T : class
{
    private static T instance;
    public static T Instance {  get { return instance; } }

    protected virtual void Awake()
    {
        instance = this as T;
    }
    protected virtual void Start()
    {
        Initialize();
    }
    public virtual void Show()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }
    //用于初始化 控件获取/事件监听 等
    public abstract void Initialize();
}
