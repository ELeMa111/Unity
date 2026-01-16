using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class BaseTank : MonoBehaviour
{
    public int atk = 0;
    public int def = 0;
    public int maxHP = 0;
    public int currentHP = 0;
    public int moveSpeed = 0;
    public int rotateSpeed = 0;
    public int headRotateSpeed = 0;
    public GameObject head;
    public GameObject effectObject;

    public abstract void Fire();
    public virtual void Hurt(BaseTank enemy)
    {
        currentHP = Mathf.Max(0, currentHP - Mathf.Max(0,(enemy.atk - def)));
        if (currentHP == 0) { Die(); }
    }
    public virtual void Die() 
    {
        if (effectObject != null)
        {
            effectObject = GameObject.Instantiate(effectObject,this.transform.position,this.transform.rotation);
            try
            {
                AudioSource audioSource = effectObject.GetComponentInChildren<AudioSource>();
                audioSource.volume = GameDataManager.Instance.musicandSoundValue.SoundValue;
                audioSource.mute = !GameDataManager.Instance.musicandSoundValue.SoundOn;
            }
            catch { }
            finally { }
        }
        Destroy(this.gameObject);
    }

}
