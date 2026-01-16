using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject goalObejct;
    private Vector3 newPosition;
    private void Start()
    {
        newPosition.y = this.transform.position.y;
    }
    private void LateUpdate()
    {
        //摄像机相关写在LateUpdate里
        newPosition.x = goalObejct.transform.position.x;
        newPosition.z = goalObejct.transform.position.z;
        this.transform.position = newPosition;
    }
}
