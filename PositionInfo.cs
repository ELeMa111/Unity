using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlignmentType
{
    UpLeft,UpCenter,UpRight,
    CenterLeft,Center,CenterRight,
    DownLeft,DownCenter,DownRight
}
[System.Serializable]
public class PositionInfo
{
    #region 可编辑成员
    [Header("控件数据")]
    public Rect rect = new Rect(0, 0, 100, 40);
    [Header("屏幕对齐方式")]
    public AlignmentType ControlType = AlignmentType.UpLeft;
    [Header("中心点位置")]
    public AlignmentType PointType = AlignmentType.UpLeft;
    //对外获取位置的属性
    public Rect Rect 
    {
        get 
        {
            CalculateData();
            return myRect; 
        } 
    }
    #endregion
    #region 中间数据
    [Header("实际坐标")]
    private Vector2 realCenter = new Vector2(0, 0);
    [Header("中间数据")]
    private Vector2 controlPosition = new Vector2(0, 0);
    private Vector2 centerPosition = new Vector2(0, 0);
    //实际坐标，只在设置位置时使用的数据，通过只读的属性将其返回
    private Rect myRect = new Rect();
    #endregion
    //计算我的数据
    public void CalculateData()
    {
        CalculateIntermediate();
        CalculateRealPosition();
    }
    //计算并重设中间数据
    private void CalculateIntermediate()
    {
        switch (ControlType)
        {
            case AlignmentType.UpLeft:
                controlPosition.x = 0;
                controlPosition.y = 0;
                break;
            case AlignmentType.UpCenter:
                controlPosition.x = Screen.width/2;
                controlPosition.y = 0;
                break;
            case AlignmentType.UpRight:
                controlPosition.x = Screen.width;
                controlPosition.y = 0;
                break;
            case AlignmentType.CenterLeft:
                controlPosition.x = 0;
                controlPosition.y = Screen.height / 2;
                break;
            case AlignmentType.Center:
                controlPosition.x = Screen.width/2;
                controlPosition.y = Screen.height / 2;
                break;
            case AlignmentType.CenterRight:
                controlPosition.x = Screen.width;
                controlPosition.y = Screen.height / 2;
                break;
            case AlignmentType.DownLeft:
                controlPosition.x = 0;
                controlPosition.y = Screen.height;
                break;
            case AlignmentType.DownCenter:
                controlPosition.x = Screen.width / 2;
                controlPosition.y = Screen.height;
                break;
            case AlignmentType.DownRight:
                controlPosition.x = Screen.width;
                controlPosition.y = Screen.height;
                break;
            default:break;
        }
        switch (PointType)
        {
            case AlignmentType.UpLeft:
                centerPosition.x = 0;
                centerPosition.y = 0;
                break;
            case AlignmentType.UpCenter:
                centerPosition.x = rect.width / 2;
                centerPosition.y = 0;
                break;
            case AlignmentType.UpRight:
                centerPosition.x = rect.width;
                centerPosition.y = 0;
                break;
            case AlignmentType.CenterLeft:
                centerPosition.x = 0;
                centerPosition.y = rect.height / 2;
                break;
            case AlignmentType.Center:
                centerPosition.x = rect.width / 2;
                centerPosition.y = rect.height / 2;
                break;
            case AlignmentType.CenterRight:
                centerPosition.x = rect.width;
                centerPosition.y = rect.height / 2;
                break;
            case AlignmentType.DownLeft:
                centerPosition.x = 0;
                centerPosition.y = rect.height;
                break;
            case AlignmentType.DownCenter:
                centerPosition.x = rect.width / 2;
                centerPosition.y = rect.height;
                break;
            case AlignmentType.DownRight:
                centerPosition.x = rect.width;
                centerPosition.y = rect.height;
                break;
            default: break;
        }
        
    }
    //计算实际坐标
    private void CalculateRealPosition()
    {
        realCenter = controlPosition - centerPosition;
        myRect.x = rect.x + realCenter.x;
        myRect.y = rect.y + realCenter.y;
        myRect.width = rect.width;
        myRect.height = rect.height;
    }
    //为了实现窗口拖动，为其实现一个方法
    public void DragWindow(Rect newRect)
    {
        rect = newRect;
    }

}
