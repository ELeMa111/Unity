using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLabel : BaseControl
{
    protected override void DrawStyleOn()
    {
        GUI.Label(position.Rect, content, style);
    }
    protected override void DrawStyleOff()
    {
        GUI.Label(position.Rect, content);
    }
}
