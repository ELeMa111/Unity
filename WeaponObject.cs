using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public GameObject bullet;
    public Transform[] shootPos;
    public BaseTank tank;

    public void Fire()
    {
        for(int i = 0; i < shootPos.Length; i++)
        {
            GameObject bulletObject = GameObject.Instantiate(bullet, shootPos[i].position, shootPos[i].rotation);
            bulletObject.GetComponent<Bullet>().SetTank(tank);
        }
    }
    public void SetTank(BaseTank tank)
    {
        this.tank = tank;
    }
}
