using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public class SerializerDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
    public XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        XmlSerializer sKey = new XmlSerializer(typeof(TKey));
        XmlSerializer sValue = new XmlSerializer(typeof(TValue));

        reader.Read();
        while (reader.NodeType!=XmlNodeType.EndElement)
        {
            TKey key = (TKey)sKey.Deserialize(reader);
            TValue value = (TValue)sValue.Deserialize(reader);
            this.Add(key, value); 
        }
        reader.Read();
    }

    public void WriteXml(XmlWriter writer)
    {
        //自定义字典的序列化规则
        
        //翻译机器
        XmlSerializer sKey = new XmlSerializer(typeof(TKey));
        XmlSerializer sValue = new XmlSerializer(typeof(TValue));

        foreach (KeyValuePair<TKey,TValue> kv in this)
        {
            //键值对序列化
            sKey.Serialize(writer, kv.Key);
            sValue.Serialize(writer, kv.Value);
        }
    }
}
