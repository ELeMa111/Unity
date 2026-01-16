using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
#region 我第一次写的PlayerPrefsDataManager

///// <summary>
///// PlayerPrefs数据管理类，统一实现数据的存储和读取
///// 自己写的，还行
///// 不知道怎么获取对象的所有信息
///// 字段：FieldInfo
///// 私有变量?
///// 数组？
///// 没加存储数组的功能
///// 没加的好多
///// </summary>
//public class PlayerPrefsDataManager
//{
//    #region 实现单例模式
//    private static PlayerPrefsDataManager instance = new PlayerPrefsDataManager();
//    private PlayerPrefsDataManager() { }
//    public static PlayerPrefsDataManager Instance { get { return instance; } }
//    #endregion

//    /// <summary>
//    /// 存储数据
//    /// </summary>
//    /// <param name="data"></param>
//    /// <param name="keyName"></param>
//    public void SaveData(object data, string keyName)
//    {
//        /*
//        我只实现了int/float/string/bool类型的存储
//        没有实现enum/未指定的基本数据类型/私有成员等的存储
//        它应该可以通过递归存储深层数据，比如传入Player对象，可以存储它的name/age/equipmentid/equipmentnum等
//        没有实现id，同类型存储一定会覆盖，只能在调用时在keyName上加id
//        没有实现数组/List/Dic等的存储
//         */
//        if (data is Int32)
//        {
//            PlayerPrefs.SetInt(keyName, Convert.ToInt32(data));
//        }
//        else if (data is float)
//        {
//            PlayerPrefs.SetFloat(keyName, (float)data);
//        }
//        else if (data is string)
//        {
//            PlayerPrefs.SetString(keyName, data.ToString());
//        }
//        else if (data is bool)
//        {
//            PlayerPrefs.SetInt(keyName, Convert.ToBoolean(data) ? 1 : 0);
//        }
//        else
//        {
//            //此时data是一个自定义类型对象/或空
//            //不判断是否为空，让空值也可以被存储
//            /*
//            我想获取data的每个成员，  Player(name,age,Equipment)
//            然后让keyName+成员名      Player_Name/Player_Age/Player_Equipment
//            然后递归
//             */

//            Type dataType = data.GetType(); //Player自定义类型对象，有三个成员
//            FieldInfo[] fieldInfos = dataType.GetFields();  //得到Player的所有字段/name/age/Equipment
//            for (int i = 0; i < fieldInfos.Length; i++)
//            {
//                //遍历所有字段
//                String name = fieldInfos[i].Name;   //name
//                Type type = fieldInfos[i].FieldType;    //String
//                //递归调用SaveData
//                //参数一：fieldInfos[i].GetValue(data)
//                /*
//                这是FieldInfo提供的方法
//                FieldInfo：字段的元数据类，包含Name，Type等信息
//                fieldInfos[i].GetValue(data)
//                上述代码的意义：
//                返回data对象的 指定字段fieldInfos[i]的 值
//                 */
//                SaveData(fieldInfos[i].GetValue(data), keyName + "_" + name);
//            }
//        }
//    }
//    /// <summary>
//    /// 读取数据
//    /// </summary>
//    /// <param name="type"></param>
//    /// <param name="keyName"></param>
//    /// <returns></returns>
//    public object LoadData(Type type, string keyName)
//    {
//        //LoadData(int,age)//返回Object obj = new Int32 (20)
//        //LoadData(Player,player1)//返回Object obj = new Player ()

//        object obj = Activator.CreateInstance(type);

//        /*
//        遍历obj的所有成员，如果是int/float/string/bool就赋值
//        如果是自定义类型就递归
        
//        保存时
//        Player1的equipmentnum 保存为：Player1_Equipment_equipmentnum
//        读取时
//        keyName必须是上述
//         */

