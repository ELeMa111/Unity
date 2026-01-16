using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager 
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance {  get { return instance; } }
    public UserData userData;
    public UserItem thisUser;
    public int thisUserID;
    public ServerData serverData;
    public ServerItem thisServer;
    private GameDataManager()
    {
        userData = JsonDataManager.Instance.LoadData<UserData>("UserData");
        if (userData != null)
        {
            for(int i = 0; i < userData.userList.Count; i++)
            {
                if (userData.userList[i].remanber)
                {
                    thisUser = userData.userList[i];
                    
                }
            }
        }
        serverData = JsonDataManager.Instance.LoadData<ServerData>("ServerData");
    }
    public void Test() { }
    public bool AddUserData(UserItem item)
    {
        //if (userData.userList.Count == 0)
        //    item.id = 0;
        //else
        //    item.id = userData.userList.Count;
        //userData.userList.Add(item);
        if (userData != null)
        {
            for (int i = 0; i < userData.userList.Count; i++)
            {
                if (userData.userList[i].account == item.account)
                    return false;
            }
            item.id = userData.userList.Count;
            userData.userList.Add(item);
        }
        else
        {
            item.id = 0;
            userData = new UserData();
            userData.userList = new List<UserItem>() { item };
        }
        SaveUserData();
        return true;
    }
    private void SaveUserData()
    {
        JsonDataManager.Instance.SaveData<UserData>(userData,"UserData");
    }
    public bool TryLogin(string account,string password,bool remanber,bool login)
    {
        if (userData == null) return false;
        else
        {
            for(int i = 0; i < userData.userList.Count; i++)
            {
                if (userData.userList[i].account == account && userData.userList[i].password == password)
                {
                    thisUserID = i;
                    userData.userList[i].remanber = remanber;
                    userData.userList[i].login = login;
                    thisUser = userData.userList[i];
                    RefreshOthers(i);
                    SaveUserData();
                    return true;
                }   
            }
            return false;
        }
    }
    public void RefreshOthers( int j)
    {
        if (userData != null)
        {
            for(int i = 0; i < userData.userList.Count; i++)
            {
                if (i != j)
                {
                    userData.userList[i].remanber = false;
                    userData.userList[i].login = false;
                }
            }
        }
    }
    public GameObject GetVitem()
    {
        return Resources.Load<GameObject>("Vitem0");
    }
    public GameObject GetGitem()
    {
        return Resources.Load<GameObject>("Gitem0");
    }
    public void SetServer(int i)
    {
        thisUser.lastServer = i;
        thisServer = serverData.serverList[i - 1];
        SaveUserData();
    }
}
