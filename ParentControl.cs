using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 这一套组件的绘制解决方案
/// 基类BaseControl有一个绘制方法，
/// ParentControl组件会获取子对象的组件
/// 利用BaseControl装载
/// 然后遍历调用他们的绘制方法
/// 实现父对象绘制子对象，以控制绘制顺序
/// 
/// 但是这个遍历每一帧都会被执行
/// 会浪费性能
/// 
/// 需要有一个变量
/// 存储它的所有子对象
/// 并且只有在子对象被更新时，才重新遍历获取
/// 
/// 在遍历时，不再是遍历所有的子对象
/// 而是遍历List，这样可以节省性能
/// 
/// </summary>
[ExecuteAlways]
public class ParentControl : MonoBehaviour
{
    [Header("控件列表")]
    BaseControl[] baseControls = new BaseControl[0];

    private int Length = 0;

    //这个方法会在子对象更新时被调用一次
    private void OnTransformChildrenChanged()
    {
        RefreshArray();
    }
    private void Start()
    {
        RefreshArray();
    }
    //更新控件列表的方法
    private void RefreshArray()
    {
        try 
        {
            Length = this.transform.childCount;
            baseControls = new BaseControl[Length];
            for(int i = 0; i < this.transform.childCount; i++)
            {
                baseControls[i] = this.transform.GetChild(i).GetComponent<BaseControl>();
            }
        }
        catch { Debug.Log("某个子对象中不存在基于BaseControl的控件"); }
    }
    private void OnGUI()
    {
        /*
        for (int i = 0; i < this.transform.childCount; i++)
        {
            try
            {
                this.transform.GetChild(i).GetComponent<BaseControl>().Draw();
            }
            catch
            {
                Debug.Log("这个GameObject没有继承BaseControl的组件");
            }
        }
        */
        for (int i = 0; i < baseControls.Length; i++)
        {
            if (baseControls[i].gameObject.activeSelf == true)
            {
                try { baseControls[i].Draw(); }
                catch { /*Debug.Log("报错000001");*/ }
            }
        }
    }
}
