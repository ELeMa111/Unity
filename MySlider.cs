using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private float value;

    protected override void DrawStyleOn()
    {
        switch (sliderType)
        {
            case SliderType.Horizontal:
                value = GUI.HorizontalSlider(position.Rect, value, LeftorUpValue, RightorDownValue, sliderStyle, thumbStyle, thumbExtentStyle);
                break;
            case SliderType.Vertical:
                if (thumbExtentStyle == GUIStyle.none) { value = GUI.VerticalSlider(position.Rect, value, LeftorUpValue, RightorDownValue, sliderStyle, thumbStyle); }
                else { value = GUI.VerticalSlider(position.Rect, value, LeftorUpValue, RightorDownValue, sliderStyle, thumbStyle, thumbExtentStyle); }
                break;
                default:break;
        }
    }
    protected override void DrawStyleOff()
    {
        switch (sliderType)
        {
            case SliderType.Horizontal:
                value = GUI.HorizontalSlider(position.Rect, value, LeftorUpValue, RightorDownValue);
                break;
            case SliderType.Vertical:
                value = GUI.VerticalSlider(position.Rect, value, LeftorUpValue, RightorDownValue);
                break;
            default: break;
        }
    }
}