//        if (type == typeof(int))
//        {
//            /*
//            type = int
//            keyName = Player1_age
//             */
//            obj = PlayerPrefs.GetInt(keyName);
//        }
//        else if (type == typeof(float))
//        {
//            obj = PlayerPrefs.GetFloat(keyName);
//        }
//        else if (type == typeof(string))
//        {
//            obj = PlayerPrefs.GetString(keyName);
//        }
//        else if (type == typeof(bool))
//        {
//            obj = PlayerPrefs.GetInt(keyName) == 1 ? true : false;
//        }
//        else
//        {
//            /*
//            type是空/数组/List/Dic/自定义类型等
//             */
//            /*
//            type = Player
//            keyName = Player1
//             */
//            /*
//            遍历Player类的所有成员，递归
//             */
//            FieldInfo[] fieldInfos = type.GetFields();  //Player/name/age/equipment
//            for (int i = 0; i < fieldInfos.Length; i++)
//            {
//                Type fieldType = fieldInfos[i].FieldType;
//                //keyName += "_" + fieldInfos[i].Name;
//                fieldInfos[i].SetValue(obj, LoadData(fieldType, keyName + "_" + fieldInfos[i].Name));
//            }
//        }
//        return obj;
//    }
//}

#endregion
#region 我第二次写的PlayerPrefsDataManager
/// <summary>
/// 1、存储和读取Dictionary方法中的判断逻辑
/// typeof(IDictionary).IsAssignableForm(typeof(dataType))
/// 只能判断出当前类型继承IDictionary
/// 可能为哈希表/字典/继承IDictionary的自定义类等
/// 懒得写细分了
/// 
/// 2、在Load方法中，每个方法都需要实例化一个目标类型的对象
/// 对其赋值或对其参数赋值并返回
/// 在实例化对象时，每个方法都
/// 已经知道要返回的对象类型
/// 所以直接new一个目标类型的对象
/// 但是，
/// 通过Activator.CreateInstance(Type dataType)
/// 该方法更符合面向对象的逻辑
/// 即不在意调用者的对象类型
/// 只有在调用该方法时，dataType通过方法参数传进来
/// 此时才知道要实例化一个什么类型的对象
/// 
/// 3、看半天看不出来，存的时候key是小写的
/// 取得时候Key是大写的，测试半天一直返回空
/// 
/// 4、用Type.Name的时候，换成Type.FullName，不然有可能找不到
/// 
/// 5、这个储存的方法不能存储一个没有成员的对象
/// 除非把对象名作为String存储到注册表
/// 
/// </summary>
public class PlayerPrefsDataManager
{
    #region 实现单例模式
    private static PlayerPrefsDataManager instance = new PlayerPrefsDataManager();
    private PlayerPrefsDataManager() { }
    public static PlayerPrefsDataManager Instance { get { return instance; } }
    #endregion
    #region 实现保存数据
    public void SaveData(object data,String key)    //(player1,Player1)(20,Age)
    {
        /*
        传统的SaveData传入的不是int id ，而是String keyName
        保存为 keyName，data
        使用时必须SaveData(new Player player1,player1)
        才能保存为 player1，player1
        我希望直接传入编号1，就可以保存

        不行
        key必须作为参数，才能传给下一层级的成员
         */

        //判空，为空时不保存
        if (data == null) { return; }
        //获取传入的对象类型
        Type dataType = data.GetType();

        //获取传入的对象的类名
        //本来想作为键的路径
        //不能用，可能有多个同类成员
        //String typeName = dataType.Name;

        if (dataType == typeof(sbyte) ||
            dataType == typeof(short) ||
            dataType == typeof(int) ||
            dataType == typeof(long) ||
            dataType == typeof(byte) ||
            dataType == typeof(ushort) ||
            dataType == typeof(uint) ||
            dataType == typeof(ulong))
        {
            //处理整数类型的存储
            SaveInt(data, key);
        }
        else if (dataType == typeof(float) ||
                 dataType == typeof(double) ||
                 dataType == typeof(decimal))
        {
            //处理浮点类型的存储
            SaveFloat(data, key);
        }
        else if (dataType == typeof(String)||
                 dataType == typeof(char))
        {
            //处理String类型和char类型的存储
            SaveString(data, key);
        }
        else if (dataType == typeof(bool))
        {
            //处理bool类型的存储
            SaveBool(data, key);
        }
        else if (dataType.IsArray)
        {
            //处理数组类型的存储
            SaveArray(data, key);
        }
        else if(dataType == typeof(ArrayList))
        {
            //处理ArrayList的存储
            SaveArrayList(data,key);
        }
        else if (dataType.IsGenericType &&
                 dataType.GetGenericTypeDefinition() == typeof(List<>))
        {
            //dataType是不是IList的子类
            //List<T>类是IList的子类
            //不能用是否继承IList判断
            //dataType是不是泛型类/dataType是不是泛型List<>
            //处理List<>的存储
            SaveGenericList(data, key);
        }
        else if (typeof(IDictionary).IsAssignableFrom(dataType))
        {
            //dataType是不是IDictionary的子类
            //Dictionary<,>是IDictionary的子类
            //处理Dictionary的存储
            SaveDictionary(data, key);
        }
        else
        {
            //处理自定义类的存储
            SaveOthers(data, key);
        }
        //将内存的数据保存到硬盘
        PlayerPrefs.Save();
    }
    private void SaveInt(object data, String key)
    {
        PlayerPrefs.SetInt(key,Convert.ToInt32(data));
    }
    private void SaveFloat(object data,String key) 
    {
        PlayerPrefs.SetFloat(key,Convert.ToSingle(data));
    }
    private void SaveString(object data,String key)
    {
        PlayerPrefs.SetString(key,Convert.ToString(data));
    }
    private void SaveBool(object data,String key)
    {
        PlayerPrefs.SetInt(key, Convert.ToBoolean(data) == true ? 1 : 0);
    }
    private void SaveArray(object data,String key)
    {
        /*
        Type dataType = data.GetType();
        Type[] Ttype = dataType.GetGenericArguments();
        搞错了，数组可以是一个任意类型对象的集合，
        但是数组不是一个泛型
         */

        //先获取data的类型
        //Type dataType = data.GetType();
        //这个类型一定是一个数组类型
        //通过GetElementType()方法获取数组的元素类型
        //Type itemType = dataType.GetElementType();

        //直接把data转换为数组类型
        Array dataArr = (Array)data;
        //遍历dataArr，并递归调用
        for(int i = 0; i < dataArr.Length; i++)
        {
            object item = dataArr.GetValue(i);
            SaveData(item, key+"_"+i);
        }
        //保存数组长度
        SaveData(dataArr.Length, key + "_" + "Length");
    }
    private void SaveArrayList(object data,String key)
    {
        ArrayList arrayList = (ArrayList)data;
        for(int i = 0; i < arrayList.Count; i++)
        {
            Type itemType = arrayList[i].GetType();
            String typeName = itemType.Name;
            SaveData(typeName, key + "_" + i + "_" + "Type");
            SaveData(arrayList[i], key + "_" + i);
        }
    }
    private void SaveGenericList(object data,String key)
    {
        //获取data类型
        //Type dataType = data.GetType();
        //List<T>
        
        //想半天，直接将object装载的list对象
        //装换为IList对象，而不在意它的元素泛型是什么类型
        IList ilist = (IList)data;
        for(int i = 0; i < ilist.Count; i++)
        {
            SaveData(ilist[i], key + "_" + i);
        }
        //保存List长度
        SaveData(ilist.Count, key + "_" + "Count");
    }
    private void SaveDictionary(object data,String key)
    {
        //把data装换为IDictionary类型的对象
        IDictionary idictionary = (IDictionary)data;
        int i = 0;
        foreach(object keyofDictionary in idictionary.Keys)
        {
            object dataofDictionary = idictionary[keyofDictionary];
            SaveData(keyofDictionary, key + "_" + "Key"+"_"+i);
            SaveData(dataofDictionary, key + "_" + "Value" + "_" + i);
            i++;
        }
        SaveData(i, key + "_" + "Count");
        //保存字典键和值的类型
        Type[] keyandValueType = data.GetType().GetGenericArguments();
        Type keyType = keyandValueType[0];
        Type valueType = keyandValueType[1];
        PlayerPrefs.SetString(key+"_"+"KeyType",keyType.FullName);
        PlayerPrefs.SetString(key + "_" + "ValueType", valueType.FullName);
    }
    private void SaveOthers(object data,String key)
    {
        //保存自定义类型的方法
        /*
        获取自定义类型的类型
        通过反射获取它的所有成员
        遍历所有成员
        根据成员名重写key
        递归
         */
        Type dataType = data.GetType();
        //获取该类的成员信息
        //包括公共字段、私有字段、静态字段、非静态字段
        FieldInfo[] fieldInfos = dataType.GetFields(BindingFlags.Public|
                                                    BindingFlags.NonPublic|
                                                    BindingFlags.Static|
                                                    BindingFlags.Instance);
        for(int i = 0; i < fieldInfos.Length; i++)
        {
            object dataofField;
            if (fieldInfos[i].IsStatic)
                dataofField = fieldInfos[i].GetValue(null);
            else
                dataofField = fieldInfos[i].GetValue(data);
            String keyofField = key + "_" + fieldInfos[i].Name;
            SaveData(dataofField, keyofField);
        }
    }
    #endregion
    #region 实现读取数据
    public object LoadData(Type dataType,String key)
    {
        object data;
        if (dataType == null) { return null; }
        if(dataType == typeof(sbyte) ||
            dataType == typeof(short) ||
            dataType == typeof(int) ||
            dataType == typeof(long) ||
            dataType == typeof(byte) ||
            dataType == typeof(ushort) ||
            dataType == typeof(uint) ||
            dataType == typeof(ulong))
        {
            //处理整形类型的读取
            data = LoadInt(dataType, key);
        }
        else if(dataType == typeof(float) ||
                 dataType == typeof(double) ||
                 dataType == typeof(decimal))
        {
            //处理浮点类型的读取
            data = LoadFloat(dataType, key);
        }
        else if(dataType == typeof(String)||
                dataType == typeof(char)) 
        {
            //处理String或char类型的读取
            data = LoadString(dataType, key);
        }
        else if (dataType == typeof(bool))
        {
            //处理bool类型的读取
            data = LoadBool(dataType, key);
        }
        else if (dataType.IsArray)
        {
            //处理数组类型的读取
            data = LoadArray(dataType, key);
        }
        else if(dataType == typeof(ArrayList))
        {
            //处理ArrayList的读取
            data = LoadArrayList(dataType,key);
        }
        else if (dataType.IsGenericType &&
                 dataType.GetGenericTypeDefinition() == typeof(List<>))
        {
            //dataType是不是IList的子类
            //List<T>类是IList的子类
            //处理List<T>的读取
            data = LoadGenericList(dataType, key);
        }
        else if (typeof(IDictionary).IsAssignableFrom(dataType))
        {
            //dataType是不是IDictionary的子类
            //Dictionary<,>是IDictionary的子类
            //处理Dictionary的读取
            data = LoadDictionary(dataType, key);
        }
        else
        {
            //处理自定义类的读取
            data = LoadOthers(dataType, key);
        }

