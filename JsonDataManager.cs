using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
/// <summary>
/// 选择使用的存读方法
/// Unity内置的JsonUtility
/// 第三方插件LitJson
/// </summary>
public enum E_JsonType
{
    JsonUtility,
    LitJson
}
/// <summary>
/// 自定义的Json数据持久化单例工具
/// 实现了泛型方法，没有泛型约束
/// 
/// 1、对外提供了SaveData<T>方法
/// 参数一： T data                 要保存的数据
/// 参数二： string path            要保存的路径，路径中的文件夹路径必须存在
/// 参数三： E_JsonType jsonType    默认值为 E_JsonType.LitJson
/// 2、对外提供了LoadData<T>方法
/// 参数一： string path            数据的读取路径
/// 参数二： E_JsonType jsonType    默认值为 E_JsonType.LitJson
/// 返回值： T data                 加载失败时返回T的默认值    
/// 
/// 这个数据持久化工具类在使用时需要导入LitJson工具
/// </summary>
public class JsonDataManager
{
    private static JsonDataManager instance = new JsonDataManager();
    private JsonDataManager() { }
    public static JsonDataManager Instance {  get { return instance; } }

    public void SaveData<T>(T data, string path, E_JsonType jsonType = E_JsonType.LitJson)
    {
        string newPath = Application.persistentDataPath + "/" + path + ".json";
        string jsonStr = null;
        switch (jsonType)
        {
            case E_JsonType.JsonUtility:
                jsonStr = JsonUtility.ToJson(data);          
                break;
            case E_JsonType.LitJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
            default:break;
        }
        File.WriteAllText(newPath, jsonStr);
    }
    public T LoadData<T> (string path,E_JsonType jsonType = E_JsonType.LitJson)
    {
        string newPath = Application.persistentDataPath + "/" + path + ".json";
        if (!File.Exists(newPath))
        {
            Debug.LogWarning("PersistentDataPath/"+path+".json路径下未找到目标数据");
            newPath = Application.streamingAssetsPath + "/" + path + ".json";
            if (!File.Exists(newPath))
            {
                Debug.LogWarning("StreamingAssetsPath"+path+".json路径下未找到目标数据");
                Debug.LogWarning(path + ".json的访问返回默认值");
                return default(T);
            }
        }
        string jsonStr = File.ReadAllText(newPath);
        T data = default(T);
        switch (jsonType)
        {
            case E_JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case E_JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
            default:break;
        }
        return data;
    }
}
