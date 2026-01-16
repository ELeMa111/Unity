using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 声明了一个事件ButtonAction
/// 如果想要使用这个button组件，需要获取到它
/// muButton1.ButtonAction += ()=>{ //按钮响应事件 }
/// 通过这种方法调用，来让按钮响应
/// </summary>
public class MyButton :BaseControl
{
    public event Action ButtonAction = ()=> { };
    protected override void DrawStyleOn()
    {
        if(GUI.Button(position.Rect, content, style))
        {
            ButtonAction?.Invoke();
        }    
    }
    protected override void DrawStyleOff()
    {
        if (GUI.Button(position.Rect, content))
        {
            ButtonAction?.Invoke();
        }
    }
}
