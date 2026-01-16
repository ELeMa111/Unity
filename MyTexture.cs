using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTexture : BaseControl
{
    //[Header("图片")]
    //public Texture image;
    [Header("缩放模式")]
    public ScaleMode scaleMode = ScaleMode.StretchToFill;
    [Header("透明度通道")]
    public bool alphaOn = true;
    [Header("宽高比")]
    public float scale = 0f;
    protected override void DrawStyleOn()
    {
        GUI.DrawTexture(position.Rect,content.image,scaleMode,alphaOn,scale);    
    }
    protected override void DrawStyleOff()
    {
        GUI.DrawTexture(position.Rect, content.image, scaleMode, alphaOn, scale);
    }
}
