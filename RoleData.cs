using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 所有数据的数据集合
/// </summary>
public class RoleData
{
    public List<RoleInfo> roleList = new List<RoleInfo>();
}
/// <summary>
/// 单个角色数据
/// </summary>
public class RoleInfo
{
    [XmlAttribute]
    public int HP;
    [XmlAttribute] 
    public int speed;
    [XmlAttribute] 
    public int volume;
    //资源路径
    [XmlAttribute] 
    public string resName;
    //选角界面缩放大小
    [XmlAttribute] 
    public float scale;
}