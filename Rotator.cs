using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Rotator类
/// 旋转器
/// 让当前关联的游戏对象绕自身Y轴正方向旋转
/// </summary>
public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 45;
    private void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        transform.Rotate(Vector3.up * rotateSpeed*Time.deltaTime, Space.Self); ;
    }
}
