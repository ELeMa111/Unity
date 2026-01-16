using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewardWeapon : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject rewardEff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int r = Random.Range(0,weapons.Length);
            other.GetComponent<PlayerTank>().SetWeapon(weapons[r]);

            GameObject eff = GameObject.Instantiate(rewardEff,this.transform.position,this.transform.rotation);
            AudioSource audioSource = eff.GetComponent<AudioSource>();
            audioSource.volume = GameDataManager.Instance.musicandSoundValue.SoundValue;
            audioSource.mute = !GameDataManager.Instance.musicandSoundValue.SoundOn;
            Destroy(eff,3);
            Destroy(this.gameObject);
        }
    }
}
