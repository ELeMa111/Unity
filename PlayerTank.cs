using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : BaseTank
{
    public WeaponObject weapon;
    public Transform weaponMount;
    private void Update()
    {
        Move();
        Rotate();
        RotateHead();
        Fire();
    }
    public override void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //开火
            if (weapon != null)
            {
                //weapon.SetTank(this);
                weapon.Fire();
            }
        }
    }
    private void Move()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Input.GetAxis("Vertical")*Time.deltaTime,Space.Self);
        #region 通过GetKey移动
        /*
        if (Input.GetKey(KeyCode.W)) { }
         */
        #endregion
    }
    private void Rotate()
    {
        this.transform.Rotate(Vector3.up,rotateSpeed*Input.GetAxis("Horizontal")*Time.deltaTime,Space.Self);
    }
    private void RotateHead()
    {
        head.transform.Rotate(Vector3.up, headRotateSpeed * Input.GetAxis("Mouse X") * Time.deltaTime, Space.Self); ;
    }
    public override void Die()
    {
        //
        FailPanel.Instance.ShoworHide(true);
        Time.timeScale = 0f;
        //base.Die();
    }
    public override void Hurt(BaseTank enemy)
    {
        base.Hurt(enemy);
        GamePanel.Instance.RefreshHP(maxHP, currentHP);
    }
    public void SetWeapon(GameObject newWeapon)
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }
        weapon = GameObject.Instantiate(newWeapon,weaponMount.position,weaponMount.rotation).GetComponent<WeaponObject>();
        
        //相当于GameObject.Instantiate(newWeapon,weaponMount，false);//false：不因父子关系改变缩放大小
        //对于Instantiate的重载，封装了设置父对象的功能
        weapon.transform.parent = weaponMount;
        
        weapon.SetTank(this);
    }
}
