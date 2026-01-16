using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 面板基类，提供显示自己和隐藏自己的方法
/// 
/// 所有继承这个基类的面板本身是一个自己的类
/// 如开始面板是一个开始面板类
/// 
/// 同时他们都会是一个实现单例模式的类
/// 
/// 为了将实现单例模式的代码封装在基类中
/// 使用泛型代表继承这个基类的对象
/// 根据泛型类型实现单例模式
/// 
/// 因为所有面板都应该被挂载在一个游戏对象上
/// 所以基类应该继承MonoBehaviour
/// 同时，在单例模式的实现上，instance应该在Awake方法里被初始化
/// 
/// 在基类里，this指的是基类对象，而instance应该指向自己的对象类型
/// 所以将this as 成 T
/// 
/// T不能是一个结构体，否则会报错，所以加where T:class泛型约束
/// 确保继承基类的对象是一个自定义类
/// </summary>
public class BasePanel<T> : MonoBehaviour where T:class
{
    private static T instance;
    public static T Instance { get { return instance; } }
    protected void Awake()
    {
        //在Awake中初始化的原因是
        //我们的面板脚本 在场景上，一定只会挂载一次
        //那么我们可以在这个脚本的生命周期函数的Awake中
        //直接记录场景上 唯一的这个脚本
        instance = this as T;
    }

    public virtual void ShoworHide(bool show)
    {
        this.gameObject.SetActive(show);
    }

}
