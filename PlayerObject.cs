using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private static PlayerObject instance;
    private PlayerObject() { }
    public static PlayerObject Instance {  get { return instance; } }
    private void Awake()
    {
        instance = this;
    }


    public int currentHP;
    public int maxHP;
    public int speed;
    public int rotateSpeed;
    private Quaternion targetQ;
    public bool isDie = false;
    
    public void Hurt()
    {
        this.currentHP--;
        GamePanel.Instance.ChangeHP(currentHP);
        if (currentHP <= 0) Die();
    }
    public void Die()
    {
        isDie = true;
        EndPanel.Instance.Show();
    }
    private Vector3 LeftDownPos;
    private Vector3 RightUpPos;
    private void Start()
    {
        LeftDownPos = new Vector3(0,0,Camera.main.transform.position.y);
        RightUpPos = new Vector3(Screen.width,Screen.height,Camera.main.transform.position.y);
        LeftDownPos = Camera.main.ScreenToWorldPoint(LeftDownPos);
        RightUpPos = Camera.main.ScreenToWorldPoint(RightUpPos);
    }
    private float hValue;
    private float vValue;
    private Vector3 newPos;
    private void Update()
    {
        if (isDie) return;

        hValue = Input.GetAxisRaw("Horizontal");
        vValue = Input.GetAxisRaw("Vertical");

        #region 旋转
        if (hValue == 0) targetQ = Quaternion.LookRotation(Vector3.forward);
        //感觉这里有问题，目标角度应该是朝上的，而不是朝前的
        else
            targetQ = hValue < 0 ? Quaternion.AngleAxis(30, Vector3.forward) : Quaternion.AngleAxis(-30, Vector3.forward);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetQ, Time.deltaTime * rotateSpeed);
        #endregion
        #region 移动
        //this.transform.Translate(Vector3.forward * speed * vValue * Time.deltaTime, Space.World);
        //this.transform.Translate(Vector3.right * speed * hValue * Time.deltaTime, Space.World);
        newPos.x = Mathf.Clamp(this.transform.position.x + hValue * speed * Time.deltaTime, LeftDownPos.x,RightUpPos.x);
        newPos.z = Mathf.Clamp(this.transform.position.z + vValue * speed * Time.deltaTime, LeftDownPos.z,RightUpPos.z);
        newPos.y = 0;
        this.transform.position = newPos;
        #endregion
        Fire();
    }
    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 1000f))
            {
                if (hit.collider.gameObject.CompareTag("Bullet"))
                {
                    hit.collider.GetComponent<BulletObject>().Die();
                }
            }
        }
    }
}
