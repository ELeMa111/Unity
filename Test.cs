using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.name = "Ã∆¿œ ¶";
        playerInfo.age = 10;
        playerInfo.speed = 12.1f;
        playerInfo.list = new List<int>();
        playerInfo.list.Add(13);
        playerInfo.list.Add(14);
        playerInfo.list.Add(15);
        Item item1 = new Item();
        item1.name = "AAA";
        item1.id = 0;
        Item item2 = new Item();
        item2.name = "BBB";
        item2.id = 1;
        playerInfo.dic = new SerializerDictionary<int, Item>();
        playerInfo.dic.Add(1, item1);
        playerInfo.dic.Add(2, item2);

        //XMLDataManager.Instance.SaveData(playerInfo,"PlayerInfo");
        //PlayerInfo playerInfo1 = new PlayerInfo();
        //playerInfo1 = XMLDataManager.Instance.LoadData<PlayerInfo>("PlayerInfo");
        //Debug.Log(playerInfo1.dic[2].name);
    }
}
public class PlayerInfo
{
    public string name;
    public int age;
    public float speed;
    public List<int> list;
    public SerializerDictionary<int, Item> dic;
}
public class Item
{
    public int id;
    public string name;
}