        return data;
    }
    private object LoadInt(Type dataType,String key)
    {
        object data;
        data = PlayerPrefs.GetInt(key,0);
        return data;
    }
    private object LoadFloat(Type dataType, String key)
    {
        object data;
        data = PlayerPrefs.GetFloat(key, 0f);
        return data;
    }
    private object LoadString(Type dataType, String key)
    {
        object data;
        data = PlayerPrefs.GetString(key, null);
        return data;
    }
    private object LoadBool(Type dataType,String key)
    {
        object data;
        data = PlayerPrefs.GetInt(key, 0) == 1 ? true : false;
        return data;
    }
    private object LoadArray(Type dataType,String key)
    {
        object data;
        Type itemType = dataType.GetElementType();
        int length = PlayerPrefs.GetInt(key+"_"+"Length", 0);
        Array arr = Array.CreateInstance(itemType,length);
        for(int i = 0; i < length; i++)
        {
            object dataofArray = LoadData(itemType, key + "_" + i);
            arr.SetValue(dataofArray,i);
        }
        data = arr;
        return data;
    }
    private object LoadArrayList(Type dataType,String key)
    {
        object data;
        ArrayList arrayList = new ArrayList();
        int count = PlayerPrefs.GetInt(key+"_"+"Count",0);
        for(int i = 0; i < count; i++)
        {
            String typeName = PlayerPrefs.GetString(key+"_"+i+"_"+"Type");
            Type itemType = Type.GetType(typeName);
            object item = LoadData(itemType,key+"_"+i);
            arrayList.Add(item);
        }
        data = arrayList;
        return data;
    }
    private object LoadGenericList(Type dataType,String key)
    {
        object data;
        //Type itemType = dataType.GetElementType();
        Type[] itemTypes = dataType.GetGenericArguments();
        Type itemType = itemTypes[0];
        int count = PlayerPrefs.GetInt(key+"_"+"Count",0);
        //IList list = new List<object>();
        IList list = (IList)Activator.CreateInstance(dataType);
        for(int i = 0; i < count; i++)
        {
            object dataofList = LoadData(itemType, key + "_" + i);
            list.Add(dataofList);
        }
        data = list;
        return data;
    }
    private object LoadDictionary(Type dataType,String key)
    {
        object data;
        int count = PlayerPrefs.GetInt(key+"_"+"Count",0);
        Type keyType = Type.GetType(PlayerPrefs.GetString(key + "_" + "KeyType", null));
        Type valueType = Type.GetType(PlayerPrefs.GetString(key + "_" + "ValueType", null));
        //IDictionary dic = new Dictionary<object, object>();
        IDictionary dic = (IDictionary)Activator.CreateInstance(dataType);
        for(int i = 0; i < count; i++)
        {
            object itemKey = LoadData(keyType, key + "_" + "Key" + "_" + i);
            object itemValue = LoadData(valueType, key + "_" + "Value" + "_" + i);
            dic.Add(itemKey, itemValue);
        }
        data = dic;
        return data;
    }
    private object LoadOthers(Type dataType,String key)
    {
        if (dataType == null) {  return null; }
        object data = Activator.CreateInstance(dataType);
        FieldInfo[] fieldInfos = dataType.GetFields(BindingFlags.Public |
                                                    BindingFlags.NonPublic |
                                                    BindingFlags.Instance|
                                                    BindingFlags.Static);
        for(int i = 0; i < fieldInfos.Length; i++)
        {
            FieldInfo field = fieldInfos[i];
            Type fieldType = fieldInfos[i].FieldType;
            String fieldName = fieldInfos[i].Name;
            if (fieldInfos[i].IsStatic)
                field.SetValue(null, LoadData(fieldType, key + "_" + fieldName));
            else
                field.SetValue(data, LoadData(fieldType, key + "_" + fieldName));
        }
        return data;
    }
    #endregion
}
#endregion
#region 总结
/*
实现存储功能
通过传入的object data 得到它的Type
通过它的Type 得到它的所有字段
得到所有字段就可以存储

存储Array、ArrayList、List<T>、Dictionary等时
需要额外存储数据
Length、Type、Count、KeyType、ValueType等
 */
