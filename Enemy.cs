using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseTank
{
    private Transform targetTransform;
    public Transform[] randomTransforms;
    public Transform lookTarget;
    public float fireDistance = 5f;
    public float waitTime;
    private float thisTime = 0f;
    public Transform[] shotTransforms;
    public GameObject bullet;
    public Texture textureBack;
    public Texture nowHPTexture;
    public Rect maxHPRect;
    public Rect nowHPRect;
    private float showTime;

    private void Awake()
    {
        RandomTransform();
    }
    private void Update()
    {
        
        this.transform.LookAt(targetTransform);
        this.transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime,Space.Self);

        //Vector3中得到两个点之间距离的方法
        if (Vector3.Distance(this.transform.position, targetTransform.position) < 0.5f)
        {
            RandomTransform();
        }
        if (lookTarget != null)
        {
            head.transform.LookAt(lookTarget);

            if (Vector3.Distance(this.transform.position, lookTarget.transform.position) <= fireDistance)
            {
                thisTime += Time.deltaTime;
                if (thisTime >= waitTime)
                {
                    thisTime = 0f;
                    Fire();
                }
            }
        }
    }
    private void RandomTransform()
    {
        if (randomTransforms.Length == 0)
            return;
        targetTransform = randomTransforms[Random.Range(0, randomTransforms.Length)];
    }
    public override void Fire()
    {
        for(int i = 0; i < shotTransforms.Length; i++)
        {
            GameObject thisBullet = GameObject.Instantiate(bullet, shotTransforms[i].position, shotTransforms[i].rotation);
            thisBullet.GetComponent<Bullet>().SetTank(this);       
        }
    }
    public override void Die()
    {
        GamePanel.Instance.AddScore(10);
        base.Die();
    }
    private void OnGUI()
    {
        if (showTime > 0)
        {
            showTime-= Time.deltaTime;

            Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
            pos.y = Screen.height - pos.y;

            maxHPRect.width = 100*Mathf.Max(0,(50-pos.z))/50f;
            maxHPRect.height = 15* Mathf.Max(0, (50 - pos.z)) / 50f;
            maxHPRect.x = pos.x - maxHPRect.width/2;
            maxHPRect.y = pos.y - 60;
            
            GUI.DrawTexture(maxHPRect, textureBack);
            nowHPRect.x = pos.x - maxHPRect.width / 2;
            nowHPRect.y = pos.y - 60;
            nowHPRect.width = (float)currentHP / maxHP * maxHPRect.width;
            nowHPRect.height = maxHPRect.height;
            GUI.DrawTexture(nowHPRect, nowHPTexture);
        }
        
    }
    public override void Hurt(BaseTank enemy)
    {
        base.Hurt(enemy);
        showTime = 3;
    }
}
