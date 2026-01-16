using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObject : MonoBehaviour
{
    public int maxHP;
    public GameObject[] rewards;
    private int currentHP;
    public GameObject effectObject;

    private void Start() 
    {
        currentHP = maxHP;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            int hurt = other.GetComponent<Bullet>().tank.atk;
            currentHP = Mathf.Max(0, currentHP - hurt);
            if (currentHP == 0)
            {
                CreateReward();
                GameObject eff = GameObject.Instantiate(effectObject, this.transform.position, this.transform.rotation);
                if (eff.GetComponent<AudioSource>() != null)
                {
                    AudioSource audioSource = eff.GetComponent<AudioSource>();
                    audioSource.volume = GameDataManager.Instance.musicandSoundValue.SoundValue;
                    audioSource.mute = !GameDataManager.Instance.musicandSoundValue.SoundOn;
                }
                Destroy(this.gameObject);
            }
        }
    }
    private void CreateReward()
    {
        int index = Random.Range(0,100);
        if (index < 50)
        {
            //50%
            if (index >= 0 && index < 10) { GameObject.Instantiate(rewards[0], this.transform.position, this.transform.rotation); }
            else if (index >= 10 && index < 20) { GameObject.Instantiate(rewards[1], this.transform.position, this.transform.rotation); }
            else if (index >= 20 && index < 30) { GameObject.Instantiate(rewards[2], this.transform.position, this.transform.rotation); }
            else if (index >= 30 && index < 40) { GameObject.Instantiate(rewards[3], this.transform.position, this.transform.rotation); }
            else { GameObject.Instantiate(rewards[4], this.transform.position, this.transform.rotation); }
        }
    }
}