using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum E_PosType
{
    TopLeft,TopCenter,TopRight,CenterLeft,CenterRight,BottomLeft,BottomCenter,BottonRight
}
public class FireObject : MonoBehaviour
{
    public E_PosType posType = E_PosType.TopLeft;
    Vector3 LeftDown;
    Vector3 RightUp;
    private Vector3 initDir;
    private FireInfo fireInfo;

    private void Start()
    {
        RefreshPos();
    }
    private void Update()
    {
        RefreshPos();
        RefreshFireInfo();
        UpdateFire();
    }
    private void RefreshPos()
    {
        LeftDown = new Vector3(0,0,Camera.main.transform.position.y);
        LeftDown = Camera.main.ScreenToWorldPoint(LeftDown);
        RightUp = new Vector3(Screen.width,Screen.height,Camera.main.transform.position.y);
        RightUp = Camera.main.ScreenToWorldPoint(RightUp);

        switch (posType)
        {
            case E_PosType.TopLeft:
                this.transform.position = new Vector3( LeftDown.x, 0, RightUp.z);
                initDir = Vector3.right;
                break;
            case E_PosType.TopCenter:
                this.transform.position = new Vector3( 0, 0, RightUp.z);
                initDir = Vector3.right;
                break;
            case E_PosType.TopRight:
                this.transform.position = new Vector3( RightUp.x, 0, RightUp.z);
                initDir = -Vector3.forward;
                break;
            case E_PosType.CenterLeft:
                this.transform.position = new Vector3( LeftDown.x, 0, 0);
                initDir = Vector3.forward;
                break;
            case E_PosType.CenterRight:
                this.transform.position = new Vector3( RightUp.x, 0, 0);
                initDir = -Vector3.forward;
                break;
            case E_PosType.BottomLeft:
                this.transform.position = new Vector3( LeftDown.x, 0, LeftDown.z);
                initDir = -Vector3.right;
                break;
            case E_PosType.BottomCenter:
                this.transform.position = new Vector3( 0, 0, LeftDown.z);
                initDir = -Vector3.right;
                break;
            case E_PosType.BottonRight:
                this.transform.position = new Vector3( RightUp.x, 0, LeftDown.z);
                initDir = Vector3.forward;
                break;
        }
    }
    private int nowNum;
    private float nowCD;
    private float nowDelay;
    private BulletInfo nowBulletInfo;
    private float changeAngle;
    private void RefreshFireInfo()
    {
        if (nowCD != 0 && nowNum != 0) return;
        if (fireInfo != null)
        {
            nowDelay -= Time.deltaTime;
            if (nowDelay > 0) return;
        }

        List<FireInfo> list = GameDataManager.Instance.fireData.fireInfoList;
        fireInfo = list[Random.Range(0, list.Count)];

        nowNum = fireInfo.num;
        nowCD = fireInfo.cd;
        nowDelay = fireInfo.delay;

        string[] strs = fireInfo.ids.Split(',');
        int beginID = int.Parse(strs[0]);
        int endID = int.Parse(strs[1]);
        int randomBulletID = Random.Range(beginID, endID+1);
        nowBulletInfo = GameDataManager.Instance.bulletData.bulletInfoList[randomBulletID - 1];
        if (fireInfo.type == 2)
        {
            switch (posType)
            {
                case E_PosType.TopLeft:
                case E_PosType.TopRight:
                case E_PosType.BottomLeft:
                case E_PosType.BottonRight:
                    changeAngle = 90f / (nowNum - 1);
                    break;
                case E_PosType.TopCenter:               
                case E_PosType.CenterLeft:
                case E_PosType.CenterRight:
                case E_PosType.BottomCenter:
                    changeAngle = 180f / (nowNum - 1);
                    break;
            }
        }
    }
    private Vector3 nowDir;

    private void UpdateFire()
    {
        if (nowCD == 0 && nowNum == 0) return;



        nowCD-=Time.deltaTime;
        if (nowCD>0)
        {
            return;
        }
        GameObject bullet;
        BulletObject bulletObject;
        
        switch (fireInfo.type)
        {
            case 1:
                bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                bulletObject = bullet.AddComponent<BulletObject>();
                bulletObject.Initialize(nowBulletInfo);
                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = Quaternion.LookRotation(PlayerObject.Instance.transform.position-this.transform.position);
                nowNum--;
                nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                break;
            case 2:
                if (nowCD == 0)
                {
                    for(int i = 0; i < nowNum; i++)
                    {
                        bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                        bulletObject = bullet.AddComponent<BulletObject>();
                        bulletObject.Initialize(nowBulletInfo);
                        bullet.transform.position = this.transform.position;

                        nowDir = Quaternion.AngleAxis(changeAngle * i, Vector3.up) * initDir;
                        bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    }
                    nowCD = nowNum = 0;
                }
                else
                {
                    bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                    bulletObject = bullet.AddComponent<BulletObject>();
                    bulletObject.Initialize(nowBulletInfo);
                    bullet.transform.position = this.transform.position;

                    nowDir = Quaternion.AngleAxis(changeAngle * (fireInfo.num-nowNum), Vector3.up) * initDir;
                    bullet.transform.rotation = Quaternion.LookRotation(nowDir);

                    nowNum--;
                    nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                }
                    break;
        }
    }
}

