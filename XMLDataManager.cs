using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// XML数据持久化解决方案
/// 为XML格式的数据封装了存储和读取的方法
/// 在这个单例管理器中进行统一的数据存储和读取
/// 避免在每次需要进行数据传输时编写重复代码
/// 同时实现了泛型方法，提升方法泛用性
/// 这个数据管理器利用C#提供的xml序列化解决方案实现数据存取
/// 在进行数据存储和读取时，对数据有一定要求
/// 存储时：
/// 1、当数据或某个成员为空的引用类型时，这个对象不会在xml文件中体现
/// 2、当数据或某个成员为Dictionary类型对象时，会直接报错
/// 3、如果想要存储Dictionary类对象，需要为Dictionary类自定义一个子类，
///    在其中实现IXmlSerializable接口，并在继承的接口方法中自定义这个字典的存取方法
/// 读取时：
/// 1、对于List类对象或成员，DeSerialize方法不会将List对象的初始值重写
///    而是在List的初始值后面再添加读取的数据
///    通过XML反序列化读取数据的对象，最好不要在声明时赋初始值
/// 
/// 实现：
/// 
/// 一、数据存储
/// 1、根据传入的字符串数据，拼接出完整路径
/// 传入的字符串应该为persistentDataPath下的可访问.xml文件名 ,如PlayerInfo
/// 完整访问路径为 Application.persistentDataPath + "/" + "PlayerInfo" + ".xml"
/// 2、根据完整访问路径，创建文件流
/// 通过using(){ }方法创建一个自动关闭的文件流
/// StreamingWriter writer = new StreamingWriter(completePath)创建文件流
/// 3、在using方法体中，声明对应类型的翻译器
/// XmlSerializer serializer = new XmlSerializer(typeof(T))
/// 4、翻译器调用Serialize方法进行数据存储
/// 参数一：文件流对象
/// 参数二：数据对象
/// 5、using结束，文件流自动关闭，存储结束
/// 
/// 二、数据加载
/// 1、根据传入的字符串数据，拼接完整路径
/// 优先拼接出 persistentDataPath目录下的路径，
/// 通过File.Exist(completePath) 方法判断数据文件是否存在
/// 当数据文件不存在时，有可能是第一次加载该对象数据，还没有在persistentDataPath文件夹下进行过保存
/// 尝试通过File.Exist()寻找 StreamingAssetsPath目录下的该类数据文件，可能有这个类对象的初始化文件
/// 如果两个路径都没有，就直接返回空并报警告
/// 2、根据拼接路径，创建文件流
/// 如果在上文某个目录中找到了该文件，就根据完整路径创建文件流
/// using(StreamingReader reader = new StreamingReader(completePath)){ }
/// 3、在using方法体中，创建对应类型的翻译器
/// XmlSerializer serializer = new XmlSerializer(typeof(T))
/// 4、翻译器调用Deserialize方法进行数据加载
/// 参数一：文件流
/// 返回值：object类型数据
/// 通过在using方法外部声明的T泛型类对象data接收Deserializer的返回值
/// 5、using结束，文件流自动关闭
/// 6、将data返回给外部，加载结束
/// </summary>
public class XMLDataManager
{
    private static XMLDataManager instance = new XMLDataManager();
    private XMLDataManager() { }
    public static XMLDataManager Instance {  get { return instance; } }

    public void SaveData<T>(T data,string path)
    {
        string completePath = Application.persistentDataPath + "/" + path + ".xml";
        using (StreamWriter writer = new StreamWriter(completePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(writer,data);  
        }
    }
    public T LoadData<T>(string path)where T:class
    {
        T data = null;
        string completePath = Application.persistentDataPath + "/" + path + ".xml";
        //如果PersistentDataPath目录下不存在目标数据，就尝试去StreamingAssetPath目录下找
        if (!File.Exists(completePath))
        {
            Debug.LogWarning("PersistentDataPath未找到指定数据");
            completePath = Application.streamingAssetsPath + "/" + path + ".xml";
            if (!File.Exists(completePath))
            {
                Debug.LogWarning("StreamingAssetsPath未找到指定数据");
                return null;
            }
        }
        using (StreamReader reader = new StreamReader(completePath))
        {
            XmlSerializer serializer = new XmlSerializer (typeof(T));
            data = serializer.Deserialize(reader) as T;
        }
        return data;
    }
}
