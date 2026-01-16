using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BaseControl : MonoBehaviour
{
    [Header("控件信息类")]
    public PositionInfo position = new PositionInfo();
    [Header("控件内容信息")]
    public GUIContent content = new GUIContent();
    [Header("是否开启自定义样式")]
    public bool GUIStyleOn = false;
    [Header("控件样式信息")]
    public GUIStyle style;

    public void Draw()
    {
        switch (GUIStyleOn)
        {
            case true:
                DrawStyleOn();
                break;
            case false:
                DrawStyleOff();
                break;
        }
    }
    protected virtual void DrawStyleOn() { }
    protected virtual void DrawStyleOff() { }
}
