using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCantMove : BaseTank
{
    public float waitTime = 1f;
    public float thisTime = 0;
    public Transform[] shotPlaces;
    public GameObject bullet;
    public void Awake()
    {
        
    }
    private void Update()
    {
        thisTime += Time.deltaTime;
        if (thisTime >= waitTime)
        {
            thisTime = 0f;
            Fire();
        }
    }
    public override void Fire()
    {

        for(int i = 0; i < shotPlaces.Length; i++)
        {
            GameObject thisBullet = GameObject.Instantiate(bullet, shotPlaces[i].position, shotPlaces[i].rotation);
            
            thisBullet.GetComponent<Bullet>().SetTank(this);
        }
    }
    public override void Hurt(BaseTank baseTank)
    {
        //≤ªª· ‹…À
    }
}
