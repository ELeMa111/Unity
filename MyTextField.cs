using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TextType
{
    Normal,Password
}
public class MyTextField : BaseControl
{
    //public string text = new string("");
    [Header("输入框类型")]
    public TextType type = TextType.Normal;
    [Header("加密字符")]
    public char passwordType = '*';
    protected override void DrawStyleOn()
    {
        switch (type)
        {
            case TextType.Normal:
                content.text = GUI.TextField(position.Rect, content.text, style);
                break;
            case TextType.Password:
                content.text = GUI.PasswordField(position.Rect, content.text, passwordType,style);
                break;
            default:break;
        }
    }
    protected override void DrawStyleOff()
    {
        switch (type)
        {
            case TextType.Normal:
                content.text = GUI.TextField(position.Rect, content.text);
                break;
            case TextType.Password:
                content.text = GUI.PasswordField(position.Rect, content.text, passwordType);
                break;
            default: break;
        }
    }
}
