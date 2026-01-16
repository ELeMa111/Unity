using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        IntTest intTest = new IntTest();
        ArrayTest arrayTest = new ArrayTest();
        ArrayListTest arrayListTest = new ArrayListTest();
        GenericListTest genericListTest = new GenericListTest();
        DictionaryTest dictionaryTest = new DictionaryTest();
        CustonTest custonTest = new CustonTest();

        #region Test1
        //PLayerPrefsDataManager.Instance.SaveData(intTest, "intTest");
        //IntTest t = PLayerPrefsDataManager.Instance.LoadData(typeof(IntTest), "intTest") as IntTest;
        //Debug.Log(t.intValue);
        //Debug.Log(t.floatValue);
        //Debug.Log(t.stringValue);
        //Debug.Log(t.boolValue);
        #endregion
        #region Test2
        //PLayerPrefsDataManager.Instance.SaveData(arrayTest, "arrayTest");
        //ArrayTest t = PLayerPrefsDataManager.Instance.LoadData(typeof(ArrayTest), "arrayTest") as ArrayTest;
        //for(int i = 0; i < t.ints.Length; i++)
        //{
        //    Debug.Log(t.ints[i]);
        //    Debug.Log(t.array[i].value);
        //}
        #endregion
        #region Test3
        //PLayerPrefsDataManager.Instance.SaveData(arrayListTest, "arrayListTest");
        //ArrayListTest t = PLayerPrefsDataManager.Instance.LoadData(typeof(ArrayListTest), "arrayListTest") as ArrayListTest;
        #endregion
        #region Test4
        //PLayerPrefsDataManager.Instance.SaveData(dictionaryTest, "DictionaryTest");
        //DictionaryTest t = PLayerPrefsDataManager.Instance.LoadData(typeof(DictionaryTest), "DictionaryTest") as DictionaryTest;
        //foreach (var key in t.dictionary.Keys)
        //{
        //    Debug.Log(key);
        //    Debug.Log(t.dictionary[key]);
        //}
        #endregion
        #region Test5
        //PLayerPrefsDataManager.Instance.SaveData(custonTest, "CustonTest");
        //CustonTest t = PLayerPrefsDataManager.Instance.LoadData(typeof(CustonTest), "CustonTest") as CustonTest;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class IntTest
{
    public int intValue;
    public float floatValue;
    public string stringValue;
    public bool boolValue;
    public IntTest()
    {
        this.intValue = 99;
        this.floatValue = 98.1f;
        this.stringValue = "TTTTTT";
        this.boolValue = false;
    }
}
public class ArrayTest
{
    public int[] ints;
    public ArrayItem[] array;
    public ArrayTest()
    {
        ints = new int[] { 1, 2, 3 };
        array = new ArrayItem[] { new ArrayItem(10), new ArrayItem(12), new ArrayItem(14) };
    }   
}
public class ArrayItem
{
    public int value;
    public ArrayItem(int value)
    {
        this.value = value;
    }
    public ArrayItem() { }
}
public class ArrayListTest
{
    public ArrayList arrayList;
    public ArrayListTest()
    {
        arrayList = new ArrayList();
        ArrayListItem1 arrayListItem1 = new ArrayListItem1();
        ArrayListItem2 arrayListItem2 = new ArrayListItem2();
        ArrayListItem3 arrayListItem3 = new ArrayListItem3();
        arrayList.Add(arrayListItem1);
        arrayList.Add(arrayListItem2);
        arrayList.Add(arrayListItem3);
    }
}
public class ArrayListItem1 { public int i = 0; }
public class ArrayListItem2 { public int i = 0; }
public class ArrayListItem3 { public int i = 0; }
public class GenericListTest
{
    List<int> genericList;
    public GenericListTest()
    {
        genericList = new List<int>() { 3, 4, 5, 6 };
    }
}
public class DictionaryTest
{
    public Dictionary<int, string> dictionary;
    public DictionaryTest()
    {
        dictionary = new Dictionary<int, string>() { { 1, "A" }, { 2, "B" } };
    }
}
public class CustonTest
{
    public CustonType1 CustonItem1;
    public CustonType2 CustonItem2;
    public CustonTest()
    {
        CustonItem1 = new CustonType1();
        CustonItem2 = new CustonType2();
    }
}
public class CustonType1 { public int i = 1; }
public class CustonType2 { public int i = 1; }