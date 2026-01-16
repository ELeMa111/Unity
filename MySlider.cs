using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum SliderType
{
    Horizontal, Vertical
}
public class MySlider : BaseControl
{
    [Header("拖动条类型")]
    public SliderType sliderType = SliderType.Horizontal;
    [Header("拖动条范围")]
    public float LeftorUpValue = 0;
    public float RightorDownValue = 1;
    [Header("GUIStyleofSlider")]
    public GUIStyle sliderStyle;
    public GUIStyle thumbStyle;
    public GUIStyle thumbExtentStyle;

    public UnityAction<float> SliderAction = null;
    
    public float newValue;
    private float oldValue;

    protected override void DrawStyleOn()
    {
        switch (sliderType)
        {
            case SliderType.Horizontal:
                newValue = GUI.HorizontalSlider(position.Rect, newValue, LeftorUpValue, RightorDownValue, sliderStyle, thumbStyle, thumbExtentStyle);
                break;
            case SliderType.Vertical:
                if (thumbExtentStyle == GUIStyle.none) { newValue = GUI.VerticalSlider(position.Rect, newValue, LeftorUpValue, RightorDownValue, sliderStyle, thumbStyle); }
                else { newValue = GUI.VerticalSlider(position.Rect, newValue, LeftorUpValue, RightorDownValue, sliderStyle, thumbStyle, thumbExtentStyle); }
                break;
                default:break;
        }
        DidValueChanged();
    }
    protected override void DrawStyleOff()
    {
        switch (sliderType)
        {
            case SliderType.Horizontal:
                newValue = GUI.HorizontalSlider(position.Rect, newValue, LeftorUpValue, RightorDownValue);
                break;
            case SliderType.Vertical:
                newValue = GUI.VerticalSlider(position.Rect, newValue, LeftorUpValue, RightorDownValue);
                break;
            default: break;
        }
        DidValueChanged();
    }
    private void DidValueChanged()
    {
        if (oldValue != newValue)
        {
            //数值被修改
            SliderAction?.Invoke(newValue);
            oldValue = newValue;
        }
    }
}
