using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserItem 
{
    public int id;
    public string account;
    public string password;
    public bool remanber;
    public bool login;
    public int lastServer;
}
public class UserData
{
    public List<UserItem> userList=new List<UserItem>();
}