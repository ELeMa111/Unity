using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyToggle : BaseControl
{
    [Header("当前选中")]
    public bool value = false;
    protected override void DrawStyleOn()
    {
        value = GUI.Toggle(position.Rect, value, content, style);
    }
    protected override void DrawStyleOff()
    {
        value = GUI.Toggle(position.Rect, value, content);
    }
}
