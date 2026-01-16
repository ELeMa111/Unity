using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 本来应该不再为单选框组件设置List
/// 遍历单选框对象的所有子对象
/// 他们应该是一个多选框对象
/// 获取返回值
/// 如果为真，而index不指向它//点击了一个本来为假的选项
/// 就让index指向它，并立刻遍历子对象，将其他全部置为假
/// 如果为假，而index指向它//点击了一个本来为真的选项
/// 就让它置为真
/// 
/// 但是本来已经实现了按钮加选项，就拉倒了
/// 
/// 但是我的ParentControl组件只绘制下一级对象，而单选框的下一级没有被绘制
/// 在CreateandControl方法中,在遍历list时Draw一次
/// 有个问题，如果一个对象是单选框的子对象，但不属于单选框的List
/// 他就不会被画出来
/// 因为单选框遍历的是List，而不是所有子对象
/// 拉倒了，懒得改了
/// 
/// 妈的纯屎山
/// 
/// 单选框组件和窗口组件不一样
/// 
/// 还是改成了利用数组存储
/// 不再每帧遍历子对象，只在子对象被更新时遍历
/// 每帧遍历控件列表，计算并绘制他们
/// </summary>
public class MyRadioToggle : BaseControl
{
    [Header("当前选中单选框的索引")]
    public int index = 0;
    //[Header("待创建对象实例")]
    //public GameObject MyTogglePrefab;
    //[Header("单选框列表")]
    //public List<GameObject> myToggles = null;
    [Header("控件列表")]
    public MyToggle[] myToggles = new MyToggle[0];

    //这个方法会在子对象更新时被调用一次
    private void OnTransformChildrenChanged()
    {
        RefreshArray();
    }
    //更新控件列表的方法
    private void RefreshArray()
    {
        try { myToggles = this.GetComponentsInChildren<MyToggle>(); }
        catch { Debug.Log("子对象中不存在关联MyToggle的控件"); }
    }

    //因为单选框就是创建一组多选框并进行数值管理
    //Style和Content取决于每个单选框的单独设置
    protected override void DrawStyleOn()
    {
        CalculateValueandDraw();
    }
    protected override void DrawStyleOff()
    {
        CalculateValueandDraw();
    }
    //被弃用的实现单选框的方法
    //他还实现了添加预制单选框功能
    //private void CreateandControl()
    //{
    //    //遍历List
    //    for (int i = 0; i < myToggles.Count; i++)
    //    {
    //        //如果为空，创建对象实例
    //        if (myToggles[i] == null || myToggles[i].name != "myRadioToggle" + i)
    //        {
    //            GameObject myRadioToggle = GameObject.Instantiate(MyTogglePrefab);
    //            myRadioToggle.name = "myRadioToggle" + i;
    //            myRadioToggle.transform.parent = this.transform;
    //            //myRadioToggle.GetComponent<MyToggle>().value = true;
    //            myToggles[i] = myRadioToggle;
    //        }
    //        //如果没有多选框脚本，添加脚本
    //        else if (myToggles[i].GetComponent<MyToggle>() == null)
    //        {
    //            myToggles[i].AddComponent<MyToggle>();
    //        }
    //        //进入，实现单选框
    //        else
    //        {
    //            if (myToggles[i].GetComponent<MyToggle>().value && index != i)
    //            {
    //                //返回值为真且index没有指向它
    //                //让index指向它并立刻修改其他所有单选框的值
    //                //防止后续遍历
    //                index = i;
    //                //遍历修改value
    //                for (int j = 0; j < myToggles.Count; j++)
    //                {
    //                    myToggles[j].GetComponent<MyToggle>().value = index == j ? true : false;
    //                }
    //            }
    //            //如果value为false但是index指向它，说明本来为真的选项被点击改成假的
    //            //希望设置这个单选框组件是有且只有一个为真
    //            //所以将它的value重置为真
    //            else if (!myToggles[i].GetComponent<MyToggle>().value && index == i)
    //            {
    //                myToggles[i].GetComponent<MyToggle>().value = true;
    //            }
    //        }
    //        myToggles[i].GetComponent<BaseControl>().Draw();
    //    }
    //}

    private void CalculateValueandDraw()
    {
        //遍历控件列表
        for(int i = 0; i < myToggles.Length; i++)
        {
            if (myToggles[i].newValue && index != i)
            {
                index = i;
                for (int j = 0; j < myToggles.Length; j++)
                {
                    myToggles[j].newValue = index == j ? true : false;
                }
            }
            else if (!myToggles[i].newValue && index == i)
            {
                myToggles[i].newValue = true;
            }
            //绘制每个控件
            if(myToggles[i].gameObject.activeSelf == true)
            {
                try { myToggles[i].Draw(); }
                catch { }
                finally { }
            }
        }
    }
}
