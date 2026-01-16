using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WindowType
{
    Normal,ModalWindow
}
/// <summary>
/// 理论上应该实现在Window中添加组件的功能
/// 拖拽组件到Window组件下，作为它的子对象
/// MyWindow组件获取子对象的组件脚本
/// 在DrawWindow方法中调用子对象的绘制方法
/// 取消激活Window组件下的所有组件gameObject，防止绘制两次
/// 或者只在window中绘制未激活的gameObject
/// 
/// Draw方法不能被调用
/// 
/// Draw方法现在可以被调用了
/// 
/// 不再每帧遍历子对象
/// 只有在子对象更新时，遍历子对象并更新控件列表
/// 每帧遍历控件列表并执行绘制方法
/// </summary>
public class MyWindow : BaseControl
{
    [Header("窗口类型")]
    public WindowType windowType = WindowType.Normal;
    [Header("能否拖拽")]
    public bool canDrag = true;
    [Header("拖拽范围")]
    public Rect dragSpace = new Rect();
    [Header("窗口ID")]
    public int id=0;
    [Header("控件列表")]
    public BaseControl[] baseControls = new BaseControl[0];

    private int Length = 0;
    private Rect defaultRect = new Rect(0, 0, 0, 0);
    private Rect newRect;

    //这个方法会在子对象更新时被调用一次
    private void OnTransformChildrenChanged()
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

    protected override void DrawStyleOn()
    {
        switch (windowType)
        {
            case WindowType.Normal:
                newRect = GUI.Window(id, position.Rect, DrawWindowStyleOn, content, style);
                position.DragWindow(newRect);
                break;
            case WindowType.ModalWindow:
                newRect = GUI.ModalWindow(id, position.Rect, DrawWindowStyleOn, content, style);
                position.DragWindow(newRect); 
                break;
            default:break;
        }
    }
    protected override void DrawStyleOff()
    {
        switch (windowType)
        {
            case WindowType.Normal:
                newRect = GUI.Window(id, position.Rect, DrawWindowStyleOff, content);
                position.DragWindow(newRect);
                break;
            case WindowType.ModalWindow:
                newRect = GUI.ModalWindow(id, position.Rect, DrawWindowStyleOff, content);
                position.DragWindow(newRect); 
                break;
            default: break;
        }
    }
    private void DrawWindowStyleOn(int id)
    {
        if (canDrag)
        {
            if (dragSpace == defaultRect) GUI.DragWindow();
            else { GUI.DragWindow(dragSpace); }
        }
        DrawMyChild();
    }
    private void DrawWindowStyleOff(int id)
    {
        if (canDrag)
        {
            if (dragSpace == defaultRect) GUI.DragWindow();
            else { GUI.DragWindow(dragSpace); }
        }
        DrawMyChild();
    }
    private void DrawMyChild()
    {
        /*
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<BaseControl>().Draw();
        }
        */
        for (int i = 0; i < baseControls.Length; i++)
        {
            baseControls[i].Draw();
        }
    }
}