/*
得到dataType的方法
Type dataType = data.GetType();
得到所有字段的方法
FieldInfo[] fieldInfos = dataType.GetFields(BindingFlags.Public|
                                            BindingFlags.NonPublic|
                                            BindingFlags.Static|
                                            BindingFlags.Instance);
遍历所有字段
for(int i=0;i<fieldInfos.Length;i++)
得到字段引用和字段名
object dataofField;
if (fieldInfos[i].IsStatic)
    dataofField = fieldInfos[i].GetValue(null);
else
    dataofField = fieldInfos[i].GetValue(data);
String keyofField = key + "_" + fieldInfos[i].Name;
SaveData(dataofField, keyofField);
 */
/*
实现读取功能
通过传入的Type dataType 
object data = Activator.Create(dataType)实例化一个目标返回对象
可以用里氏替换原则来装载上述实例化的对象
比如用IList装List<T>
遍历成员
通过fieldInfos[i].SetValue(data,LoadData(fieldInfos[i].Type));
设置data的成员
递归给所有下级成员赋值
 */
/*
读取List<>
IList genericList = (IList)Activator.Create(dateType);
 */
/*
读取Dictionary
IDictionary genericDictionary = (IDictionary)Activator.Create(dataType)
 */
#endregion
#region 加密思路
/*
找不到，把数据文件放在硬盘上不容易找到的地方
看不懂，让数据的key和value让别人看不懂
解不出，不让别人获取你的加密规则

单机游戏只能提高别人修改数据的门槛
只要获取到源代码
加密就失效了

加密规则

对值加密
存储时：通过运算存储运算后的值
读取时：通过运算得到真实的值

对键加密
存储和读取时：使用自定义的键的运算规则和不能表意的key值

int i = Origioni + 10
SetInt(key,i)

int Resoulti = GetInt(key)
Origioni = Resoulti - 10


 */
#endregion