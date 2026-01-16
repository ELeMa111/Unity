using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 50;
    public BaseTank tank;
    public GameObject effectObject;
    private void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == null || tank == null) return;
        if (other.gameObject != tank.gameObject &&
           other.tag != "Reward")
        {
            if (other.GetComponent<BaseTank>() != null)
            {
                other.GetComponent<BaseTank>().Hurt(tank);
            }
            if (effectObject != null)
            {
                GameObject eff = GameObject.Instantiate(effectObject, this.transform.position, this.transform.rotation);
                try
                {
                    eff.GetComponent<AudioSource>().volume = GameDataManager.Instance.musicandSoundValue.SoundValue;
                    eff.GetComponent<AudioSource>().mute = !GameDataManager.Instance.musicandSoundValue.SoundOn;
                }
                catch { }
                finally { }
                Destroy(eff, 2f);
            }
            Destroy(this.gameObject);
        }
    }
     public void SetTank(BaseTank tank)
    {
        this.tank = tank;
    }
}
