using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerData
{
    public List<ServerItem> serverList = new List<ServerItem>();
}
public class ServerItem
{
    public int id;
    public string name;
    public int serverType;
    public bool isNew;
}