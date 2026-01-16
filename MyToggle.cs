using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyToggle : BaseControl
{
    [Header("当前选中")]
    public bool newValue = false;
    private bool oldValue = false;

    public UnityAction<bool> ToggleAction;
    protected override void DrawStyleOn()
    {
        newValue = GUI.Toggle(position.Rect, newValue, content, style);
        DidValueChanged();
    }
    protected override void DrawStyleOff()
    {
        newValue = GUI.Toggle(position.Rect, newValue, content);
        DidValueChanged();
    }
    private void DidValueChanged()
    {
        if(newValue != oldValue)
        {
            //按钮被点击
            ToggleAction(newValue);
            oldValue = newValue;
        }
    }
}
