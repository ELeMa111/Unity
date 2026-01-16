using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    public BulletInfo info;
    private float time = 0;
    public void Initialize(BulletInfo info) 
    {
        this.info = info;
        //Destroy(gameObject,info.lifeTime);
        Invoke("WaitToDestroy",info.lifeTime);
    }
    private void WaitToDestroy()
    {
        Destroy(gameObject);
    }
    public void Die()
    {
        GameObject eff = GameObject.Instantiate(Resources.Load<GameObject>(info.deadEffres));
        eff.transform.position = this.transform.position;
        eff.transform.rotation = Quaternion.LookRotation(-this.transform.forward);
        Destroy(eff,1f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        //
        PlayerObject obj = other.GetComponent<PlayerObject>();
        obj.Hurt();
        Die();
    }
    private void Move()
    {
        //œÚ«∞“∆∂Ø
        this.transform.Translate(Vector3.forward * Time.deltaTime * info.forwardSpeed, Space.Self);
        //
        switch (info.type)
        {
            case 2:
                this.transform.Translate(Vector3.right * Time.deltaTime * Mathf.Sin(time*Mathf.PI) * info.rightSpeed);
                break;
            case 3:
                this.transform.rotation *= Quaternion.AngleAxis(info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 4:
                this.transform.rotation *= Quaternion.AngleAxis(info.roundSpeed * Time.deltaTime, -Vector3.up);
                break;
            case 5:
                Quaternion targetQ = Quaternion.LookRotation(PlayerObject.Instance.transform.position-this.transform.position);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,targetQ,Time.deltaTime*info.roundSpeed);
                break;
            default:break;
        }
    }
    private void Update()
    {
        time += Time.deltaTime;
        Move();
    }
}
