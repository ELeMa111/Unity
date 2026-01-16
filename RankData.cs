using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RankData
{
    public List<ItemData> list = new List<ItemData>(); 
}
public class ItemData
{
    [XmlAttribute]
    public string name;
    [XmlAttribute]
    public float time;
    public ItemData(string name,float time)
    {
        this.name = name;
        this.time = time;
    }
    public ItemData() { }
}